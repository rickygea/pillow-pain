using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    private class playerstat 
    {
        public int hp;
        public int sp;
       public int move;
        public bool ultimate;
    }
    //private class angkakalkulasi
    //{
    //    public int p1getdamage, p2getdamage, p1getsp, p2getsp;
    //}
    public Prefabs prefabs;
    [Header("TimeSetting")]
    public float timer;
    public float beforefighttime, afterfighttime, afterprepare ;
    [Header("Damages")]
    public int highdmg;
    public int lowdmg;
    [Header("PlayerSetting")]
    public int starthealth;
    public int maxsp;
    private playerstat player1, player2;
    //[SerializeField]
    //string player1move, player2move;
    //public int player1hp, player2hp, player1sp, player2sp;
    private bool infight;
    private int[,,,] arrangkakalkulasi;
    GameObject lastpress, nowpress;
    Coroutine coroutine;
    void Start()
    {
        prefabs.winp1.value = nilaistatis.p1win;
        prefabs.winp2.value = nilaistatis.p2win;
        prefabs.timer.maxValue = timer;
        player1 = new playerstat();
        player2 = new playerstat();
        player1.hp = starthealth;
        player2.hp = starthealth;
        
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
        reset();
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
        arrangkakalkulasi[0, 0, 3, 1] = 0;
        arrangkakalkulasi[0, 0, 3, 2] = 0;
        arrangkakalkulasi[0, 0, 3, 3] = 0;


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
        arrangkakalkulasi[0, 1, 3, 1] = 0;
        arrangkakalkulasi[0, 1, 3, 2] = 0;
        arrangkakalkulasi[0, 1, 3, 3] = 0;


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


        arrangkakalkulasi[0, 3, 0, 0] = 0;
        arrangkakalkulasi[0, 3, 0, 1] = 0;
        arrangkakalkulasi[0, 3, 0, 2] = 0;
        arrangkakalkulasi[0, 3, 0, 3] = 0;

        arrangkakalkulasi[0, 3, 1, 0] = 0;
        arrangkakalkulasi[0, 3, 1, 1] = 0;
        arrangkakalkulasi[0, 3, 1, 2] = 0;
        arrangkakalkulasi[0, 3, 1, 3] = 0;

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
        arrangkakalkulasi[1, 0, 0, 1] = 100;
        arrangkakalkulasi[1, 0, 0, 2] = 20;
        arrangkakalkulasi[1, 0, 0, 3] = 100;

        arrangkakalkulasi[1, 0, 1, 0] = 40;
        arrangkakalkulasi[1, 0, 1, 1] = 100;
        arrangkakalkulasi[1, 0, 1, 2] = 20;
        arrangkakalkulasi[1, 0, 1, 3] = 100;

        arrangkakalkulasi[1, 0, 2, 0] = 0;
        arrangkakalkulasi[1, 0, 2, 1] = 100;
        arrangkakalkulasi[1, 0, 2, 2] = 10;
        arrangkakalkulasi[1, 0, 2, 3] = 100;

        arrangkakalkulasi[1, 0, 3, 0] = 0;
        arrangkakalkulasi[1, 0, 3, 1] = 0;
        arrangkakalkulasi[1, 0, 3, 2] = 0;
        arrangkakalkulasi[1, 0, 3, 3] = 0;


        arrangkakalkulasi[1, 1, 0, 0] = 20;
        arrangkakalkulasi[1, 1, 0, 1] = 200;
        arrangkakalkulasi[1, 1, 0, 2] = 40;
        arrangkakalkulasi[1, 1, 0, 3] = 200;

        arrangkakalkulasi[1, 1, 1, 0] = 0;
        arrangkakalkulasi[1, 1, 1, 1] = 200;
        arrangkakalkulasi[1, 1, 1, 2] = 0;
        arrangkakalkulasi[1, 1, 1, 3] = 200;

        arrangkakalkulasi[1, 1, 2, 0] = 40;
        arrangkakalkulasi[1, 1, 2, 1] = 400;
        arrangkakalkulasi[1, 1, 2, 2] = 0;
        arrangkakalkulasi[1, 1, 2, 3] = 200;

        arrangkakalkulasi[1, 1, 3, 0] = 0;
        arrangkakalkulasi[1, 1, 3, 1] = 0;
        arrangkakalkulasi[1, 1, 3, 2] = 0;
        arrangkakalkulasi[1, 1, 3, 3] = 0;


        arrangkakalkulasi[1, 2, 0, 0] = 10;
        arrangkakalkulasi[1, 2, 0, 1] = 100;
        arrangkakalkulasi[1, 2, 0, 2] = 0;
        arrangkakalkulasi[1, 2, 0, 3] = 100;

        arrangkakalkulasi[1, 2, 1, 0] = 0;
        arrangkakalkulasi[1, 2, 1, 1] = 200;
        arrangkakalkulasi[1, 2, 1, 2] = 40;
        arrangkakalkulasi[1, 2, 1, 3] = 400;

        arrangkakalkulasi[1, 2, 2, 0] = 0;
        arrangkakalkulasi[1, 2, 2, 1] = 0;
        arrangkakalkulasi[1, 2, 2, 2] = 0;
        arrangkakalkulasi[1, 2, 2, 3] = 0;

        arrangkakalkulasi[1, 2, 3, 0] = 0;
        arrangkakalkulasi[1, 2, 3, 1] = 0;
        arrangkakalkulasi[1, 2, 3, 2] = 0;
        arrangkakalkulasi[1, 2, 3, 3] = 0;


        arrangkakalkulasi[1, 3, 0, 0] = 0;
        arrangkakalkulasi[1, 3, 0, 1] = 0;
        arrangkakalkulasi[1, 3, 0, 2] = 0;
        arrangkakalkulasi[1, 3, 0, 3] = 0;

        arrangkakalkulasi[1, 3, 1, 0] = 0;
        arrangkakalkulasi[1, 3, 1, 1] = 0;
        arrangkakalkulasi[1, 3, 1, 2] = 0;
        arrangkakalkulasi[1, 3, 1, 3] = 0;

        arrangkakalkulasi[1, 3, 2, 0] = 0;
        arrangkakalkulasi[1, 3, 2, 1] = 0;
        arrangkakalkulasi[1, 3, 2, 2] = 0;
        arrangkakalkulasi[1, 3, 2, 3] = 0;

        arrangkakalkulasi[1, 3, 3, 0] = 0;
        arrangkakalkulasi[1, 3, 3, 1] = 0;
        arrangkakalkulasi[1, 3, 3, 2] = 0;
        arrangkakalkulasi[1, 3, 3, 3] = 0;
        #endregion

        #region kasus2
        arrangkakalkulasi[2, 0, 0, 0] = 20;
        arrangkakalkulasi[2, 0, 0, 1] = 100;
        arrangkakalkulasi[2, 0, 0, 2] = 10;
        arrangkakalkulasi[2, 0, 0, 3] = 25;

        arrangkakalkulasi[2, 0, 1, 0] = 40;
        arrangkakalkulasi[2, 0, 1, 1] = 100;
        arrangkakalkulasi[2, 0, 1, 2] = 10;
        arrangkakalkulasi[2, 0, 1, 3] = 50;

        arrangkakalkulasi[2, 0, 2, 0] = 0;
        arrangkakalkulasi[2, 0, 2, 1] = 100;
        arrangkakalkulasi[2, 0, 2, 2] = 5;
        arrangkakalkulasi[2, 0, 2, 3] = 100;

        arrangkakalkulasi[2, 0, 3, 0] = 0;
        arrangkakalkulasi[2, 0, 3, 1] = 0;
        arrangkakalkulasi[2, 0, 3, 2] = 0;
        arrangkakalkulasi[2, 0, 3, 3] = 0;


        arrangkakalkulasi[2, 1, 0, 0] = 20;
        arrangkakalkulasi[2, 1, 0, 1] = 200;
        arrangkakalkulasi[2, 1, 0, 2] = 20;
        arrangkakalkulasi[2, 1, 0, 3] = 100;

        arrangkakalkulasi[2, 1, 1, 0] = 0;
        arrangkakalkulasi[2, 1, 1, 1] = 200;
        arrangkakalkulasi[2, 1, 1, 2] = 0;
        arrangkakalkulasi[2, 1, 1, 3] = 25;

        arrangkakalkulasi[2, 1, 2, 0] = 40;
        arrangkakalkulasi[2, 1, 2, 1] = 400;
        arrangkakalkulasi[2, 1, 2, 2] = 0;
        arrangkakalkulasi[2, 1, 2, 3] = 50;

        arrangkakalkulasi[2, 1, 3, 0] = 0;
        arrangkakalkulasi[2, 1, 3, 1] = 0;
        arrangkakalkulasi[2, 1, 3, 2] = 0;
        arrangkakalkulasi[2, 1, 3, 3] = 0;


        arrangkakalkulasi[2, 2, 0, 0] = 10;
        arrangkakalkulasi[2, 2, 0, 1] = 100;
        arrangkakalkulasi[2, 2, 0, 2] = 0;
        arrangkakalkulasi[2, 2, 0, 3] = 50;

        arrangkakalkulasi[2, 2, 1, 0] = 0;
        arrangkakalkulasi[2, 2, 1, 1] = 200;
        arrangkakalkulasi[2, 2, 1, 2] = 20;
        arrangkakalkulasi[2, 2, 1, 3] = 100;

        arrangkakalkulasi[2, 2, 2, 0] = 0;
        arrangkakalkulasi[2, 2, 2, 1] = 0;
        arrangkakalkulasi[2, 2, 2, 2] = 0;
        arrangkakalkulasi[2, 2, 2, 3] = 25;

        arrangkakalkulasi[2, 2, 3, 0] = 0;
        arrangkakalkulasi[2, 2, 3, 1] = 0;
        arrangkakalkulasi[2, 2, 3, 2] = 0;
        arrangkakalkulasi[2, 2, 3, 3] = 0;


        arrangkakalkulasi[2, 3, 0, 0] = 0;
        arrangkakalkulasi[2, 3, 0, 1] = 0;
        arrangkakalkulasi[2, 3, 0, 2] = 0;
        arrangkakalkulasi[2, 3, 0, 3] = 0;

        arrangkakalkulasi[2, 3, 1, 0] = 0;
        arrangkakalkulasi[2, 3, 1, 1] = 0;
        arrangkakalkulasi[2, 3, 1, 2] = 0;
        arrangkakalkulasi[2, 3, 1, 3] = 0;

        arrangkakalkulasi[2, 3, 2, 0] = 0;
        arrangkakalkulasi[2, 3, 2, 1] = 0;
        arrangkakalkulasi[2, 3, 2, 2] = 0;
        arrangkakalkulasi[2, 3, 2, 3] = 0;

        arrangkakalkulasi[2, 3, 3, 0] = 0;
        arrangkakalkulasi[2, 3, 3, 1] = 0;
        arrangkakalkulasi[2, 3, 3, 2] = 0;
        arrangkakalkulasi[2, 3, 3, 3] = 0;
        #endregion
        
        #region kasus3
        arrangkakalkulasi[3, 0, 0, 0] = 10;
        arrangkakalkulasi[3, 0, 0, 1] = 25;
        arrangkakalkulasi[3, 0, 0, 2] = 20;
        arrangkakalkulasi[3, 0, 0, 3] = 100;

        arrangkakalkulasi[3, 0, 1, 0] = 20;
        arrangkakalkulasi[3, 0, 1, 1] = 100;
        arrangkakalkulasi[3, 0, 1, 2] = 20;
        arrangkakalkulasi[3, 0, 1, 3] = 100;

        arrangkakalkulasi[3, 0, 2, 0] = 0;
        arrangkakalkulasi[3, 0, 2, 1] = 50;
        arrangkakalkulasi[3, 0, 2, 2] = 10;
        arrangkakalkulasi[ 3, 0, 2, 3] = 100;

        arrangkakalkulasi[3, 0, 3, 0] = 0;
        arrangkakalkulasi[3, 0, 3, 1] = 0;
        arrangkakalkulasi[3, 0, 3, 2] = 0;
        arrangkakalkulasi[3, 0, 3, 3] = 0;


        arrangkakalkulasi[3, 1, 0, 0] = 10;
        arrangkakalkulasi[3, 1, 0, 1] = 50;
        arrangkakalkulasi[3, 1, 0, 2] = 40;
        arrangkakalkulasi[3, 1, 0, 3] = 200;

        arrangkakalkulasi[3, 1, 1, 0] = 0;
        arrangkakalkulasi[3, 1, 1, 1] = 25;
        arrangkakalkulasi[3, 1, 1, 2] = 10;
        arrangkakalkulasi[3, 1, 1, 3] = 200;

        arrangkakalkulasi[3, 1, 2, 0] = 20;
        arrangkakalkulasi[3, 1, 2, 1] = 100;
        arrangkakalkulasi[3, 1, 2, 2] = 0;
        arrangkakalkulasi[3, 1, 2, 3] = 200;

        arrangkakalkulasi[3, 1, 3, 0] = 0;
        arrangkakalkulasi[3, 1, 3, 1] = 0;
        arrangkakalkulasi[3, 1, 3, 2] = 0;
        arrangkakalkulasi[3, 1, 3, 3] = 0;

    
        arrangkakalkulasi[3, 2, 0, 0] = 5;
        arrangkakalkulasi[3, 2, 0, 1] = 100;
        arrangkakalkulasi[3, 2, 0, 2] = 0;
        arrangkakalkulasi[3, 2, 0, 3] = 100;

        arrangkakalkulasi[3, 2, 1, 0] = 0;
        arrangkakalkulasi[3, 2, 1, 1] = 50;
        arrangkakalkulasi[3, 2, 1, 2] = 20;
        arrangkakalkulasi[3, 2, 1, 3] = 400;

        arrangkakalkulasi[3, 2, 2, 0] = 0;
        arrangkakalkulasi[3, 2, 2, 1] = 25;
        arrangkakalkulasi[3, 2, 2, 2] = 0;
        arrangkakalkulasi[3, 2, 2, 3] = 0;
        
        arrangkakalkulasi[3, 2, 3, 0] = 0;
        arrangkakalkulasi[3, 2, 3, 1] = 0;
        arrangkakalkulasi[3, 2, 3, 2] = 0;
        arrangkakalkulasi[3, 2, 3, 3] = 0;


        arrangkakalkulasi[3, 3, 0, 0] = 0;
        arrangkakalkulasi[3, 3, 0, 1] = 0;
        arrangkakalkulasi[3, 3, 0, 2] = 0;
        arrangkakalkulasi[3, 3, 0, 3] = 0;

        arrangkakalkulasi[3, 3, 1, 0] = 0;
        arrangkakalkulasi[3, 3, 1, 1] = 0;
        arrangkakalkulasi[3, 3, 1, 2] = 0;
        arrangkakalkulasi[3, 3, 1, 3] = 0;

        arrangkakalkulasi[3, 3, 2, 0] = 0;
        arrangkakalkulasi[3, 3, 2, 1] = 0;
        arrangkakalkulasi[3, 3, 2, 2] = 0;
        arrangkakalkulasi[3, 3, 2, 3] = 0;

        arrangkakalkulasi[3, 3, 3, 0] = 0;
        arrangkakalkulasi[3, 3, 3, 1] = 0;
        arrangkakalkulasi[3, 3, 3, 2] = 0;
        arrangkakalkulasi[3, 3, 3, 3] = 0;
        #endregion
    }

    private void FixedUpdate()
    {
        if (!infight)
        {
            prefabs.timer.value -= Time.deltaTime;
            if (prefabs.timer.value <= 0)
            {
                fight();
            }
        }
    }
    void Update()
    {

        //tekan = Input.inputString;
        //if (tekan != null)
        //{
        //    Debug.Log(tekan);
        //}
        //    switch (tekan)
        //{
        //    case "a":
        //        ubahposisiplayer(1, "high");
        //        break;
        //    case "s":
        //        ubahposisiplayer(1, "block");
        //        break;
        //    case "d":
        //        ubahposisiplayer(1, "low");
        //        break;
        //    case "j":
        //        ubahposisiplayer(2, "high");
        //        break;
        //    case "k":
        //        ubahposisiplayer(2, "block");
        //        break;
        //    case "l":
        //        ubahposisiplayer(2, "low");
        //        break;
        //    default:
        //        break;
        //}

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

    void ubahtombol(GameObject tombol, bool aktifkan) 
    {
        if (tombol != null)
        {
            switch (tombol.transform.name)
            {
                case "high":
                    prefabs.notblha.SetActive(aktifkan);
                    break;
                case "block":
                    prefabs.notblbloack.SetActive(aktifkan);
                    break;
                case "low":
                    prefabs.notbllow.SetActive(aktifkan);
                    break;
                default:
                    break;
            }
            tombol.GetComponent<Button>().enabled = !aktifkan;
        }
    }
    void reset()
    {
        player1.ultimate = false;
        player2.ultimate = false;
        player1.move = 3;
        prefabs.doubledamage.SetActive(false);
        ubahtombol(lastpress, false);
        ubahtombol(nowpress, true);
        lastpress = nowpress;
        nowpress = null;
        prefabs.ha1.SetActive(false);
        prefabs.block1.SetActive(false);
        prefabs.low1.SetActive(false);
        prefabs.ha2.SetActive(false);
        prefabs.block2.SetActive(false);
        prefabs.low2.SetActive(false);
        infight = false;
        prefabs.timer.value = timer;
        prefabs.tblskill.SetActive(false);
        if (player1.sp == maxsp)
        {
            prefabs.tblskill.SetActive(true);
        }
        prefabs.teks.GetComponent<Text>().text = "reset";
    }

    public void fight()
    {

        prefabs.teks.GetComponent<Text>().text = "fight";
        infight = true;
        int random = Random.Range(0, 4);
        Debug.Log(random);
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
        if (player2.sp >= maxsp)
        {
            useskill(2);
        }
        int kasus = 0;
        if (!player1.ultimate && !player2.ultimate)
        {
            kasus = 0;
        }

        if (player1.ultimate && player2.ultimate)
        {
            kasus = 1;
        }

        if (!player1.ultimate && player2.ultimate)
        {
            kasus = 2;
        }

        if (player1.ultimate && !player2.ultimate)
        {
            kasus = 3;
        }

        kalkulasidamage(kasus, player1.move, player2.move);
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(prepare());
    }

    IEnumerator prepare()
    {
        prefabs.teks.GetComponent<Text>().text = "prepare";
        bool restart = false;
        if (player1.hp <= 0)
        {
            nilaistatis.p2win++;
            restart = true;
        }
        else
        {
            if (player2.hp <= 0)
            {
                nilaistatis.p1win++;
                restart = true;
            }
        }
        prefabs.winp1.value = nilaistatis.p1win;
        prefabs.winp2.value = nilaistatis.p2win;
       
        yield return new WaitForSeconds(afterfighttime);

        if (nilaistatis.p1win == 3 || nilaistatis.p2win == 3)
        {
            SceneManager.LoadScene(0);
        }

        if (restart)
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
        switch (movep1)
        {
            case 0:
                prefabs.low1.SetActive(true);
                break;
            case 1:
                prefabs.ha1.SetActive(true);
                break;
            case 2:
                prefabs.block1.SetActive(true);
                break;
            case 3:
                break;
        }
        switch (movep2)
        {
            case 0:
                prefabs.low2.SetActive(true);
                break;
            case 1:
                prefabs.ha2.SetActive(true);
                break;
            case 2:
                prefabs.block2.SetActive(true);
                break;
            case 3:
                break;
        }
        player1.sp += ubahsp1;
        player2.sp += ubahsp2;
        prefabs.sp.value += player1.sp;

        float tempHp1 = player1.hp;
        player1.hp -= ubahhpp1 ;
        updatehealthbar(prefabs.p1hp, tempHp1, player1.hp);

        float tempHp2 = player2.hp;
        player2.hp -= ubahhpp2 ;
        updatehealthbar(prefabs.p2hp, tempHp2, player2.hp);

        //updatehealthbar();
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
                    player1.sp = 0;
                    prefabs.sp.value = player1.sp;
                    break;
                case 2:
                    player2.ultimate = true;
                    player2.sp = 0;
                    break;
                default:
                    break;
            }
        }
    }
    void updatehealthbar(HPBarController hpBar, float hpFrom, float hpTo) {
        hpBar.AnimateHP(hpFrom, hpTo);
    }

    
}
