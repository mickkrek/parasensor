using UnityEngine;
using Pixelplacement;

public class GUI_NewItemPrompt : MonoBehaviour
{
    [SerializeField] private CanvasGroup _promptPopup;
    public void TriggerPrompt()
    {
        _promptPopup.transform.parent.gameObject.SetActive(true);
        _promptPopup.transform.localPosition = new Vector3(_promptPopup.transform.localPosition.x, -30f, _promptPopup.transform.localPosition.z);
        _promptPopup.alpha = 0f;
        Tween.CanvasGroupAlpha(_promptPopup, 1f,0.5f, 0f, Tween.EaseLinear);
        Tween.LocalPosition(_promptPopup.gameObject.transform, Vector3.zero, 0.5f, 0f, Tween.EaseBounce);
    }
    public void DisablePrompt()
    {
        _promptPopup.transform.parent.gameObject.SetActive(false);
    }
}
