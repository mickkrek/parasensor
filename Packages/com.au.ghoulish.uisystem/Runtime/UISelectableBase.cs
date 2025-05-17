using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ghoulish.UISystem
{
    public class UISelectableBase : Selectable, IDeselectHandler, IPointerClickHandler, ISubmitHandler
    {
        [SerializeField] bool UseHover, UseSelected, UseSubmit; //these dont do anything right now
        private SelectionState storedSelectionState = SelectionState.Disabled; //Set the default to 'selectable' state
        private UIView[] childrenUIViews;
        
    #region Unity Button
        public class ButtonClickedEvent : UnityEvent {}
        [SerializeField] private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();
        public ButtonClickedEvent onClick
        {
            get { return m_OnClick; }
            set { m_OnClick = value; }
        }

        private void Press()
        {
            if (!IsActive() || !IsInteractable())
                return;

            UISystemProfilerApi.AddMarker("Button.onClick", this);
            m_OnClick.Invoke();
        }
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            Press();
        }
        public virtual void OnSubmit(BaseEventData eventData)
        {
            Press();

            // if we get set disabled during the press
            // don't run the coroutine.
            if (!IsActive() || !IsInteractable())
                return;

            DoStateTransition(SelectionState.Pressed, false);
            StartCoroutine(OnFinishSubmit());
        }

        private IEnumerator OnFinishSubmit()
        {
            var fadeTime = colors.fadeDuration;
            var elapsedTime = 0f;

            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            DoStateTransition(currentSelectionState, false);
        }
    #endregion
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
                        OnDisabled();
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
