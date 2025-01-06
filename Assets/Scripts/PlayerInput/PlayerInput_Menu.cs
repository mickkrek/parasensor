using Pixelplacement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Yarn.Unity;
public class PlayerInput_Menu : MonoBehaviour
{
    [SerializeField] private StateMachine stateMachine;
    private void OnOpenInventory()
    {
        if (stateMachine.currentState.name != "Inventory")
        {
            stateMachine.ChangeState("Inventory");
            //GameManager.Instance._characterMovementEnabled = false;
        }
        else
        {
            stateMachine.ChangeState("Game");
            //GameManager.Instance._characterMovementEnabled = true;
        }
    }
    /*
    void OnCancel()
    {
        DialogueRunner runner = GameObject.Find("Yarn System").GetComponent<DialogueRunner>();
        CanvasGroup optionListView = GameObject.Find("OptionBoxContainer").GetComponent<CanvasGroup>();
        if(!GameManager_GUI.Instance.PauseOpen)
        {
            if (GameManager_GUI.Instance.JournalOpen)
            {
                for(int i=0; i < _journalObjects.Length;i++)
                {
                    _journalObjects[i].gameObject.SetActive(false);
                }
                GameManager.Instance._characterMovementEnabled = true;
                GameManager_GUI.Instance.OpenInventoryIcon.gameObject.SetActive(true);
                GameManager_GUI.Instance.CloseInventoryIcon.gameObject.SetActive(false);
                GameManager_GUI.Instance.JournalOpen = false;
                return;
            }
            else if (GameManager_GUI.Instance.QuestioningOpen)
            {
                CloseQuestioning();
                runner.Stop();
                optionListView.alpha = 0f;
                runner.StartDialogue("Generic_Nevermind");
                GameManager_GUI.Instance.OpenInventoryIcon.gameObject.SetActive(true);
                GameManager_GUI.Instance.CloseInventoryIcon.gameObject.SetActive(false);
            }
            else if (runner.IsDialogueRunning)
            {
                runner.Stop();
                optionListView.alpha = 0f;
                runner.StartDialogue("Generic_Nevermind");
            }
        }
    }
    */
    public void OnPause(InputValue value)
    {
        if (stateMachine.currentState.name != "Pause")
        {
            stateMachine.ChangeState("Pause");
            //GameManager.Instance._characterMovementEnabled = false;
        }
        else
        {
            stateMachine.ChangeState("Game");
            //GameManager.Instance._characterMovementEnabled = true;
        }
    }
    public void ResumeGame()
    {
        stateMachine.ChangeState("Game");
        //GameManager.Instance._characterMovementEnabled = true;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
