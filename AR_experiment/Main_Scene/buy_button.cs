using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class buy_button : MonoBehaviour
{
    AudioSource audiosource;
    Button button;
    [SerializeField] int money;
    [SerializeField] AudioClip buy_sound;
    [SerializeField] int merchandise_index;
    private int buy_num;
    private int total_money;
    increase_button increasebutton;
    total_money total_money_script;
    buy_info buy_info_script;
    GameObject plus_button;
    GameObject total_money_text;
    GameObject buy_info;
    // Start is called before the first frame update
    void Start()
    {
        plus_button = GameObject.Find("increase_button");
        total_money_text = GameObject.Find("total_money_text");
        buy_info = GameObject.Find("buy_info_text");
        increasebutton = plus_button.GetComponent<increase_button>();
        total_money_script = total_money_text.GetComponent<total_money>();
        buy_info_script = buy_info.GetComponent<buy_info>();
        audiosource = GetComponent<AudioSource>();
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            buy_num = increasebutton.buy_num;
            total_money = buy_num * money;
            total_money_script.total_mon += total_money;
            buy_info_script.merchandise_num_list[merchandise_index] += buy_num;
            audiosource.PlayOneShot(buy_sound);
            increasebutton.buy_num = 0;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
