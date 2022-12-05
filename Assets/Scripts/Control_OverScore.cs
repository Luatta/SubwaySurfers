using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control_OverScore : MonoBehaviour
{
    private Text overscores;
    private Text overcoins;

    private String score;
    private String coin;
    
    // 游戏控制
    public Control_Score GameScore;
    
    // Start is called before the first frame update
    void Start()
    {
        overscores = GameObject.Find("overscores").GetComponent<Text>();
        overcoins = GameObject.Find("overcoins").GetComponent<Text>();
        overscores.text = "000000";
        overcoins.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        score = GameScore.scores.text.TrimStart('0');
        score = score.Length > 0 ? score : "0";
        coin = GameScore.coins.text;
        overscores.text = score;
        overcoins.text = coin;
    }
}
