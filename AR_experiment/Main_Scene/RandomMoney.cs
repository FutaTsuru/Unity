using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomMoney : MonoBehaviour
{
    [SerializeField] Text text;
    GameObject total_money_text;
    total_money total_money;
    public int[] moneys = new int[] { 1, 5, 10, 50, 100, 500, 1000, 5000, 10000 };
    public int[] num = new int[] {0,0,0,0,0,0,0,0,0};
    private int max_pay_money;
    GameObject paystart_button;
    start_payment start_payment;

    // Start is called before the first frame update
    void Start()
    {
        //total_money_text = GameObject.Find("total_money_text");
        //total_money = total_money_text.GetComponent<total_money>();
        //pay_money = total_money.total_mon;
        //SetRandomNumber();
    }

    public int[] SetRandomNumber()
    {
        int pay_money = Random.Range(3580, 13500);
        max_pay_money = 0;
        text.text = "";
        num = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        for (int i = 6; i < moneys.Length; i++)
        {
            if (moneys[i] == 10000)
            {
                num[i] = (int)(pay_money / 10000) + 1;
                break;
            }
            if ((int)pay_money / moneys[i] == 0)
            {
                num[i] = 1;
                break;
            }
        }
        for (int i = 0; i < moneys.Length; i++)
        {
            if (moneys[i] == 1 || moneys[i] == 10 || moneys[i] == 100)
            {
                num[i] = Random.Range(4, 9);
            }
            else if (moneys[i] == 1000)
            {
                num[i] = Random.Range(num[i], 9);
            }
            else if (moneys[i] == 5 || moneys[i] == 50 || moneys[i] == 500 || moneys[i] == 5000)
            {
                num[i] = Random.Range(num[i], 2);
            }
        }
        for (int i = moneys.Length - 1; i >= 0 ;  i--)
        {
            max_pay_money += moneys[i] * num[i];
            if (moneys[i] >= 1000)
            {
                text.text += moneys[i].ToString() + "円札 : " + num[i] + "枚\n";
            }
            else
            {
                text.text += moneys[i].ToString() + "円玉 : " + num[i] + "枚\n";
            }
        }
        paystart_button = GameObject.Find("paystart_button");
        start_payment = paystart_button.GetComponent<start_payment>();
        start_payment.max_pay_money = max_pay_money;
        return num;
    }

    public void write_money_info()
    {
        text.text = "";
        for (int i = moneys.Length - 1; i >= 0; i--)
        {
            if (moneys[i] >= 1000)
            {
                text.text += moneys[i].ToString() + "円札 : " + num[i] + "枚\n";
            }
            else
            {
                text.text += moneys[i].ToString() + "円玉 : " + num[i] + "枚\n";
            }
        }
    }

        // Update is called once per frame
    void Update()
    {

    }
}