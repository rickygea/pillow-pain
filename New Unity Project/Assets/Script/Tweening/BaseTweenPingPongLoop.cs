using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTweenPingPongLoop : MonoBehaviour
{
    [SerializeField] protected float duration = 1f;

    [Tooltip("Ping Pong Amount, 0 means Infinite")]
    [SerializeField] protected int loopPingPong = 0;

    [SerializeField] protected LeanTweenType easingType = LeanTweenType.notUsed;
    [Tooltip("Animation Curve only if Easing Type is set to Animation Curve")]
    [SerializeField] protected AnimationCurve animationCurve = new AnimationCurve();

    protected int _ltDescUniqID = 0;

    protected virtual void OnEnable()
    {
        CancelTweening();

        StartTweening();
    }

    //protected virtual void OnDisable()
    //{
    //    CancelTweening();
    //}

    public abstract void StartTweening();


    public virtual void CancelTweening()
    {
        if (_ltDescUniqID != 0)
        {
            LeanTween.cancel(_ltDescUniqID);
            _ltDescUniqID = 0;
        }
    }
}
