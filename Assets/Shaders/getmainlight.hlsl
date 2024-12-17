void GetMainLight_float
(
	float3 _worldPos,
	out float3 _color, out float3 _direction, out float _attenuation, out float _shadowStrength
)
{
	_color = float3(0, 0, 0);
	_direction = float3(0, 0, 0);
	_attenuation = 0;
	_shadowStrength = 0;

#ifdef SHADERGRAPH_PREVIEW
		_color = float3(0, 0, 0);
		_direction = float3(0, 0, 0);
		_attenuation = 0;
#else
#if SHADOWS_SCREEN
			half4 clipPos = TransformWorldToHClip(_worldPos);
			half4 shadowCoord = ComputeScreenPos(clipPos);
#else
			//Get Shadow Cascades & Shadow coordinates
	half cascadeIndex = ComputeCascadeIndex(_worldPos);
	float4 shadowCoord = TransformWorldToShadowCoord(_worldPos);
			//float4 shadowCoord = mul(_MainLightWorldToShadow[cascadeIndex], float4(_worldPos, 1.0));
#endif
	
	Light mainLight = GetMainLight(shadowCoord);

	_direction = mainLight.direction;
	_color = mainLight.color;
	_attenuation = saturate(mainLight.shadowAttenuation * mainLight.distanceAttenuation);
	_shadowStrength = GetMainLightShadowStrength();
#endif
}

void AdditionalLights_float(half3 WorldPosition, half3 WorldNormal, out half3 AccumulatedColors)
{
	AccumulatedColors = 0;

	#ifndef SHADERGRAPH_PREVIEW
		WorldNormal = normalize(WorldNormal);
		uint numAdditionalLights = GetAdditionalLightsCount();
		for (uint lightI = 0; lightI < numAdditionalLights; lightI++)
		{
			Light light = GetAdditionalLight(lightI, WorldPosition, 1);
			
			half ndl = dot(WorldNormal, light.direction);
			//ndl = smoothstep(0.3,0.7,ndl);
			half3 ramp;

			float3 atten = (light.distanceAttenuation * light.shadowAttenuation);
			//atten = pow(atten, float3(1,1.1,1.2));
			
			ndl = saturate(ndl);
			ndl *= atten;
			
			AccumulatedColors += ndl * light.color.rgb;
		}
	#endif
}