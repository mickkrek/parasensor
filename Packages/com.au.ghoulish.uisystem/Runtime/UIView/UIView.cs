using Pixelplacement;
using Pixelplacement.TweenSystem;
using UnityEngine;

namespace Ghoulish.UISystem
{
    public abstract class UIView : MonoBehaviour
    {
        [HideInInspector] public enum CurveTypes
        {
            _easeIn,
            _easeInStrong,
            _easeOut,
            _easeOutStrong,
            _easeInOut,
            _easeInOutStrong,
            _easeInBack,
            _easeOutBack,
            _easeInOutBack,
            _easeSpring,
            _easeBounce,
            _easeWobble,
            _easeLinear
        }
        [HideInInspector] public AnimationCurve[] animationCurves;
        [HideInInspector] public TweenBase tween;

        public void GatherAnimationData()
        {
            animationCurves = new AnimationCurve[13];
            animationCurves[0] = Tween.EaseIn;
            animationCurves[1] = Tween.EaseInStrong;
            animationCurves[2] = Tween.EaseOut;
            animationCurves[3] = Tween.EaseOutStrong;
            animationCurves[4] = Tween.EaseInOut;
            animationCurves[5] = Tween.EaseInOutStrong;
            animationCurves[6] = Tween.EaseInBack;
            animationCurves[7] = Tween.EaseOutBack;
            animationCurves[8] = Tween.EaseInOutBack;
            animationCurves[9] = Tween.EaseSpring;
            animationCurves[10] = Tween.EaseBounce;
            animationCurves[11] = Tween.EaseWobble;
            animationCurves[12] = Tween.EaseLinear;
        }
        
    #region Virtual Tasks
        public virtual void TaskOnSubmit()
        {
        }
        public virtual void TaskOnSelect()
        {
        }
        public virtual void TaskOnHover()
        {
        }
        public virtual void TaskOnDisabled()
        {
        }
        public virtual void TaskOnDefault()
        {
        }
    #endregion
        
    }
}
