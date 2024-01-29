using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class total_money : MonoBehaviour
{
    [SerializeField] Text text;
    public int total_mon = 0;
    public int mode_state = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mode_state == 0)
        {
            text.text = "合計金額：" + total_mon + "円";
        }
        else if (mode_state == 1)
        {
            if (total_mon >=0)
            {
                text.text = "支払い金額：" + total_mon + "円";
            }
            else
            {
                text.text = "支払い金額：" + "0" + "円";
            }
            
        }
        else
        {
            text.text = "お釣り：" + -total_mon + "円";
        }
    }
}
