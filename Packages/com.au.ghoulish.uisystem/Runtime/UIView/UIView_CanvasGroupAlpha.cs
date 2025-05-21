using System;
using Pixelplacement;
using UnityEngine;
using UnityEngine.UI;

namespace Ghoulish.UISystem
{
    public class UIView_CanvasGroupAlpha : UIView
    {
        [SerializeField] private AnimationState normal, disabled;
        private CanvasGroup _canvasGroup;
        public string ComponentName = "UI Canvas Group Alpha";
        [System.Serializable]
        private class AnimationState
        {
            public bool useStartAlpha = false;
            public float startAlpha, endAlpha;
            public float duration;
            public CurveTypes CurveType;
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        public override void TaskOnDefault()
        {
            base.TaskOnDefault();
            HandleTween(normal);
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
                tween = Tween.CanvasGroupAlpha(_canvasGroup, animationState.startAlpha, animationState.endAlpha, animationState.duration, 0.0f, animationCurves[(int)animationState.CurveType], Tween.LoopType.None, null, null, true);
            }
            else
            {
                tween = Tween.CanvasGroupAlpha(_canvasGroup, animationState.endAlpha, animationState.duration, 0.0f, animationCurves[(int)animationState.CurveType], Tween.LoopType.None, null, null, true);
            }
        }
    }
}