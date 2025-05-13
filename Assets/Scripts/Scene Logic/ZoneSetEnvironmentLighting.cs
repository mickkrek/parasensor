using UnityEngine;

[ExecuteInEditMode]
public class ZoneSetEnvironmentLighting : MonoBehaviour
{
    [SerializeField] [ColorUsage(true, true)] Color skyColor,equatorColor,groundColor;
    void OnValidate()
    {
        if (gameObject.activeInHierarchy)
        {
            SetColors();
        }
    }
    void OnEnable()
    {
        SetColors();
    }
    private void SetColors()
    {
        RenderSettings.ambientGroundColor = groundColor;
        RenderSettings.ambientEquatorColor = equatorColor;
        RenderSettings.ambientSkyColor = skyColor;
    }
}
