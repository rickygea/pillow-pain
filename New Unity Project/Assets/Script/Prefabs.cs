using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Prefabs : MonoBehaviour
{
    [Header("Player")]
    public GameObject player1;
    public GameObject player2;
    [Header("Jenis move")]
    public GameObject ha1;
    public GameObject ha2, block1, block2, low1, low2;
    [Header("Health Bar")]
    public HPBarController p1hp;
    public HPBarController p2hp;
    [Header("Other UI")]
    public GameObject teks;
    public Slider timer, sp;
    public Button tblha, tbllow, tblblock;
    public GameObject notblha, notbllow, notblbloack, doubledamage, tblskill;
    public Slider winp1, winp2;
       
}
