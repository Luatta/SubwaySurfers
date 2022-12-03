﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Control_Player : MonoBehaviour
{
    //前进速度
    public float m_ForwardSpeeed;
    // 游戏管理器
    public Control_GameManager gameManager;
    //动画组件
    private Animator m_Anim;
    //游戏结束
    private bool m_IsEnd = false;

    private Rigidbody rig;
    
    // Use this for initialization
    void Start()
    {
        m_Anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += Vector3.forward * m_ForwardSpeeed * Time.deltaTime;
        transform.Translate(Vector3.forward * m_ForwardSpeeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_Anim.SetTrigger("jump");
            rig.velocity = new Vector3(0, 5.5f, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_Anim.SetTrigger("slide");
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Change_PlayerZ(true);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Change_PlayerZ(false);
        }
    }

    public void Change_PlayerZ(bool IsAD)
    {
        if (IsAD)
        {
            if (System.Math.Abs(transform.position.x + 2.4f) < 0.001f)
                return;
            else if (System.Math.Abs(transform.position.x - 0) < 0.001f)
            {
                transform.position = new Vector3(-2.4f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(0f, transform.position.y, transform.position.z);
            }
        }
        else
        {
            if (System.Math.Abs(transform.position.x - 2.4f) < 0.001f)
                return;
            else if (System.Math.Abs(transform.position.x - 0) < 0.001f)
            {
                transform.position = new Vector3(2.4f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(0f, transform.position.y,transform.position.z);
            }

        }
    }
    
    //游戏结束
    void OnTriggerEnter(Collider other)
    {
        // 如果是抵达点
        if (other.name.Equals("ArrivePos"))
        {
            gameManager.changeRoad(other.transform);
        }
        // 如果是障碍物
        else if (other.tag.Equals("Obstacle"))
        {
            Debug.Log("22222");
            m_IsEnd = true;
            m_ForwardSpeeed = 0;
        }
        
    }
}
