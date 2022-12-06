using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control_Game : MonoBehaviour
{
    //玩家
    private GameObject m_Target;
    public Control_Player m_Player;
    //路线
    public Control_GameManager gameManager;
    //道路列表
    public List<Transform> roadList = new List<Transform>();
    //相机
    private GameObject m_CameraTarget;
    //分数
    public Control_Score GameScore;
    //道具倒计时
    public Control_DoubleCoinCountdown GameCoinCountdown;
    public Control_Police policeControl;
    // 倒计时面板
    public GameObject doubleCountdown;
    //倒计时
    public int timer = 3;
    public Text countdown;
    public bool isTimeout = true;

    private bool isStart = false;
    private bool isPlay = false;
    
    //the ButtonPauseMenu
    public GameObject startgameMenu;
    public GameObject ingameMenu;
    public GameObject pausegameMenu;
    public GameObject timegameMenu;
    public GameObject overgameMenu;
    
    void Start()
    {
        m_Target = GameObject.Find("Player");
        m_CameraTarget = GameObject.Find("Main Camera");
        countdown.text = "3";
        startgameMenu.SetActive(true);
        ingameMenu.SetActive(false);
        timegameMenu.SetActive(false);
        pausegameMenu.SetActive(false);
        overgameMenu.SetActive(false);
    }

    private void Update()
    {
        if (!isStart)
        {
            OnStart();
        }
    }

    //点击“开始”时执行此方法
    public void OnStart()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) || isPlay)
        {
            Debug.Log("Start");
            isStart = true;
            isTimeout = true;
            timer = 3;
            startgameMenu.SetActive(false);
            ingameMenu.SetActive(true);
            timegameMenu.SetActive(false);
            pausegameMenu.SetActive(false);
            overgameMenu.SetActive(false);
            Time.timeScale = 1.0f;
            m_Target.GetComponent<Control_Player>().enabled = true;
            m_Target.transform.position = new Vector3(0, 0.063f, -3);
            m_CameraTarget.transform.position = new Vector3(0, 4f, -8.5f);
            m_Player.m_ForwardSpeeed = 10.0f;
            m_Player.m_IsEnd = false;
            GameScore.coins.text = "0";
            GameScore.scores.text = "000000";
            GameScore.nowCoins = 0;
            GameScore.nowScores = 0;
            countdown.text = "3";
            GameCoinCountdown.isDouble = false;
            GameCoinCountdown.countDownSlider.value = 1;
            GameCoinCountdown.countDown = null;
            GameCoinCountdown.sliderImg.color = Color.green;
            GameCoinCountdown.sliderImg.color = Color.green;
            doubleCountdown.SetActive(false);
            policeControl.m_Anim.SetBool("punch", false);
            policeControl.transform.position = new Vector3(0, 0, 0);
            for (int i = 0; i < roadList.Count; i++)
            {
                roadList[i].position = new Vector3(0, 0, 100 * i);
            }
            gameManager.InitRoad(0);
            gameManager.InitRoad(1);
        }
    }
    
    //点击“暂停”时执行此方法
    public void OnPause()
    {
        Debug.Log("Pause");
        Time.timeScale = 0;
        pausegameMenu.SetActive(true);
        overgameMenu.SetActive(false);
    }

    // 使用IEnumerator倒计时
    IEnumerator StartTime()
    {
        timegameMenu.SetActive(true);
        pausegameMenu.SetActive(false);
        overgameMenu.SetActive(false);
        isTimeout = false;
        while (timer >= 0)
        {
            countdown.text = timer.ToString("f0");
            yield return new WaitForSeconds(1);   
            timer--;
        }
        if (timer == -1)
        {
            isTimeout = true;
            ingameMenu.SetActive(true);
            timegameMenu.SetActive(false);
            pausegameMenu.SetActive(false);
            overgameMenu.SetActive(false);
            timer = 3;
        }
    }
    
    //点击“Resume”时执行此方法
    public void OnResume()
    {
        Debug.Log("Resume");
        Time.timeScale = 1f;
        if (isTimeout)
        {
            StartCoroutine(StartTime());
        }
    }

    //点击"Home"时执行此方法
    public void OnHome()
    {
        Debug.Log("Home");
        isStart = false;
        m_Target.GetComponent<Control_Player>().enabled = false;
        startgameMenu.SetActive(true);
        ingameMenu.SetActive(false);
        timegameMenu.SetActive(false);
        pausegameMenu.SetActive(false);
        overgameMenu.SetActive(false);
    }
    
    // 游戏结束
    public void OnGameOver()
    {
        Debug.Log("GameOver");
        Time.timeScale = 0;
        m_Target.GetComponent<Control_Player>().enabled = false;
        overgameMenu.SetActive(true);
    }

    //点击"Play"时执行此方法
    public void OnPlay()
    {
        Debug.Log("Play");
        isStart = false;
        // UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        isPlay = true;
        OnStart();
        isPlay = false;
    }
}