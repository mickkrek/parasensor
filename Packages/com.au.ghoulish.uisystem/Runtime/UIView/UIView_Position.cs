using UnityEngine;
using Pixelplacement;

namespace Ghoulish.UISystem
{
    public class UIView_Position : UIView
    {
        [SerializeField] private AnimationState normal, hover, selected, submit, disabled;
        public string ComponentName = "UI RectTransform Position";
        private RectTransform thisRect;

        [System.Serializable] private class AnimationState
        {
            public bool useStartPosition = false;
            public Vector2 startPosition, endPosition;
            public float duration;
            public CurveTypes CurveType;
            public Tween.LoopType LoopType = Tween.LoopType.None;
        }
        private void Awake()
        {
            if (thisRect == null)
            {
                thisRect = GetComponent<RectTransform>();
            }
        }
        public override void TaskOnHover()
        {
            base.TaskOnHover();
            HandleTween(hover);
        }
        public override void TaskOnDefault()
        {
            base.TaskOnDefault();
            HandleTween(normal);
        }
        public override void TaskOnSelect()
        {
            base.TaskOnSelect();
            HandleTween(selected);
        }
        public override void TaskOnSubmit()
        {
            base.TaskOnSubmit();
            HandleTween(submit);
        }
        public override void TaskOnDisabled()
        {
            base.TaskOnDisabled();
            HandleTween(disabled);
        }

        private void HandleTween (AnimationState animationState)
        {
            if (animationState.useStartPosition)
            {
                tween = Tween.AnchoredPosition(thisRect, animationState.startPosition, animationState.endPosition, animationState.duration, 0.0f, animationCurves[(int)animationState.CurveType], animationState.LoopType, null, null, true);
            }
            else
            {
                tween = Tween.AnchoredPosition(thisRect, animationState.endPosition, animationState.duration, 0.0f, animationCurves[(int)animationState.CurveType], animationState.LoopType);
            }
        }
    }
}