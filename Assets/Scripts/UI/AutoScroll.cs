using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollBar;
    [SerializeField] private Transform contentParent;

    void Update ()
    {
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        if (currentSelected == null) return;
        if (currentSelected.transform.IsChildOf(contentParent))
        {
            SetScrollbar(scrollBar, currentSelected.transform.GetSiblingIndex(), contentParent.childCount);
        }
    }
    public void SetScrollbar(Scrollbar scrollbar, int target, int qty)
    {
        scrollbar.value = 1-(float)target / (float)(qty - 1);
    }
}
