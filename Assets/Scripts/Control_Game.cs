using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool isStart = false;
    private bool isPlay = false;
    
    //the ButtonPauseMenu
    public GameObject startgameMenu;
    public GameObject ingameMenu;
    public GameObject pausegameMenu;
    public GameObject overgameMenu;
    
    void Start()
    {
        m_Target = GameObject.Find("Player");
        m_CameraTarget = GameObject.Find("Main Camera");
        startgameMenu.SetActive(true);
        ingameMenu.SetActive(false);
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
            startgameMenu.SetActive(false);
            ingameMenu.SetActive(true);
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
    
    //点击“Resume”时执行此方法
    public void OnResume()
    { 
        Debug.Log("Resume");
        Time.timeScale = 1f;
        ingameMenu.SetActive(true);
        pausegameMenu.SetActive(false);
        overgameMenu.SetActive(false);
    }

    //点击"Home"时执行此方法
    public void OnHome()
    {
        Debug.Log("Home");
        isStart = false;
        m_Target.GetComponent<Control_Player>().enabled = false;
        startgameMenu.SetActive(true);
        ingameMenu.SetActive(false);
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