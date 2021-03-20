using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPingPongAlpha : BaseTweenPingPongLoop
{
    [Space]
    [SerializeField] private float alphaTo = 1f;
    public override void StartTweening()
    {
        if (easingType == LeanTweenType.animationCurve)
            _ltDescUniqID = LeanTween.alpha(gameObject, alphaTo, duration).setEase(animationCurve).setLoopPingPong(loopPingPong).uniqueId;
        else
            _ltDescUniqID = LeanTween.alpha(gameObject, alphaTo, duration).setEase(easingType).setLoopPingPong(loopPingPong).uniqueId;
    }
}
