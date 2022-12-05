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
    private GameObject m_RoadTarget;
    //相机
    private GameObject m_CameraTarget;
    //分数
    public Control_Score GameScore;

    private bool isStart = false;
    
    //the ButtonPauseMenu
    public GameObject startgameMenu;
    public GameObject ingameMenu;
    public GameObject pausegameMenu;
    public GameObject overgameMenu;
    
    void Start()
    {
        m_Target = GameObject.Find("Player");
        m_RoadTarget = GameObject.Find("GameManager");
        m_CameraTarget = GameObject.Find("Main Camera");
    }

    private void Update()
    {
        OnStart();
    }

    //点击“开始”时执行此方法
    public void OnStart()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) && !isStart)
        {
            isStart = true;
            Debug.Log("Start");
            // UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            startgameMenu.SetActive(false);
            ingameMenu.SetActive(true);
            pausegameMenu.SetActive(false);
            overgameMenu.SetActive(false);
            Time.timeScale = 1.0f;
            m_Player.m_ForwardSpeeed = 10.0f;
            m_Player.m_IsEnd = false;
            GameScore.coins.text = "0";
            GameScore.scores.text = "000000";
            m_Target.GetComponent<Control_Player>().enabled = true;
            m_RoadTarget.GetComponent<Control_GameManager>().enabled = true;
            m_Target.transform.position = new Vector3(0, 0.063f, -3);
            m_CameraTarget.transform.position = new Vector3(0, 4f, -8.5f);
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
        m_RoadTarget.GetComponent<Control_GameManager>().enabled = false;
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
        m_RoadTarget.GetComponent<Control_GameManager>().enabled = false;
        overgameMenu.SetActive(true);
    }

    //点击"Play"时执行此方法
    public void OnPlay()
    {
        Debug.Log("Play");
        isStart = false;
        OnStart();
    }
}