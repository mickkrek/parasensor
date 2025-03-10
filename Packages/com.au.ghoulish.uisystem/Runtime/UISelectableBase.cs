using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ghoulish.UISystem
{
    public class UISelectableBase : Selectable, IDeselectHandler
    {
        [SerializeField] bool UseHover, UseSelected, UseSubmit;
        private SelectionState storedSelectionState = SelectionState.Normal; //Set the default to 'selectable' state
        private UIView[] childrenUIViews;


        override protected void Start()
        {
            base.Start();
            transition = Transition.None;
            childrenUIViews = GetComponentsInChildren<UIView>();
         }
        void Update()
        {
            if (storedSelectionState != currentSelectionState)
            {
                ChangeState();
            }
        }
        public override void OnDeselect(BaseEventData data)
        {
            base.OnDeselect(data);
            ChangeState();
        }
        public void ToggleInteractable(bool isInteractable)
        {
            StartCoroutine(DelayedInteractable(this, isInteractable));
        }
        IEnumerator DelayedInteractable (UISelectableBase target, bool isInteractable)
        {
            yield return new WaitForEndOfFrame();
            target.interactable = isInteractable;
        }
        private void ChangeState()
        {
            if (childrenUIViews != null && Application.isPlaying)
            {
                switch(currentSelectionState)
                {
                    case SelectionState.Highlighted:
                        OnHover();
                        break;
                    case SelectionState.Selected:
                        OnSelect();
                        break;
                    case SelectionState.Pressed:
                        OnSubmit();
                        break;
                    case SelectionState.Disabled:
                        OnDisabled();
                        break;
                    case SelectionState.Normal:
                        OnDefault();
                        break;
                    default:
                        OnDefault();
                        break;
                }
            }
            storedSelectionState = currentSelectionState;
        }
        private void OnSubmit()
        {
            foreach(UIView view in childrenUIViews)
            {
                view.TaskOnSubmit();
            }
        }
        private void OnSelect()
        {
            foreach(UIView view in childrenUIViews)
            {
                view.TaskOnSelect();
            }
        }
        private void OnHover()
        {
            foreach(UIView view in childrenUIViews)
            {
                view.TaskOnHover();
            }
        }
        private void OnDisabled()
        {
            
            foreach(UIView view in childrenUIViews)
            {
                view.TaskOnDisabled();
            }
        }
        private void OnDefault()
        {
            foreach(UIView view in childrenUIViews)
            {
                view.TaskOnDefault();
            }
        } 
    }
}
