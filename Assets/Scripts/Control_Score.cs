using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control_Score : MonoBehaviour
{
    private Text scores;
    private Text coins;
    private float nowScores = 0;
    private int nowCoins = 0;
    Transform m_Follow;

    public static Control_Score _instance;
    void Awake()
    {
        _instance = this;
    }
    
    public void Addgold()
    {
        nowCoins += 1;
        coins.text = nowCoins.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Follow = GameObject.Find("Player").transform;
        
        scores = GameObject.Find("scores").GetComponent<Text>();
        coins = GameObject.Find("coins").GetComponent<Text>();

        scores.text = "000000";
        coins.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        nowScores = (int)System.Math.Abs(m_Follow.position.z + 3);
        nowScores *= 2;
        scores.text = nowScores.ToString("f0").PadLeft(6, '0');
    }
}
