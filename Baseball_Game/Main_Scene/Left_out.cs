using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Left_out : MonoBehaviour
{
    public Text textUI;
    private int left_out;
    private string outs;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        left_out = ballmoving.left_out;
        outs = left_out.ToString();
        textUI.text = "残り " + outs + " アウト";
    }
}