using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hit_info : MonoBehaviour
{
    public Text textUI;
    ballmoving ball_moving;
    private float timeElapsed;
    private float hit_close = 2.5f;
    [SerializeField] GameObject hit_popup;
    // Start is called before the first frame update
    void Start()
    {
        hit_popup.SetActive(false);
        timeElapsed = 0;
        hit_close = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = GameObject.Find("Ball");
        ball_moving = obj.GetComponent<ballmoving>();
        if (ball_moving.hit_express == true)
        {
            if (timeElapsed <= hit_close)
            {
                timeElapsed += Time.deltaTime;
                textUI.text = ball_moving.hit_kind;
                hit_popup.SetActive(true);
                if (ball_moving.hit_num == 0)
                {
                    textUI.color = Color.red;
                    textUI.fontSize = 60;
                }
                else if (ball_moving.hit_num == 1)
                {
                    textUI.color = Color.yellow;
                    textUI.fontSize = 60;
                }
                else if (ball_moving.hit_num == 2)
                {
                    textUI.color = Color.blue;
                    textUI.fontSize = 60;
                }
                else if (ball_moving.hit_num == 3)
                {
                    textUI.color = Color.green;
                    textUI.fontSize = 60;
                }
                else if (ball_moving.hit_num == 4)
                {
                    textUI.color = Color.magenta;
                    textUI.fontSize = 80;
                }
            }
            else
            {
                textUI.text = "";
                hit_popup.SetActive(false);
            }

        }
        else
        {
            textUI.text = "";
            timeElapsed = 0;
        }
    }
}