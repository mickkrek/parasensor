using UnityEngine;
using UnityEngine.UI;
using Pixelplacement;

namespace Ghoulish.UISystem
{
    public class UIView_Spritesheet : UIView
    {
        [SerializeField] private AnimationState normal, hover, selected, submit;
        private Image imageRenderer;
        public string ComponentName = "UI Spritesheet Animation";
        private AnimationState currentState;
        [System.Serializable]private class AnimationState
        {
            public bool animate = false;
            public Sprite[] sprites;
            public CurveTypes CurveType = CurveTypes._easeLinear;
            public Tween.LoopType LoopType = Tween.LoopType.Loop;
            public float duration = 1;
        }

        protected override void Start()
        {
            base.Start();
            imageRenderer = GetComponent<Image>();
        }

        public override void TaskOnHover()
        {
            base.TaskOnHover();
            if (hover.animate)
            {
                HandleTween(hover);
            }
            else
            {
                imageRenderer.sprite = hover.sprites[0];
            }
        }
        public override void TaskOnDefault()
        {
            base.TaskOnDefault();
            if (normal.animate)
            {
                HandleTween(normal);
            }
            else
            {
                imageRenderer.sprite = normal.sprites[0];
            }
        }
        public override void TaskOnSelect()
        {
            base.TaskOnSelect();
            if (selected.animate)
            {
                HandleTween(selected);
            }
            else
            {
                imageRenderer.sprite = selected.sprites[0];
            }
        }
        public override void TaskOnSubmit()
        {
            base.TaskOnSubmit();
            if (submit.animate)
            {
                HandleTween(submit);
            }
            else
            {
                imageRenderer.sprite = submit.sprites[0];
            }
        }
        void HandleFrameChange (float value)
        {
            imageRenderer.sprite = currentState.sprites[(int) value];
        }
        private void HandleTween(AnimationState animationState)
        {
            //TODO: Looping is not working. This package seems to be way overcomplicated for tweening a single float.
            Tween.Value(0f, animationState.sprites.Length, HandleFrameChange, animationState.duration, 0.0f, animationCurves[(int)animationState.CurveType], animationState.LoopType);
            currentState = animationState;
        }
    }
}