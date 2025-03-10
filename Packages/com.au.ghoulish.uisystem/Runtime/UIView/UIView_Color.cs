using Pixelplacement;
using UnityEngine;
using UnityEngine.UI;

namespace Ghoulish.UISystem
{
    public class UIView_Color : UIView
    {
        [SerializeField] private AnimationState normal, hover, selected, submit, disabled;
        private Image imageRenderer;
        public string ComponentName = "UI Image Color";
        [System.Serializable]private class AnimationState
        {
            public Color color;
            public float duration;
            public CurveTypes CurveType;
        }

        protected override void Start()
        {
            base.Start();
            imageRenderer = GetComponent<Image>();
            imageRenderer.color = normal.color;
        }
        public override void TaskOnHover()
        {
            base.TaskOnHover();
            tween = Tween.Color(imageRenderer, hover.color, hover.duration, 0.0f, animationCurves[(int)hover.CurveType]);
        }
        public override void TaskOnDefault()
        {
            base.TaskOnDefault();
            tween = Tween.Color(imageRenderer, normal.color, normal.duration, 0.0f, animationCurves[(int)normal.CurveType]);
        }
        public override void TaskOnSelect()
        {
            base.TaskOnSelect();
            tween = Tween.Color(imageRenderer, selected.color, selected.duration, 0.0f, animationCurves[(int)selected.CurveType]);
        }
        public override void TaskOnSubmit()
        {
            base.TaskOnSubmit();
            tween = Tween.Color(imageRenderer, submit.color, submit.duration, 0.0f, animationCurves[(int)submit.CurveType]);
        }
        public override void TaskOnDisabled()
        {
            base.TaskOnDisabled();
            tween = Tween.Color(imageRenderer, disabled.color, disabled.duration, 0.0f, animationCurves[(int)disabled.CurveType]);
        }
    }
}