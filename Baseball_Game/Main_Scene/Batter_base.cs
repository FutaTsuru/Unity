using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batter_base : MonoBehaviour
{
    upper_button upper_button;
    down_button down_button;
    left_button left_button;
    right_button right_button;
    public Vector3 start_pos;
    public Rigidbody rb;
    public Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        start_pos = rb.position;
        GameObject upper_obj = GameObject.Find("upperbutton");
        GameObject down_obj = GameObject.Find("downbutton");
        GameObject left_obj = GameObject.Find("leftbutton");
        GameObject right_obj = GameObject.Find("rightbutton");
        upper_button = upper_obj.GetComponent<upper_button>();
        down_button = down_obj.GetComponent<down_button>();
        left_button = left_obj.GetComponent<left_button>();
        right_button = right_obj.GetComponent<right_button>();
        UnityEngine.UI.Button upperbutton = upper_button.button;
        UnityEngine.UI.Button downbutton = down_button.button;
        UnityEngine.UI.Button leftbutton = left_button.button;
        UnityEngine.UI.Button rightbutton = right_button.button;
        pos = rb.position;
        upperbutton.onClick.AddListener(() =>
        {
            if (start_pos.x + 0.3f > pos.x)
            {
                rb.position = new Vector3(pos.x + 0.03f, pos.y, pos.z);
                //pos.x += 0.03f;
            }

        });
        downbutton.onClick.AddListener(() =>
        {
            if (start_pos.x - 0.3f < pos.x)
            {
                rb.position = new Vector3(pos.x - 0.03f, pos.y, pos.z);
                //pos.x -= 0.03f;
            }
        });
        leftbutton.onClick.AddListener(() =>
        {
            if (start_pos.z + 0.3f > pos.z)
            {
                rb.position = new Vector3(pos.x, pos.y, pos.z + 0.03f);
                //pos.z += 0.03f;
            }
        });
        rightbutton.onClick.AddListener(() =>
        {
            if (start_pos.z - 0.3f < pos.z)
            {
                rb.position = new Vector3(pos.x, pos.y, pos.z - 0.03f);
                //pos.z -= 0.03f;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        pos = rb.position;
    }
}