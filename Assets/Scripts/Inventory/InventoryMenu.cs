using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryMenu : MonoBehaviour
{
    public Button DefaultSelected;
    private void OnEnable()
    {
        GameObject.Find("GUI_NewJournalPrompt").GetComponent<GUI_NewItemPrompt>().DisablePrompt();
        StartCoroutine(HighlightBtn());
    }
    IEnumerator HighlightBtn()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
        EventSystem.current.SetSelectedGameObject(DefaultSelected.gameObject);
    }
}
