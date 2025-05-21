using Pixelplacement;
using UnityEngine;
using UnityEngine.UI;

namespace Ghoulish.UISystem
{
    public class UIView_Color : UIView
    {
        [SerializeField] private AnimationState normal, hover, selected, submit, disabled;
        private Image _imageRenderer;
        public string ComponentName = "UI Image Color";
        [System.Serializable]
        private class AnimationState
        {
            public bool useStartAlpha = false;
            public Color startColor = Color.white, endColor = Color.white;
            public float duration;
            public CurveTypes CurveType;
        }

        private void Awake()
        {
            _imageRenderer = GetComponent<Image>();
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
            if (animationState.useStartAlpha)
            {
                tween = Tween.Color(_imageRenderer, animationState.startColor, animationState.endColor, animationState.duration, 0.0f, animationCurves[(int)animationState.CurveType], Tween.LoopType.None, null, null, true);
            }
            else
            {
                tween = Tween.Color(_imageRenderer, animationState.endColor, animationState.duration, 0.0f, animationCurves[(int)animationState.CurveType], Tween.LoopType.None, null, null, true);
            }
        }
    }
}