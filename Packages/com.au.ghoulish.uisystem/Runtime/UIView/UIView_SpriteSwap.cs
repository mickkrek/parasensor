using UnityEngine;
using UnityEngine.UI;
using Pixelplacement;

namespace Ghoulish.UISystem
{
    public class UIView_SpriteSwap : UIView
    {
        [SerializeField] private AnimationState normal, hover, selected, submit;
        private Image imageRenderer;
        public string ComponentName = "UI Sprite Swap";
        [System.Serializable]private class AnimationState
        {
            public Sprite sprite;
        }

        private void Awake()
        {
            imageRenderer = GetComponent<Image>();
        }

        public override void TaskOnHover()
        {
            base.TaskOnHover();
            if (hover.sprite != null)
            {
                imageRenderer.sprite = hover.sprite;
            }
        }
        public override void TaskOnDefault()
        {
            base.TaskOnDefault();
            if (normal.sprite != null)
            {
                imageRenderer.sprite = normal.sprite;
            }
        }
        public override void TaskOnSelect()
        {
            base.TaskOnSelect();
            if (selected.sprite != null)
            {
                imageRenderer.sprite = selected.sprite;
            }
        }
        public override void TaskOnSubmit()
        {
            base.TaskOnSubmit();
            if (submit.sprite != null)
            {
                imageRenderer.sprite = submit.sprite;
            }
        }
    }
}