using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buy_info : MonoBehaviour
{
    [SerializeField] Text text;
    public int[] merchandise_num_list = new int[6]{0,0,0,0,0,0};
    private string[] merchandise_name_list = new string[6] { "ナス", "サンドウィッチ", "トマト", "バナナ", "コーラ", "ハンバーガー" };

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "";
        for (int i = 0; i < merchandise_num_list.Length; i++)
        {
            text.text += merchandise_name_list[i] + ": " + merchandise_num_list[i] + "個\n";
        }
    }
}