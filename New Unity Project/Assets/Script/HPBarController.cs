using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    [SerializeField] private Slider hpBar = null;
    public Slider HpBar => hpBar;
    [SerializeField] private Slider hpBarLinger = null;
    public Slider HpBarLinger => hpBarLinger;

    [SerializeField] private float HpLingerDelay = 0.5f;
    [SerializeField] private float HpLingerDuration = 1f;

    private int _ltLinger = 0;

    public void AnimateHP(float HpFrom, float HpTo)
    {
        SetHpBar(HpTo);

        _ltLinger = LeanTween.delayedCall(HpLingerDelay, () => {

            _ltLinger = LeanTween.value(hpBarLinger.gameObject, (float x) => {

                hpBarLinger.value = x;

            }, HpFrom, HpTo, HpLingerDuration).setOnComplete(() => {

                SetHpBarLinger(HpTo);
                _ltLinger = 0;

            }).uniqueId;

        }).uniqueId;
    }

    public void SetHpBar(float value)
    {
        hpBar.value = value;
    }

    public void SetHpBarLinger(float value)
    {
        hpBarLinger.value = value;
    }
}
