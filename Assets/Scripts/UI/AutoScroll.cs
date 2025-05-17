using Ghoulish.UISystem;
using Pixelplacement;
using Pixelplacement.TweenSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollBar;
    [SerializeField] private Transform _contentParent;
    private TweenBase _tween;
    private GameObject _storedSelected;

    void Update ()
    {
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        if (currentSelected == null) return;

        if (currentSelected == _storedSelected) return;
        if (currentSelected.transform.IsChildOf(_contentParent))
        {
            if (currentSelected.GetComponent<UISelectableBase>() != null) //if the current selected object is a button just scroll to the bottom
            {
                HandleTween(_contentParent.childCount, _contentParent.childCount);
            }
            HandleTween(currentSelected.transform.GetSiblingIndex(), _contentParent.childCount);
        }
        _storedSelected = currentSelected;
    }
    public void HandleTween(int target, int qty)
    {
        float targetValue = 1-(float)target / (float)(qty - 1);
        _tween = Tween.Value(_scrollBar.value, targetValue, SetScrollbar, 0.5f, 0.0f, Tween.EaseOut, Tween.LoopType.None);
    }
    private void SetScrollbar(float value)
    {
        _scrollBar.value = value;
    }
}
