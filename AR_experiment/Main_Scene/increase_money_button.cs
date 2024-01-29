using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class increase_money_button : MonoBehaviour
{
    Button button;
    public int pay_num = 0;
    [SerializeField] Text text;
    AudioSource audiosource;
    [SerializeField] AudioClip push_audio;
    [SerializeField] AudioClip bad_audio;
    [SerializeField] int money;
    [SerializeField] int money_index;
    GameObject paystart_button;
    RandomMoney randommoney;
    public int limit_num;
    // Start is called before the first frame update
    void Start()
    {
        paystart_button = GameObject.Find("paystart_button");
        randommoney = paystart_button.GetComponent<RandomMoney>();
        button = GetComponent<Button>();
        audiosource = GetComponent<AudioSource>();
        text.text = "0枚";
        button.onClick.AddListener(() =>
        {
            if (pay_num < limit_num)
            {
                pay_num++;
                audiosource.PlayOneShot(push_audio);
                text.text = pay_num.ToString() + "枚 : " + pay_num * money + "円";
            }
            else
            {
                audiosource.PlayOneShot(bad_audio);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        text.text = pay_num.ToString() + "枚 : " + pay_num * money + "円";
        limit_num = randommoney.num[money_index];
    }
}