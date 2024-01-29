using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class pay_button : MonoBehaviour
{
    AudioSource audiosource;
    Button button;
    [SerializeField] int money;
    [SerializeField] AudioClip buy_sound;
    private int pay_num;
    private int total_money;
    increase_money_button increasebutton;
    total_money total_money_script;
    GameObject plus_button;
    GameObject total_money_text;
    RandomMoney randommoney;
    GameObject start_pay;

    // Start is called before the first frame update
    void Start()
    {
        plus_button = GameObject.Find("increase_button");
        total_money_text = GameObject.Find("total_money_text");
        start_pay = GameObject.Find("paystart_button");
        increasebutton = plus_button.GetComponent<increase_money_button>();
        total_money_script = total_money_text.GetComponent<total_money>();
        randommoney = start_pay.GetComponent<RandomMoney>();
        audiosource = GetComponent<AudioSource>();
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            pay_num = increasebutton.pay_num;
            total_money = pay_num * money;
            total_money_script.total_mon -= total_money;
            audiosource.PlayOneShot(buy_sound);
            randommoney.num[Array.IndexOf(randommoney.moneys, money)] -= pay_num;
            //increasebutton.limit_num -= pay_num;
            increasebutton.pay_num = 0;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}