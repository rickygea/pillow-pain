using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPingPongRotate : BaseTweenPingPongLoop
{
    [Space]
    [SerializeField] private Vector3 rotateTo = Vector3.zero;

    public override void StartTweening()
    {
        if (easingType == LeanTweenType.animationCurve)
            _ltDescUniqID = LeanTween.rotateLocal(gameObject, rotateTo, duration).setEase(animationCurve).setLoopPingPong(loopPingPong).uniqueId;
        else
            _ltDescUniqID = LeanTween.rotateLocal(gameObject, rotateTo, duration).setEase(easingType).setLoopPingPong(loopPingPong).uniqueId;
    }
}
