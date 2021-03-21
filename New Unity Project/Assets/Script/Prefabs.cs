using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Prefabs : MonoBehaviour
{
    [Header("Player")]
    public GameObject player1;
    public GameObject player2;
    [Header("Health Bar")]
    public HPBarController p1hp;
    public HPBarController p2hp;
    [Header("Other")]
    public GameObject uipause;
    public GameObject kamera;
    public GameObject Canvas;
    public GameObject teks;
    public CanvasGroup beginningCG = null;
    public TextMeshProUGUI beginningText = null;
    public Image Timerfill;
    public Slider  sp;
    public GameObject attackButtons = null;
    public Button tblha, tbllow, tblblock;
    public GameObject notblha, notbllow, notblbloack, doubledamage, tblskill;
    public GameObject[] wincount1, wincount2;
    public CanvasGroup winPanel, losePanel = null;
}
