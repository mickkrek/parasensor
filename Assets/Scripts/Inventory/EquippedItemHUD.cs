using UnityEngine;
using UnityEngine.UI;

public class EquippedItemHUD : MonoBehaviour
{
    private Image previewRenderer;
    void Start()
    {
        previewRenderer = GetComponent<Image>();
        GameManager_Inventory.Instance.EquippedItemChanged.AddListener(UpdateDisplaySprite);
        UpdateDisplaySprite();
    }

    private void UpdateDisplaySprite()
    {
        if (GameManager_Inventory.Instance.EquippedItem != null)
        {
            previewRenderer.sprite = GameManager_Inventory.Instance.EquippedItem.descriptionImage;
        }
    }
}
