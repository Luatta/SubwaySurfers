using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown
{
    public int hour;
    public int minute;
    public int second;

    private int countDownTime; //倒计时剩余时间，秒为单位

    public int CountDownTime
    {
        get { return countDownTime; }
    }

    private int totalTime; //总时间，秒为单位

    public int TotalTime
    {
        get { return totalTime; }
    }

    public CountDown(int _hour, int _minute, int _second)
    {
        hour = _hour;
        minute = _minute;
        second = _second;
        totalTime = 3600 * hour + 60 * minute + second; //计算总时间
        countDownTime = totalTime;
    }

    private float time = 0; //用来控制时间间隔

    // 更新剩余时间，间隔一秒，时间-1
    public void UpdateTime()
    {
        time += Time.deltaTime;
        if (time >= 1 && countDownTime != 0)
        {
            countDownTime--;
            time = 0;
        }
    }
}

public class Control_DoubleCoinCountdown : MonoBehaviour
{
    public Slider countDownSlider; //进度条显示
    public Image sliderImg; //进度条填充图
    public CountDown countDown = null; //声明倒计时对象
    public bool isDouble = false;
    // 倒计时面板
    private GameObject doubleCountdown;
    public Control_Game gameControler;
    
    void Start()
    {
        countDownSlider.value = 1;
        doubleCountdown = GameObject.Find("DoubleCoinSlider");
    }

    //开始倒计时构造一个一分钟的倒计时对象
    public void OnCountDownClick()
    {
        countDown = new CountDown(0, 0, 10);
    }

    void Update()
    {
        if (countDown != null)
        {
            isDouble = true;
            countDown.UpdateTime(); //开启倒计时
            //将倒计时时间映射到进度条上
            if (gameControler.timer == 3)
                countDownSlider.value = countDown.CountDownTime / (countDown.TotalTime * 1.0f);
            //控制进度条显示颜色
            if (countDownSlider.value > 0.6f)
            {
                sliderImg.color = Color.green;
            }
            else if (countDownSlider.value > 0.3f)
            {
                sliderImg.color = Color.yellow;
            }
            else
            {
                sliderImg.color = Color.red;
                if (countDownSlider.value == 0)
                {
                    isDouble = false;
                    doubleCountdown.SetActive(false);
                    countDown = null; //倒计时结束
                } 
            }
        }
    }
}
