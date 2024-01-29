using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audience_moving : MonoBehaviour
{
    [SerializeField] private Animator audience_animator;
    private float timeElapsed;
    private bool wave_flag;
    // Start is called before the first frame update
    void Start()
    {
        audience_animator.SetBool("wave_flag", false);
        wave_flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > 3)
        {
            if (wave_flag)
            {
                audience_animator.SetBool("wave_flag", false);
                wave_flag = false;
            }
            else
            {
                audience_animator.SetBool("wave_flag", true);
                wave_flag = true;
            }
            timeElapsed = 0;
        }
    }
}