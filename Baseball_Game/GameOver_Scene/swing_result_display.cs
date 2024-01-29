using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class swing_result_display : MonoBehaviour
{
    public GameObject swing_result_pop;
    private Button button;
    private bool popup_flag = false;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        swing_result_pop.SetActive(false);
        button.onClick.AddListener(() =>
        {
            if (!popup_flag)
            {
                swing_result_pop.SetActive(true);
                popup_flag = true;
            }
            else if (popup_flag)
            {
                swing_result_pop.SetActive(false);
                popup_flag = false;
            }

        });
    }

    // Update is called once per frame
    void Update()
    {
  
    }
}