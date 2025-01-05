using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCategory : MonoBehaviour, ISelectHandler
{
    [SerializeField] private Transform SlotsParentToEnable;
    [SerializeField] private Transform SlotsParentToDisable;

    public void OnSelect(BaseEventData eventData)
    {
        SlotsParentToEnable.gameObject.SetActive(true);
        SlotsParentToDisable.gameObject.SetActive(false);
    }
}