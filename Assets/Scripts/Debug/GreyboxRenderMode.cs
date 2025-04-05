using UnityEngine;

public class GreyboxRenderMode : MonoBehaviour
{
    [SerializeField] private Camera greyboxRenderCam, mainRenderCam;
    [SerializeField] private Transform enabledIcon;
    private bool GreyboxModeActive = false;
    public void ToggleGreyboxRenderMode()
    {
        GreyboxModeActive = !GreyboxModeActive;
        greyboxRenderCam.gameObject.SetActive(GreyboxModeActive);
        mainRenderCam.gameObject.SetActive(!GreyboxModeActive);
        enabledIcon.gameObject.SetActive(GreyboxModeActive);
    }
}
