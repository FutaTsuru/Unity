using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class increase_button : MonoBehaviour
{
    Button button;
    public int buy_num = 0;
    [SerializeField]Text text;
    AudioSource audiosource;
    [SerializeField] AudioClip push_audio;
    [SerializeField] int money;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        text.text = "0個";
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            buy_num++;
            audiosource.PlayOneShot(push_audio);
            text.text = buy_num.ToString() + "個 : " + buy_num * money + "円";
        });

    }
    // Update is called once per frame
    void Update()
    {
        text.text = buy_num.ToString() + "個 : " + buy_num * money + "円";
    }
}