using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public int timeOver = 180;
    public Text timerUI;
    
    // Start is called before the first frame update
    void Start()
    {
        CountDown(); //bat dau dem nguoc
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CountDown(){
        if(timeOver >= 0){
            TimeSpan spantime = TimeSpan.FromSeconds(timeOver); //đặt đơn vị là giây
            timerUI.text = "Time Left: " + spantime.Minutes + ":" + spantime.Seconds; //đặt hiển thị
            timeOver--;
            Invoke("CountDown", 1f); //giảm 1 giây mỗi 1s
        } else{
            FindObjectOfType<GameManager>().GameOver();
        }
    }
    
}
