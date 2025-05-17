#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

// @Cyanilux | https://github.com/Cyanilux/URP_ShaderGraphCustomLighting
// Note this version of the package assumes v12+ due to usage of "Branch on Input Connection" node
// For older versions, see branches on github repo!

//------------------------------------------------------------------------------------------------------
// Main Light
//------------------------------------------------------------------------------------------------------

/*
- Obtains the Direction, Color and Distance Atten for the Main Light.
- (DistanceAtten is either 0 or 1 for directional light, depending if the light is in the culling mask or not)
- If you want shadow attenutation, see MainLightShadows_float, or use MainLightFull_float instead
*/
void MainLight_float (out float3 Direction, out float3 Color, out float DistanceAtten){
	#ifdef SHADERGRAPH_PREVIEW
		Direction = normalize(float3(1,1,-0.4));
		Color = float4(1,1,1,1);
		DistanceAtten = 1;
	#else
		Light mainLight = GetMainLight();
		Direction = mainLight.direction;
		Color = mainLight.color;
		DistanceAtten = mainLight.distanceAttenuation;
	#endif
}

//------------------------------------------------------------------------------------------------------
// Main Light Layer Test
//------------------------------------------------------------------------------------------------------

#ifndef SHADERGRAPH_PREVIEW
	#if UNITY_VERSION < 202220
	/*
	GetMeshRenderingLayer() is only available in 2022.2+
	Previous versions need to use GetMeshRenderingLightLayer()
	*/
	uint GetMeshRenderingLayer(){
		return GetMeshRenderingLightLayer();
	}
	#endif
#endif
		
/*
- Tests whether the Main Light Layer Mask appears in the Rendering Layers from renderer
- (Used to support Light Layers, pass your shading from Main Light into this)
- To work in an Unlit Graph, requires keywords :
	- Boolean Keyword, Global Multi-Compile "_LIGHT_LAYERS"
*/
void MainLightLayer_float(float3 Shading, out float3 Out){
	#ifdef SHADERGRAPH_PREVIEW
		Out = Shading;
	#else
		Out = 0;
		uint meshRenderingLayers = GetMeshRenderingLayer();
		#ifdef _LIGHT_LAYERS
			if (IsMatchingLightLayer(GetMainLight().layerMask, meshRenderingLayers))
		#endif
		{
			Out = Shading;
		}
	#endif
}

/*
- Obtains the Light Cookie assigned to the Main Light
- (For usage, You'd want to Multiply the result with your Light Colour)
- To work in an Unlit Graph, requires keywords :
	- Boolean Keyword, Global Multi-Compile "_LIGHT_COOKIES"
*/
void MainLightCookie_float(float3 WorldPos, out float3 Cookie){
	Cookie = 1;
	#if defined(_LIGHT_COOKIES)
        Cookie = SampleMainLightCookie(WorldPos);
    #endif
}

//------------------------------------------------------------------------------------------------------
// Main Light Shadows
//------------------------------------------------------------------------------------------------------

/*
- This undef (un-define) is required to prevent the "invalid subscript 'shadowCoord'" error,
  which occurs when _MAIN_LIGHT_SHADOWS is used with 1/No Shadow Cascades with the Unlit Graph.
- It's not required for the PBR/Lit graph, so I'm using the SHADERPASS_FORWARD to ignore it for that pass
*/
#ifndef SHADERGRAPH_PREVIEW
	#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
	#if (SHADERPASS != SHADERPASS_FORWARD)
		#undef REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
	#endif
#endif

/*
- Samples the Shadowmap for the Main Light, based on the World Position passed in. (Position node)
- For shadows to work in the Unlit Graph, the following keywords must be defined in the blackboard :
	- Enum Keyword, Global Multi-Compile "_MAIN_LIGHT", with entries :
		- "SHADOWS"
		- "SHADOWS_CASCADE"
		- "SHADOWS_SCREEN"
	- Boolean Keyword, Global Multi-Compile "_SHADOWS_SOFT"
- For a PBR/Lit Graph, these keywords are already handled for you.
*/
void MainLightShadows_float (float3 WorldPos, half4 Shadowmask, out float ShadowAtten, out float ShadowStrength){
	#ifdef SHADERGRAPH_PREVIEW
		ShadowAtten = 1;
		ShadowStrength = 1;
	#else
		#if defined(_MAIN_LIGHT_SHADOWS_SCREEN) && !defined(_SURFACE_TYPE_TRANSPARENT)
		float4 shadowCoord = ComputeScreenPos(TransformWorldToHClip(WorldPos));
		#else
		float4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
		#endif
		ShadowAtten = MainLightShadow(shadowCoord, WorldPos, Shadowmask, _MainLightOcclusionProbes);
		ShadowStrength = GetMainLightShadowStrength();
	#endif
}

