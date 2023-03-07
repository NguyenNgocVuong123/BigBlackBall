using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    public int pointStart = 1000;
    public Text pointUI;
    public int getScore;
    // Start is called before the first frame update
    void Start()
    {
        PointDown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PointDown(){
        if(pointStart >= 0){
            pointUI.text = "Point Left: " + pointStart; //đặt hiển thị
            pointStart--;
            Invoke("PointDown", 0.01f); //giảm 1 điểm mỗi 0.1s
            getScore = pointStart;
        } else{
            FindObjectOfType<GameManager>().GameOver();
            
        }
    }
}
