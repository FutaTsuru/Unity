using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class swing_result : MonoBehaviour
{
    public Text swing_advise;
    public Text swing_angle;
    private List<float> power_angle_List;
    float average;
    float sum;
    float standard_dev;
    float scatter_threshold;
    float upper_threshold = 0.2f;
    float down_threshold = -0.2f;
    // Start is called before the first frame update
    void Start()
    {
        power_angle_List = new_bat_swing.power_angle_List;
        if (power_angle_List.Any())
        {
            average = power_angle_List.Average();
            sum = power_angle_List.Sum(d => Mathf.Pow(d - average, 2));
            standard_dev = Mathf.Sqrt((sum) / power_angle_List.Count());
            Debug.Log("標準偏差: " + standard_dev);
            if (standard_dev > scatter_threshold)
            {
                swing_advise.text = "スイング軌道がバラバラです。安定したスイングを目指そう！";
            }
            else
            {
                swing_advise.text = "安定したスイングです！";
            }

            if (average > upper_threshold)
            {
                swing_angle.text = "アッパースイング気味";
            }
            else if (average < down_threshold)
            {
                swing_angle.text = "ダウンスイング気味";
            }
            else if (standard_dev > scatter_threshold)
            {
                swing_angle.text = "アッパースイングとダウンスイングを繰り返しています。";
            }
            else
            {
                swing_angle.text = "レベルスイング気味";
            }
            
        }
        else
        {
            swing_advise.text = "スイングなし";
            swing_angle.text = "スイングなし";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}