void InvLerp_float(float from, float to, float value, out float output) {
	output = (value - from) / (to - from);
}

void MainLightShadows_float (float3 WorldPos, out float ShadowAtten,out float ShadowStrength){
	MainLightShadows_float(WorldPos, half4(1,1,1,1), ShadowAtten, ShadowStrength);
}

//------------------------------------------------------------------------------------------------------
// Shadowmask (v10+)
//------------------------------------------------------------------------------------------------------

/*
- Used to support "Shadowmask" mode in Lighting window.
- Should be sampled once in graph, then input into the Main Light Shadows and/or Additional Light subgraphs/functions.
- To work in an Unlit Graph, likely requires keywords :
	- Boolean Keyword, Global Multi-Compile "SHADOWS_SHADOWMASK" 
	- Boolean Keyword, Global Multi-Compile "LIGHTMAP_SHADOW_MIXING"
	- (also LIGHTMAP_ON, but I believe Shader Graph is already defining this one)
*/
void Shadowmask_half (float2 lightmapUV, out half4 Shadowmask){
	#ifdef SHADERGRAPH_PREVIEW
		Shadowmask = half4(1,1,1,1);
	#else
		OUTPUT_LIGHTMAP_UV(lightmapUV, unity_LightmapST, lightmapUV);
		Shadowmask = SAMPLE_SHADOWMASK(lightmapUV);
	#endif
}

//------------------------------------------------------------------------------------------------------
// Default Additional Lights
//------------------------------------------------------------------------------------------------------

/*
- Handles additional lights (e.g. additional directional, point, spotlights)
- For custom lighting, you may want to duplicate this and swap the LightingLambert / LightingSpecular functions out. See Toon Example below!
- To work in the Unlit Graph, the following keywords must be defined in the blackboard :
	- Boolean Keyword, Global Multi-Compile "_ADDITIONAL_LIGHT_SHADOWS"
	- Boolean Keyword, Global Multi-Compile "_ADDITIONAL_LIGHTS"
- To support Forward+ path,
	- Boolean Keyword, Global Multi-Compile "_FORWARD_PLUS" (2022.2+)
*/
void AdditionalLights_float(float3 WorldPosition, float3 WorldNormal, float3 WorldView, half4 Shadowmask, float2 SmoothstepMinMax, float3 Power, float Smoothness,
							out float3 Diffuse, out float3 Specular) {
	float3 diffuseColor = 0;
	float3 specularColor = 0;
#ifndef SHADERGRAPH_PREVIEW
	uint pixelLightCount = GetAdditionalLightsCount();
	uint meshRenderingLayers = GetMeshRenderingLayer();

	LIGHT_LOOP_BEGIN(pixelLightCount)
		Light light = GetAdditionalLight(lightIndex, WorldPosition, Shadowmask);
	#ifdef _LIGHT_LAYERS
		if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
	#endif
		{
			float ndl = saturate(dot(WorldNormal, light.direction));
			float3 lambert = smoothstep(SmoothstepMinMax.x,SmoothstepMinMax.y, ndl);
			lambert = pow(lambert, Power);
			lambert *= (light.distanceAttenuation * light.shadowAttenuation);
			lambert *= light.color;
			diffuseColor += lambert;
			float3 halfDir = normalize(WorldView + light.direction);
			float ndvl = saturate(dot(WorldNormal, halfDir));
			float3 spec = pow(ndvl, Smoothness * 50);
			spec *= (light.distanceAttenuation * light.shadowAttenuation);
			spec = smoothstep(0.4,0.6,spec);
			spec *= pow(Smoothness, 4);
			spec *= light.color;
			specularColor += spec;
		}
	LIGHT_LOOP_END
#endif

	Diffuse = diffuseColor;
	Specular = specularColor;
}

// For backwards compatibility (before Shadowmask was introduced)
void AdditionalLights_float(float3 WorldPosition, float3 WorldNormal, float3 WorldView, float2 SmoothstepMinMax,float3 Power, float Smoothness,
							out float3 Diffuse, out float3 Specular) {
AdditionalLights_float(WorldPosition, WorldNormal, WorldView, half4(1,1,1,1), SmoothstepMinMax, Power, Smoothness, Diffuse, Specular);
}

#endif // CUSTOM_LIGHTING_INCLUDED
