using UnityEngine;
using Pixelplacement;
using Pixelplacement.TweenSystem;

namespace Ghoulish.UISystem
{
    public class UIView_Scale : UIView
    {
        public AnimationState normal, hover, selected, submit, disabled;
        public string ComponentName = "UI Image Scale";

        [System.Serializable]public class AnimationState
        {
            public float startScale, endScale;
            public float duration;
            public CurveTypes CurveType;
            public Tween.LoopType LoopType = Tween.LoopType.None;
        }
        protected override void Start()
        {
            base.Start();
            HandleTween(normal);
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
            if (animationState.LoopType != Tween.LoopType.None)
            {
                tween = Tween.LocalScale(transform, new Vector3(animationState.startScale,animationState.startScale,animationState.startScale),new Vector3(animationState.endScale,animationState.endScale,animationState.endScale), animationState.duration, 0.0f, animationCurves[(int)animationState.CurveType], animationState.LoopType);
            }
            else
            {
                tween = Tween.LocalScale(transform, new Vector3(animationState.endScale,animationState.endScale,animationState.endScale), animationState.duration, 0.0f, animationCurves[(int)animationState.CurveType], Tween.LoopType.None);
            }
        }
    }
}