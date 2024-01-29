using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class start_payment : MonoBehaviour
{
    AudioSource audiosource;
    [SerializeField] AudioClip push_audio;
    [SerializeField] AudioClip great_audio;
    [SerializeField] AudioClip bad_audio;
    Button button;
    [SerializeField] Text total_text;
    [SerializeField] Text info_text;
    [SerializeField] Text button_text;
    [SerializeField] Text attention_text;
    [SerializeField] Text accessment_text;
    GameObject attention_image;
    total_money total_money_script;
    GameObject total_money_text;
    GameObject merchandise;
    GameObject money;
    GameObject buy_info;
    GameObject access_text;
    GameObject access_image;
    finalresult final_result;
    buy_info bought_info;
    RandomMoney randomoney;
    private int total_money;
    private int pay_mode = 0;
    public int max_pay_money;
    private int[] num;
    private int need_money;


    
    // Start is called before the first frame update
    void Start()
    {
        access_image = GameObject.Find("access_image");
        access_text = GameObject.Find("access_text");
        final_result = access_text.GetComponent<finalresult>();
        access_image.SetActive(false);
        attention_image = GameObject.Find("attention_image");
        attention_image.SetActive(false);
        audiosource = GetComponent<AudioSource>();
        buy_info = GameObject.Find("buy_info_text");
        total_money_text = GameObject.Find("total_money_text");
        merchandise = GameObject.Find("merchandise");
        money = GameObject.Find("money_Target");
        money.SetActive(false);
        total_money_script = total_money_text.GetComponent<total_money>();
        bought_info = buy_info.GetComponent<buy_info>();
        //所持金の決定
        randomoney = GetComponent<RandomMoney>();
        num = randomoney.SetRandomNumber();
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (pay_mode == 0)
            {
                total_money = total_money_script.total_mon;
                if (total_money == 0)
                {
                    audiosource.PlayOneShot(bad_audio);
                    info_text.text = "まだお買い物していないよ";
                }
                audiosource.PlayOneShot(push_audio);
                info_text.text = "お会計中";
                info_text.color = Color.green;
                total_money_script.mode_state = 1;
                button_text.text = "お会計終了";
                pay_mode = 1;
                need_money = total_money_script.total_mon;
                money.SetActive(true);
                merchandise.SetActive(false);
            }
            else if (pay_mode == 1)
            {
                total_money = total_money_script.total_mon;
                if (total_money > 0)
                {
                    info_text.text = "まだ全て支払っていません。";
                    audiosource.PlayOneShot(bad_audio);
                }
                else
                {
                    //audiosource.PlayOneShot(great_audio);
                    info_text.text = "支払い完了!!";
                    info_text.color = Color.red;
                    total_text.text = "お釣り: " + -total_money;
                    access_image.SetActive(true);
                    final_result.access_pay(-total_money, num, need_money);
                    total_money_script.mode_state = 2;
                    button_text.text = "お買い物開始";
                    pay_mode = 2;
                    money.SetActive(false);
                }
            }
            else
            {
                accessment_text.text = "";
                access_image.SetActive(false);
                audiosource.PlayOneShot(push_audio);
                info_text.text = "お買い物中";
                info_text.color = Color.blue;
                button_text.text = "お会計開始";
                total_money_script.mode_state = 0;
                total_money_script.total_mon = 0;
                pay_mode = 0;
                merchandise.SetActive(true);
                bought_info.merchandise_num_list = new int[] { 0, 0, 0, 0, 0, 0 };
                //所持金の決定
                randomoney = GetComponent<RandomMoney>();
                num = randomoney.SetRandomNumber();
            }

        });
    }

    // Update is called once per frame
    void Update()
    {
        randomoney.write_money_info();
        total_money = total_money_script.total_mon;
        if (total_money > max_pay_money)
        {
            //合計金額が所持金を超えてしまった時の対応
            attention_text.text = "所持金より高い買い物はできないよ！\n最初からやり直そう！";
            attention_image.SetActive(true);
            pay_mode = 2;
            button_text.text = "お買い物開始";
        }
        else
        {
            attention_text.text = "";
            attention_image.SetActive(false);
        }
    }


}