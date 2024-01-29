using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play_mode : MonoBehaviour
{
    ballmoving ball_moving;
    public Text textUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = GameObject.Find("Ball");
        ball_moving = obj.GetComponent<ballmoving>();
        if (ball_moving.play_style == 0)
        {
            textUI.text = "イージーモード(ストレート)";
            textUI.color = Color.blue;
        }
        else if (ball_moving.play_style == 1)
        {
            textUI.text = "ハードモード(ランダム)";
            textUI.color = Color.red;
        }
    }
}
