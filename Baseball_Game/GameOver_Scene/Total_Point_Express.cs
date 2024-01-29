using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Total_Point_Express : MonoBehaviour
{
    public Text textUI;
    private string total_point;
    // Start is called before the first frame update
    void Start()
    {
        total_point = ballmoving.expressed_total.ToString();
        textUI.text = "今回の得点 " + total_point + " Pt";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}