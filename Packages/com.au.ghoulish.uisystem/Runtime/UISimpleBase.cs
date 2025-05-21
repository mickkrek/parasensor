using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ghoulish.UISystem
{
    public class UISimpleBase : MonoBehaviour
    {
        private UIView[] _childrenUIViews;
        [SerializeField] private float _disableTimeSeconds = 1f;
        private void Start()
        {
            if (_childrenUIViews == null)
            {
                _childrenUIViews = GetComponentsInChildren<UIView>();
                foreach (UIView child in _childrenUIViews)
                {
                    child.GatherAnimationData();
                }
            }
            TurnOn();
        }
        IEnumerator SetInactiveTimer()
        {
            foreach(UIView view in _childrenUIViews)
            {
                view.TaskOnDisabled();
            }
            yield return new WaitForSeconds(_disableTimeSeconds);
            gameObject.SetActive(false);
        }
        public void TurnOff()
        {
            if (_childrenUIViews != null && Application.isPlaying)
            {
                StartCoroutine(SetInactiveTimer());
            }
        }
        public void TurnOn()
        {
            if (_childrenUIViews != null && Application.isPlaying)
            {
                foreach(UIView view in _childrenUIViews)
                {
                    view.TaskOnDefault();
                }
            }
        }
    }
}
