using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ultimateefects : MonoBehaviour
{
    GameManager gamemanager;
    public CameraShake shakescript;
    public int code;
    public ParticleSystem hiteffect, asap;
    private void Start()
    {
        gamemanager = GameManager.sgtgamemanager;
    }

    void shake() 
    {
        switch (code)
        {
            case 1:
                if (gamemanager.player1.ultimate)
                {
                    shakescript.enabled = true;
                    if (gamemanager.maupakaiparticle)
                    {
                        hiteffect.Play();
                    }
                }
                else
                {
                    if (gamemanager.maupakaiparticle)
                    {
                        asap.Play();
                    }
                }
                break;
            case 2:
                if (gamemanager.player2.ultimate)
                {

                    shakescript.enabled = true;
                    if (gamemanager.maupakaiparticle)
                    {
                        hiteffect.Play();
                    }
                }
                else 
                {
                    if (gamemanager.maupakaiparticle)
                    {
                        asap.Play();
                    }
                }
                break;
            default:
                break;
        }
        
      
    }
}
