using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPingPongLocalMove : BaseTweenPingPongLoop
{
    [Space]
    [SerializeField] private Vector3 moveTo = Vector3.zero;

    public override void StartTweening()
    {
        if (easingType == LeanTweenType.animationCurve)
            _ltDescUniqID = LeanTween.moveLocal(gameObject, moveTo, duration).setEase(animationCurve).setLoopPingPong(loopPingPong).uniqueId;
        else
            _ltDescUniqID = LeanTween.moveLocal(gameObject, moveTo, duration).setEase(easingType).setLoopPingPong(loopPingPong).uniqueId;
    }
}
