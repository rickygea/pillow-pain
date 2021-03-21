using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ultimateefects : MonoBehaviour
{
    GameManager gamemanager;
    public CameraShake shakescript;
    public int code;
    private void Start()
    {
        gamemanager = GameManager.sgtgamemanager;
    }

    void shake() 
    {
        if (gamemanager.player1.ultimate && code == 1)
        {
            shakescript.enabled = true;
        }
        if(gamemanager.player2.ultimate && code ==2)
        {
            shakescript.enabled = true;
        }
    }
}
