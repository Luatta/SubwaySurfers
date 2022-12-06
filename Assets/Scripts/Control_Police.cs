using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Police : MonoBehaviour
{
    //玩家
    private GameObject m_Target;
    // 游戏控制
    public Control_Player gamePlayer;
    //动画组件
    public Animator m_Anim;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Target = GameObject.Find("Player");
        m_Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePlayer.m_IsEnd)
        {
            m_Anim.SetBool("punch", true);
            transform.localPosition = new Vector3(m_Target.transform.localPosition.x, 0,
                m_Target.transform.localPosition.z + 5.8f);
        }
    }
}
