using UnityEngine;
using Pixelplacement;
using System;

namespace Ghoulish.UISystem
{
    public class UIView_Rotation : UIView
    {
        [SerializeField] private AnimationState normal, hover, selected, submit, disabled;
        public string ComponentName = "UI Image Rotation";

        [System.Serializable]private class AnimationState
        {
            public float startRotation, endRotation;
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
                tween = Tween.LocalRotation(transform, new Vector3(0.0f,0.0f,animationState.startRotation),new Vector3(0.0f,0.0f,animationState.endRotation), animationState.duration, 0.0f, animationCurves[(int)animationState.CurveType], animationState.LoopType);
            }
            else
            {
                tween = Tween.LocalRotation(transform, new Vector3(0.0f,0.0f,animationState.endRotation), animationState.duration, 0.0f, animationCurves[(int)animationState.CurveType], Tween.LoopType.None);
            }
        }
    }
}