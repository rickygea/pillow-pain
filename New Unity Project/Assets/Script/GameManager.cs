using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool maupakaiparticle;
    public static GameManager sgtgamemanager;
    private void Awake()
    {
        if (sgtgamemanager == null)
        {
            sgtgamemanager = this;
        }
    }
    [System.Serializable]
    public class playerstat 
    {
        public int hp;
        public int sp;
       public int move;
        public bool ultimate;
    }
    public CameraShake shakescript;
    public Prefabs prefabs;
    [Header("TimeSetting")]
    public float timer;
    public float beforefighttime, afterfighttime, afterprepare, waktuskill, waitforanimationdamage;
    [Header("Damages")]
    public int highdmg;
    public int lowdmg;
    [Header("PlayerSetting")]
    public int starthealth;
    public int maxsp;
    public playerstat player1, player2;

    private bool infight;
    private int[,,,] arrangkakalkulasi;
    GameObject lastpress, nowpress;
    Coroutine coroutine;
    Animator anip1, anip2, anicanvas;
    private float time;
    string tekan;
    void Start()
    {
        refreshcount();
        prefabs.sp.maxValue = maxsp;
        player1 = new playerstat();
        player2 = new playerstat();
        player1.hp = starthealth;
        player2.hp = starthealth;
        time = timer;
        prefabs.p1hp.HpBar.maxValue = starthealth;
        prefabs.p1hp.HpBarLinger.maxValue = starthealth;
        prefabs.p1hp.SetHpBar(starthealth);
        prefabs.p2hp.HpBar.maxValue = starthealth;
        prefabs.p2hp.HpBarLinger.maxValue = starthealth;
        prefabs.p2hp.SetHpBar(starthealth);

        player1.sp = 0;
        player2.sp = 0;
        inisialisasitabeldamage();
        prefabs.sp.maxValue = maxsp;
        anip1 = prefabs.player1.GetComponent<Animator>();
        anip2 = prefabs.player2.GetComponent<Animator>();
        anicanvas = prefabs.Canvas.GetComponent<Animator>();
        reset();

        StartCoroutine(StartBattle());

        AudioManager.Instance.PlayAudio("Battle");
    }

    private IEnumerator StartBattle()
    {
        infight = true;
        prefabs.beginningText.text = "Ready";

        yield return new WaitForSeconds(1f);
        prefabs.beginningText.text = "Fight!";

        LeanTween.alphaCanvas(prefabs.beginningCG, 0, 1f).setOnComplete(() => {
            prefabs.beginningCG.gameObject.SetActive(false);
        }).setEaseInQuart();

        yield return new WaitForSeconds(1f);
        infight = false;
    }

    void inisialisasitabeldamage() 
    {

        arrangkakalkulasi = new int[4,4,4,4];
        #region kasus0
        arrangkakalkulasi[0, 0, 0, 0] = 10;
        arrangkakalkulasi[0, 0, 0, 1] = 25;
        arrangkakalkulasi[0, 0, 0, 2] = 10;
        arrangkakalkulasi[0, 0, 0, 3] = 25;

        arrangkakalkulasi[0, 0, 1, 0] = 20;
        arrangkakalkulasi[0, 0, 1, 1] = 100;
        arrangkakalkulasi[0, 0, 1, 2] = 10;
        arrangkakalkulasi[0, 0, 1, 3] = 50;

        arrangkakalkulasi[0, 0, 2, 0] = 0;
        arrangkakalkulasi[0, 0, 2, 1] = 50;
        arrangkakalkulasi[0, 0, 2, 2] = 5;
        arrangkakalkulasi[0, 0, 2, 3] = 100;

        arrangkakalkulasi[0, 0, 3, 0] = 0;
        arrangkakalkulasi[0, 0, 3, 1] = 50;
        arrangkakalkulasi[0, 0, 3, 2] = 20;
        arrangkakalkulasi[0, 0, 3, 3] = 100;


        arrangkakalkulasi[0, 1, 0, 0] = 10;
        arrangkakalkulasi[0, 1, 0, 1] = 50;
        arrangkakalkulasi[0, 1, 0, 2] = 20;
        arrangkakalkulasi[0, 1, 0, 3] = 100;

        arrangkakalkulasi[0, 1, 1, 0] = 0;
        arrangkakalkulasi[0, 1, 1, 1] = 25;
        arrangkakalkulasi[0, 1, 1, 2] = 0;
        arrangkakalkulasi[0, 1, 1, 3] = 25;

        arrangkakalkulasi[0, 1, 2, 0] = 20;
        arrangkakalkulasi[0, 1, 2, 1] = 100;
        arrangkakalkulasi[0, 1, 2, 2] = 0;
        arrangkakalkulasi[0, 1, 2, 3] = 50;

        arrangkakalkulasi[0, 1, 3, 0] = 0;
        arrangkakalkulasi[0, 1, 3, 1] = 25;
        arrangkakalkulasi[0, 1, 3, 2] = 40;
        arrangkakalkulasi[0, 1, 3, 3] = 150;


        arrangkakalkulasi[0, 2, 0, 0] = 5;
        arrangkakalkulasi[0, 2, 0, 1] = 100;
        arrangkakalkulasi[0, 2, 0, 2] = 0;
        arrangkakalkulasi[0, 2, 0, 3] = 50;

        arrangkakalkulasi[0, 2, 1, 0] = 0;
        arrangkakalkulasi[0, 2, 1, 1] = 50;
        arrangkakalkulasi[0, 2, 1, 2] = 20;
        arrangkakalkulasi[0, 2, 1, 3] = 100;

        arrangkakalkulasi[0, 2, 2, 0] = 0;
        arrangkakalkulasi[0, 2, 2, 1] = 25;
        arrangkakalkulasi[0, 2, 2, 2] = 0;
        arrangkakalkulasi[0, 2, 2, 3] = 25;

        arrangkakalkulasi[0, 2, 3, 0] = 0;
        arrangkakalkulasi[0, 2, 3, 1] = 0;
        arrangkakalkulasi[0, 2, 3, 2] = 0;
        arrangkakalkulasi[0, 2, 3, 3] = 0;


        arrangkakalkulasi[0, 3, 0, 0] = 20;
        arrangkakalkulasi[0, 3, 0, 1] = 100;
        arrangkakalkulasi[0, 3, 0, 2] = 0;
        arrangkakalkulasi[0, 3, 0, 3] = 50;

        arrangkakalkulasi[0, 3, 1, 0] = 40;
        arrangkakalkulasi[0, 3, 1, 1] = 150;
        arrangkakalkulasi[0, 3, 1, 2] = 0;
        arrangkakalkulasi[0, 3, 1, 3] = 25;

        arrangkakalkulasi[0, 3, 2, 0] = 0;
        arrangkakalkulasi[0, 3, 2, 1] = 0;
        arrangkakalkulasi[0, 3, 2, 2] = 0;
        arrangkakalkulasi[0, 3, 2, 3] = 0;

        arrangkakalkulasi[0, 3, 3, 0] = 0;
        arrangkakalkulasi[0, 3, 3, 1] = 0;
        arrangkakalkulasi[0, 3, 3, 2] = 0;
        arrangkakalkulasi[0, 3, 3, 3] = 0;

        #endregion

        #region kasus1
        arrangkakalkulasi[1, 0, 0, 0] = 20;
        arrangkakalkulasi[1, 0, 0, 1] = -100;
        arrangkakalkulasi[1, 0, 0, 2] = 20;
        arrangkakalkulasi[1, 0, 0, 3] = -100;

        arrangkakalkulasi[1, 0, 1, 0] = 40;
        arrangkakalkulasi[1, 0, 1, 1] = -100;
        arrangkakalkulasi[1, 0, 1, 2] = 20;
        arrangkakalkulasi[1, 0, 1, 3] = -100;

        arrangkakalkulasi[1, 0, 2, 0] = 0;
        arrangkakalkulasi[1, 0, 2, 1] = -100;
        arrangkakalkulasi[1, 0, 2, 2] = 10;
        arrangkakalkulasi[1, 0, 2, 3] = -100;

        arrangkakalkulasi[1, 0, 3, 0] = 0;
        arrangkakalkulasi[1, 0, 3, 1] = -100;
        arrangkakalkulasi[1, 0, 3, 2] = 30;
        arrangkakalkulasi[1, 0, 3, 3] = -100;


        arrangkakalkulasi[1, 1, 0, 0] = 20;
        arrangkakalkulasi[1, 1, 0, 1] = -200;
        arrangkakalkulasi[1, 1, 0, 2] = 40;
        arrangkakalkulasi[1, 1, 0, 3] = -200;

        arrangkakalkulasi[1, 1, 1, 0] = 0;
        arrangkakalkulasi[1, 1, 1, 1] = -200;
        arrangkakalkulasi[1, 1, 1, 2] = 0;
        arrangkakalkulasi[1, 1, 1, 3] = -200;

        arrangkakalkulasi[1, 1, 2, 0] = 40;
        arrangkakalkulasi[1, 1, 2, 1] = -400;
        arrangkakalkulasi[1, 1, 2, 2] = 0;
        arrangkakalkulasi[1, 1, 2, 3] = -200;

        arrangkakalkulasi[1, 1, 3, 0] = 0;
        arrangkakalkulasi[1, 1, 3, 1] = -200;
        arrangkakalkulasi[1, 1, 3, 2] = 60;
        arrangkakalkulasi[1, 1, 3, 3] = -100;


        arrangkakalkulasi[1, 2, 0, 0] = 10;
        arrangkakalkulasi[1, 2, 0, 1] = -100;
        arrangkakalkulasi[1, 2, 0, 2] = 0;
        arrangkakalkulasi[1, 2, 0, 3] = -100;

        arrangkakalkulasi[1, 2, 1, 0] = 0;
        arrangkakalkulasi[1, 2, 1, 1] = -200;
        arrangkakalkulasi[1, 2, 1, 2] = 40;
        arrangkakalkulasi[1, 2, 1, 3] = -400;

        arrangkakalkulasi[1, 2, 2, 0] = 0;
        arrangkakalkulasi[1, 2, 2, 1] = -100;
        arrangkakalkulasi[1, 2, 2, 2] = 0;
        arrangkakalkulasi[1, 2, 2, 3] = -100;

        arrangkakalkulasi[1, 2, 3, 0] = 0;
        arrangkakalkulasi[1, 2, 3, 1] = -0;
        arrangkakalkulasi[1, 2, 3, 2] = 0;
        arrangkakalkulasi[1, 2, 3, 3] = -100;


        arrangkakalkulasi[1, 3, 0, 0] = 30;
        arrangkakalkulasi[1, 3, 0, 1] = -100;
        arrangkakalkulasi[1, 3, 0, 2] = 0;
        arrangkakalkulasi[1, 3, 0, 3] = -100;

        arrangkakalkulasi[1, 3, 1, 0] = 60;
        arrangkakalkulasi[1, 3, 1, 1] = -100;
        arrangkakalkulasi[1, 3, 1, 2] = 0;
        arrangkakalkulasi[1, 3, 1, 3] = -100;

        arrangkakalkulasi[1, 3, 2, 0] = 0;
        arrangkakalkulasi[1, 3, 2, 1] = -100;
        arrangkakalkulasi[1, 3, 2, 2] = 0;
        arrangkakalkulasi[1, 3, 2, 3] = -100;

        arrangkakalkulasi[1, 3, 3, 0] = 0;
        arrangkakalkulasi[1, 3, 3, 1] = -100;
        arrangkakalkulasi[1, 3, 3, 2] = 0;
        arrangkakalkulasi[1, 3, 3, 3] = -100;
        #endregion

        #region kasus2
        arrangkakalkulasi[2, 0, 0, 0] = 20;
        arrangkakalkulasi[2, 0, 0, 1] = -100;
        arrangkakalkulasi[2, 0, 0, 2] = 10;
        arrangkakalkulasi[2, 0, 0, 3] = 25;

        arrangkakalkulasi[2, 0, 1, 0] = 40;
        arrangkakalkulasi[2, 0, 1, 1] = -100;
        arrangkakalkulasi[2, 0, 1, 2] = 10;
        arrangkakalkulasi[2, 0, 1, 3] = 50;

        arrangkakalkulasi[2, 0, 2, 0] = 0;
        arrangkakalkulasi[2, 0, 2, 1] = -100;
        arrangkakalkulasi[2, 0, 2, 2] = 5;
        arrangkakalkulasi[2, 0, 2, 3] = 100;

        arrangkakalkulasi[2, 0, 3, 0] = 0;
        arrangkakalkulasi[2, 0, 3, 1] = -100;
        arrangkakalkulasi[2, 0, 3, 2] = 20;
        arrangkakalkulasi[2, 0, 3, 3] = 100;


        arrangkakalkulasi[2, 1, 0, 0] = 20;
        arrangkakalkulasi[2, 1, 0, 1] = -200;
        arrangkakalkulasi[2, 1, 0, 2] = 20;
        arrangkakalkulasi[2, 1, 0, 3] = 100;

        arrangkakalkulasi[2, 1, 1, 0] = 0;
        arrangkakalkulasi[2, 1, 1, 1] = -200;
        arrangkakalkulasi[2, 1, 1, 2] = 0;
        arrangkakalkulasi[2, 1, 1, 3] = 25;

        arrangkakalkulasi[2, 1, 2, 0] = 20;
        arrangkakalkulasi[2, 1, 2, 1] = -200;
        arrangkakalkulasi[2, 1, 2, 2] = 0;
        arrangkakalkulasi[2, 1, 2, 3] = 50;

        arrangkakalkulasi[2, 1, 3, 0] = 0;
        arrangkakalkulasi[2, 1, 3, 1] = -200;
        arrangkakalkulasi[2, 1, 3, 2] = 40;
        arrangkakalkulasi[2, 1, 3, 3] = 150;


        arrangkakalkulasi[2, 2, 0, 0] = 10;
        arrangkakalkulasi[2, 2, 0, 1] = -100;
        arrangkakalkulasi[2, 2, 0, 2] = 0;
        arrangkakalkulasi[2, 2, 0, 3] = 50;

        arrangkakalkulasi[2, 2, 1, 0] = 0;
        arrangkakalkulasi[2, 2, 1, 1] = -200;
        arrangkakalkulasi[2, 2, 1, 2] = 20;
        arrangkakalkulasi[2, 2, 1, 3] = 100;

        arrangkakalkulasi[2, 2, 2, 0] = 0;
        arrangkakalkulasi[2, 2, 2, 1] = -100;
        arrangkakalkulasi[2, 2, 2, 2] = 0;
        arrangkakalkulasi[2, 2, 2, 3] = 25;

        arrangkakalkulasi[2, 2, 3, 0] = 0;
        arrangkakalkulasi[2, 2, 3, 1] = -0;
        arrangkakalkulasi[2, 2, 3, 2] = 0;
        arrangkakalkulasi[2, 2, 3, 3] = 0;


        arrangkakalkulasi[2, 3, 0, 0] = 30;
        arrangkakalkulasi[2, 3, 0, 1] = -100;
        arrangkakalkulasi[2, 3, 0, 2] = 0;
        arrangkakalkulasi[2, 3, 0, 3] = 50;

        arrangkakalkulasi[2, 3, 1, 0] = 60;
        arrangkakalkulasi[2, 3, 1, 1] = -100;
        arrangkakalkulasi[2, 3, 1, 2] = 0;
        arrangkakalkulasi[2, 3, 1, 3] = 25;

        arrangkakalkulasi[2, 3, 2, 0] = 0;
        arrangkakalkulasi[2, 3, 2, 1] = -100;
        arrangkakalkulasi[2, 3, 2, 2] = 0;
        arrangkakalkulasi[2, 3, 2, 3] = 0;

        arrangkakalkulasi[2, 3, 3, 0] = 0;
        arrangkakalkulasi[2, 3, 3, 1] = -100;
        arrangkakalkulasi[2, 3, 3, 2] = 0;
        arrangkakalkulasi[2, 3, 3, 3] = 0;
        #endregion
        
        #region kasus3
        arrangkakalkulasi[3, 0, 0, 0] = 10;
        arrangkakalkulasi[3, 0, 0, 1] = 25;
        arrangkakalkulasi[3, 0, 0, 2] = 20;
        arrangkakalkulasi[3, 0, 0, 3] = -100;

        arrangkakalkulasi[3, 0, 1, 0] = 20;
        arrangkakalkulasi[3, 0, 1, 1] = 100;
        arrangkakalkulasi[3, 0, 1, 2] = 20;
        arrangkakalkulasi[3, 0, 1, 3] = -100;

        arrangkakalkulasi[3, 0, 2, 0] = 0;
        arrangkakalkulasi[3, 0, 2, 1] = 50;
        arrangkakalkulasi[3, 0, 2, 2] = 10;
        arrangkakalkulasi[ 3, 0, 2, 3] = -100;

        arrangkakalkulasi[3, 0, 3, 0] = 0;
        arrangkakalkulasi[3, 0, 3, 1] = 50;
        arrangkakalkulasi[3, 0, 3, 2] = 30;
        arrangkakalkulasi[3, 0, 3, 3] = -100;


        arrangkakalkulasi[3, 1, 0, 0] = 10;
        arrangkakalkulasi[3, 1, 0, 1] = 50;
        arrangkakalkulasi[3, 1, 0, 2] = 40;
        arrangkakalkulasi[3, 1, 0, 3] = -200;

        arrangkakalkulasi[3, 1, 1, 0] = 0;
        arrangkakalkulasi[3, 1, 1, 1] = 25;
        arrangkakalkulasi[3, 1, 1, 2] = 10;
        arrangkakalkulasi[3, 1, 1, 3] = -200;

        arrangkakalkulasi[3, 1, 2, 0] = 20;
        arrangkakalkulasi[3, 1, 2, 1] = 100;
        arrangkakalkulasi[3, 1, 2, 2] = 0;
        arrangkakalkulasi[3, 1, 2, 3] = -200;

        arrangkakalkulasi[3, 1, 3, 0] = 0;
        arrangkakalkulasi[3, 1, 3, 1] = 25;
        arrangkakalkulasi[3, 1, 3, 2] = 60;
        arrangkakalkulasi[3, 1, 3, 3] = -100;

    
        arrangkakalkulasi[3, 2, 0, 0] = 5;
        arrangkakalkulasi[3, 2, 0, 1] = 100;
        arrangkakalkulasi[3, 2, 0, 2] = 0;
        arrangkakalkulasi[3, 2, 0, 3] = -100;

        arrangkakalkulasi[3, 2, 1, 0] = 0;
        arrangkakalkulasi[3, 2, 1, 1] = 50;
        arrangkakalkulasi[3, 2, 1, 2] = 20;
        arrangkakalkulasi[3, 2, 1, 3] = -200;

        arrangkakalkulasi[3, 2, 2, 0] = 0;
        arrangkakalkulasi[3, 2, 2, 1] = 25;
        arrangkakalkulasi[3, 2, 2, 2] = 0;
        arrangkakalkulasi[3, 2, 2, 3] = -100;
        
        arrangkakalkulasi[3, 2, 3, 0] = 0;
        arrangkakalkulasi[3, 2, 3, 1] = 0;
        arrangkakalkulasi[3, 2, 3, 2] = 0;
        arrangkakalkulasi[3, 2, 3, 3] = -100;


        arrangkakalkulasi[3, 3, 0, 0] = 20;
        arrangkakalkulasi[3, 3, 0, 1] = 100;
        arrangkakalkulasi[3, 3, 0, 2] = 0;
        arrangkakalkulasi[3, 3, 0, 3] = -100;

        arrangkakalkulasi[3, 3, 1, 0] = 40;
        arrangkakalkulasi[3, 3, 1, 1] = 150;
        arrangkakalkulasi[3, 3, 1, 2] = 0;
        arrangkakalkulasi[3, 3, 1, 3] = -100;

        arrangkakalkulasi[3, 3, 2, 0] = 0;
        arrangkakalkulasi[3, 3, 2, 1] = 0;
        arrangkakalkulasi[3, 3, 2, 2] = 0;
        arrangkakalkulasi[3, 3, 2, 3] = -100;

        arrangkakalkulasi[3, 3, 3, 0] = 0;
        arrangkakalkulasi[3, 3, 3, 1] = 0;
        arrangkakalkulasi[3, 3, 3, 2] = 0;
        arrangkakalkulasi[3, 3, 3, 3] = -100;
        #endregion
    }
    void refreshcount() {
        for (int a = 0; a < nilaistatis.p1win; a++)
        {
            prefabs.wincount1[a].SetActive(true);
        }
        for (int a = 0; a < nilaistatis.p2win; a++)
        {
            prefabs.wincount2[a].SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        if (!infight)
        {
            time -= Time.deltaTime;
            prefabs.Timerfill.transform.localScale = new Vector3(time / timer, time / timer, 1f);
            if (time <= 0)
            {
                fight();
            }
        }
    }
    void Update()
    {

        tekan = Input.inputString;
        switch (tekan)
        {
            case "a":
                player1.sp = maxsp ;
                break;
            case "s":
                player2.sp = maxsp;
                break;
            case "d":
                player1.sp = 400;
                break;
            case "q":
                player1.hp = 1;
                break;

            case "w":
                player2.hp = 1;
                break;
            default:
                break;
        }

    }

   public void ubahposisiplayer( GameObject tombol)
    {
        if (!infight)
        {
            nowpress = tombol;
            player1.move = int.Parse( tombol.transform.name);
            fight();
        }
    }

    void ubahtombol(GameObject tombol, bool nilai) 
    {
        if (tombol != null)
        {
            switch (tombol.transform.name)
            {
                case "1":
                    prefabs.notblha.SetActive(nilai);
                    break;
                case "2":
                    prefabs.notblbloack.SetActive(nilai);
                    break;
                case "0":
                    prefabs.notbllow.SetActive(nilai);
                    break;
                default:
                    break;
            }
            if (nilai)
            {
                tombol.GetComponent<Image>().color = new Color(1, 1, 1,1);
            }
            else
            {

                tombol.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
            tombol.GetComponent<Button>().enabled = nilai;
        }
    }
    void reset()
    {
        player1.ultimate = false;
        player2.ultimate = false;
        player1.move = 3;
        prefabs.doubledamage.SetActive(false);
        ubahtombol(lastpress, true);
        ubahtombol(nowpress, false);
        lastpress = nowpress;
        nowpress = null;
        infight = false;
        time = timer;
        prefabs.tblskill.SetActive(false);
        prefabs.attackButtons.gameObject.SetActive(true);

        if (player1.sp >= maxsp)
        {
            prefabs.tblskill.SetActive(true);
        }
        prefabs.teks.GetComponent<Text>().text = "reset";
        shakescript.enabled = false;
    }

    public void fight()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(fightprocess());

    }

    int previousBotMove = -1;
    IEnumerator fightprocess() { 
        prefabs.teks.GetComponent<Text>().text = "fight";
        prefabs.attackButtons.gameObject.SetActive(false);
        prefabs.doubledamage.gameObject.SetActive(false); 
        if (player2.sp >= maxsp)
        {
            useskill(2);
        }
        infight = true;

        int randomBotIdleChance = Random.Range(0, 5);

        int random = Random.Range(0, (randomBotIdleChance >= 4 ? 4 : 3));
        while(previousBotMove == random)
        {
            random = Random.Range(0, (randomBotIdleChance >= 4 ? 4 : 3));
        }
        previousBotMove = random;
       // Debug.Log(random);
            switch (random)
            {
                case 0:
                player2.move = 0;
                break;
                case 1:
                    player2.move = 1;
                    break;
                case 2:
                player2.move = 2;
                    break;
                default:
                player2.move = 3;
                    break;
            }
        
        int kasus = 0;
        if (!player1.ultimate && !player2.ultimate)
        {
            kasus = 0;
        }

        if (player1.ultimate && player2.ultimate)
        {
            kasus = 1;
            anicanvas.SetTrigger("p12");
            yield return new WaitForSeconds(waktuskill);
        }
        if (player1.ultimate && !player2.ultimate)
        {
            kasus = 2;
            anicanvas.SetTrigger("p1");
            prefabs.kamera.transform.position = new Vector3(-2.35f, 0.32f, -10f);
            prefabs.kamera.GetComponent<Camera>().orthographicSize = 2.08f;
            yield return new WaitForSeconds(waktuskill);
        }
        if (!player1.ultimate && player2.ultimate)
        {
            kasus = 3;
            anicanvas.SetTrigger("p2");
            prefabs.kamera.transform.position = new Vector3(2.35f, 0.32f, -10f);
            prefabs.kamera.GetComponent<Camera>().orthographicSize = 2.08f;
            yield return new WaitForSeconds(waktuskill);
        }

        

        prefabs.kamera.transform.position = new Vector3(0f, 0f, -10f);
        prefabs.kamera.GetComponent<Camera>().orthographicSize = 5f;
        switch (player1.move)
        {
            case 0:
                anip1.SetTrigger("low");
                break;
            case 1:
                anip1.SetTrigger("high");
                break;
            case 2:
                if (player2.move == 1)
                {
                    anip1.SetTrigger("counter");
                }
                else
                {
                    anip1.SetTrigger("defend");
                }
                break;
            case 3:
                break;
        }
        switch (player2.move)
        {
            case 0:
                anip2.SetTrigger("low");
                break;
            case 1:
                anip2.SetTrigger("high");
                break;
            case 2:
                if (player1.move == 1)
                {
                    anip2.SetTrigger("counter");
                }
                else
                {
                    anip2.SetTrigger("defend");
                }
                break;
            case 3:
                break;
        }

        yield return new WaitForSeconds(waitforanimationdamage);
        kalkulasidamage(kasus, player1.move, player2.move);
    
        prefabs.teks.GetComponent<Text>().text = "prepare";
        bool restart = false;
        if (player1.hp <= 0)
        {
            nilaistatis.p2win++;
            anip1.SetTrigger("fall");
            restart = true;
        }
        else
        {
            if (player2.hp <= 0)
            {
                nilaistatis.p1win++;
                anip2.SetTrigger("fall");
                restart = true;
            }
        }
        refreshcount();

        yield return new WaitForSeconds(afterfighttime);

        if (nilaistatis.p1win >= 2 || nilaistatis.p2win >= 2)
        {
            restart = false;
            bool isPlayerWin = nilaistatis.p1win >= 2;

            if (isPlayerWin)
            {
                AudioManager.Instance.PlayAudio("Win");
                LeanTween.alphaCanvas(prefabs.winPanel, 1, 1f);

                prefabs.winPanel.gameObject.SetActive(true);
            }
            else
            {
                AudioManager.Instance.PlayAudio("Lose");
                LeanTween.alphaCanvas(prefabs.losePanel, 1, 1f);

                prefabs.losePanel.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(isPlayerWin ? 4f : 6f);
            SceneManager.LoadScene(0);
        }

        if (restart )
        {

            SceneManager.LoadScene(1);
        }
            reset();
    }
    void kalkulasidamage(int kasus, int movep1, int movep2) 
    {
        int ubahhpp1 = arrangkakalkulasi[kasus,movep1,movep2,0];
        int ubahsp1 = arrangkakalkulasi[kasus, movep1, movep2, 1];
        int ubahhpp2 = arrangkakalkulasi[kasus, movep1, movep2, 2];
        int ubahsp2 = arrangkakalkulasi[kasus, movep1, movep2, 3];
        
        player1.sp += ubahsp1;
        
        Debug.Log("kasus " + kasus + " move1 " + movep1 + " move2 " + movep2 +" tambah " + ubahsp1 + " jadi " + player1.sp);
        player2.sp += ubahsp2;
        prefabs.sp.value = player1.sp;
        player1.sp = Mathf.Clamp(player1.sp, 0, maxsp);
        player2.sp = Mathf.Clamp(player2.sp, 0, maxsp);
        float tempHp1 = player1.hp;
        player1.hp -= ubahhpp1 ;
        updatehealthbar(prefabs.p1hp, tempHp1, player1.hp);

        float tempHp2 = player2.hp;
        player2.hp -= ubahhpp2 ;
        updatehealthbar(prefabs.p2hp, tempHp2, player2.hp);

    }
    public void useskill(int pemainke)
    {
        if (!infight)
        {
            switch (pemainke)
            {
                case 1:
                    prefabs.doubledamage.SetActive(true);
                    prefabs.tblskill.SetActive(false);
                    player1.ultimate = true;
                    break;
                case 2:
                    player2.ultimate = true;
                   // Debug.Log("a");
                    break;
                default:
                    break;
            }
        }
    }
    void updatehealthbar(HPBarController hpBar, float hpFrom, float hpTo) {
        hpBar.AnimateHP(hpFrom, hpTo);
    }

    public void pause(bool nilai) {

        prefabs.uipause.SetActive(nilai);
        if (nilai)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void mainmenu()
    {
        Time.timeScale = 1f;
        nilaistatis.p1win = 0;
        nilaistatis.p2win = 0;
        SceneManager.LoadScene(0);
    }
    public void retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
