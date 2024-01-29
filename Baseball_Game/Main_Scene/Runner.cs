using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Runner : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private AudioClip run_sound;
    private bool music_flag;
    private ballmoving ball_info;
    int runner_status;
    int previous_status;
    private Vector3 home_position = new Vector3(-7.99f, 0, 2.72f);
    private Vector3 first_position = new Vector3(8.08f, 0, -15.18f);
    private Vector3 second_position = new Vector3(19.74f, 0, 0.05f);
    private Vector3 third_position = new Vector3(7.32f, 0, 15.57f);
    private Vector3 first_angle = new Vector3(0, 35.956f, 0);
    private Vector3 third_angle = new Vector3(0, -132.788f, 0);
    private Vector3 home_angle = new Vector3(0, 131.307f, 0);
    private Vector3 second_angle = new Vector3(0, -41.713f, 0);

    private Vector3 start_position = new Vector3(-7.99f, 0, 4.0f);
    private Vector3 wait_position = new Vector3(-12, 0, 15.18f);
    private Vector3 wait_angle = new Vector3(0, -20.5f, 0);

    private float speed = 15.0f;
    private bool get_home_flag = false;
    private Vector3 _prevPosition;
    private Vector3 position;
    private Vector3 velocity;

    [SerializeField] int runner_number;

    // Start is called before the first frame update
    void Start()
    {
        music_flag = false;
        _prevPosition = transform.position;
        GameObject obj = GameObject.Find("Ball");
        ball_info = obj.GetComponent<ballmoving>();
        runner_status = ball_info.runner_state[runner_number];
        if (runner_status == 0)
        {
            transform.localPosition = start_position;
            transform.eulerAngles = home_angle;
        }
        if (runner_status == -1)
        {
            transform.localPosition = wait_position;
        }
        previous_status = runner_status;
    }

    // Update is called once per frame
    void Update()
    {
        AudioSource audio = GetComponent<AudioSource>();
        position = transform.position;
        velocity = (position - _prevPosition) / Time.deltaTime;
        animator.SetFloat("MoveSpeed", (velocity.x)*(velocity.x) + (velocity.y) * (velocity.y) + (velocity.z) * (velocity.z));
        Vector3[] position_array = new Vector3[5]  {home_position,  first_position, second_position, third_position, home_position};
        Vector3[] angle_array = new Vector3[5] {home_angle,  first_angle, second_angle, third_angle, home_angle};
        GameObject obj = GameObject.Find("Ball");
        ball_info = obj.GetComponent<ballmoving>();
        runner_status = ball_info.runner_state[runner_number];
        if (get_home_flag == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, wait_position, speed * Time.deltaTime);
            transform.eulerAngles = wait_angle;
            //transform.eulerAngles = velocity;

            if (transform.position == wait_position)
            {
                get_home_flag = false;
                transform.eulerAngles = angle_array[0];
            }
        }
        else if (runner_status != previous_status)
        {
            if (music_flag == false)
            {
                audio.PlayOneShot(run_sound);
                music_flag = true;
            }
            if (previous_status == -1)
            {
                transform.position = Vector3.MoveTowards(transform.position, start_position, speed * Time.deltaTime);
                //transform.eulerAngles = velocity;
                if (transform.position == start_position)
                {
                    transform.eulerAngles = angle_array[0];
                    previous_status = runner_status;
                }
            }
            else if (runner_status == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, position_array[previous_status + 1], speed * Time.deltaTime);
                //transform.eulerAngles = velocity;
                if (transform.position == position_array[previous_status + 1])
                {
                    transform.eulerAngles = angle_array[previous_status + 1];
                    previous_status += 1;
                }
                if (previous_status == 4)
                {
                    previous_status = runner_status;
                }
            }
            else if (runner_status == -1)
            {
                transform.position = Vector3.MoveTowards(transform.position, position_array[previous_status + 1], speed * Time.deltaTime);
                //transform.eulerAngles = velocity;
                if (transform.position == position_array[previous_status + 1])
                {
                    transform.eulerAngles = angle_array[previous_status + 1];
                    previous_status += 1;
                }
                if (previous_status == 4)
                {
                    previous_status = runner_status;
                    get_home_flag = true;
                }
               
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, position_array[previous_status + 1], speed * Time.deltaTime);
                if (transform.position == position_array[previous_status + 1])
                {
                    transform.eulerAngles = angle_array[previous_status + 1];
                    previous_status += 1;
                }
            }
        }
        else if (runner_status == previous_status)
        {
            music_flag = false;
            audio.Stop();
        }
        _prevPosition = position;
    }
    
   
}
