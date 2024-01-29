using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalPointText : MonoBehaviour
{
    ballmoving ball_moving;
    public Text textUI;
    private int total_point = 0;
    private string total;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = GameObject.Find("Ball");
        ball_moving = obj.GetComponent<ballmoving>();
        total_point = ball_moving.total_point;
        total = total_point.ToString();
        textUI.text = "現在 " + total + " Pt";
    }
}