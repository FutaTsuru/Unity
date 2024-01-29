using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class High_Score : MonoBehaviour
{
    public Text textUI;
    private string high_score;
    // Start is called before the first frame update
    void Start()
    {
        high_score = ballmoving.high_score.ToString();
        textUI.text = "ハイスコア " + high_score + " Pt";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}