using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class new_bat_swing : MonoBehaviour
{
    //Joycon
    private static readonly Joycon.Button[] m_buttons = Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];
    private List<Joycon> m_joycons;
    public Joycon m_joyconL;
    private Joycon.Button? m_pressedButtonL;
    private float min_speed = 1.0f;
    private float max_speed = 5;

    public Rigidbody rb;
    public Rigidbody base_rb;

    [SerializeField] private AudioClip swing_audio;

    SwingButton swing_button;

    AudioSource effect_audio;

    private bool button_flag;
    private Vector3 start_pos;
    private Vector3 pos;
    private String swing_kind;
    private float[] gravity = new float[3] { 0, 0, 0 };
    private float alpha = 0.99f;
    private float x_accel;
    private float y_accel;
    private float z_accel;
    private String swing_strength;
    private String swing_timing;

    public float spring = 5000;
    public float damper = 1000;
    private float openAngle = 160;
    private float closeAngle = 0;

    private bool swing_flag = false;
    private float timeElapsed;
    private float swing_back_time = 1;
    private bool close_flag = false;

    HingeJoint hj;
    private float upper_axis = -0.1f;
    private float down_axis = 0.07f;

    ballmoving ball_info;
    private bool throw_flag;
    private Vector3 ball_pos;
    private int ball_num;
    private float power_angle;
    public static List<float> power_angle_List = new List<float>();

    public Text advise_text;
    public Text swing_text;

    private String[] swing_kind_list = new string[3] { "アッパースイング", "レベルスイング", "ダウンスイング" };
    private int index = 0;



    JointSpring spr;

    // 回転軸
    [SerializeField] private Vector3 _axis = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        //ジョイコンの処理
        m_joycons = JoyconManager.Instance.j;
        if (m_joycons == null || m_joycons.Count <= 0) return;
        m_joyconL = m_joycons.Find(c => c.isLeft);

        //HingeJointの処理
        hj = GetComponent<HingeJoint>();
        spr = hj.spring;
        spr.targetPosition = closeAngle;

        //オーディオ処理
        effect_audio = GetComponent<AudioSource>();

        start_pos = base_rb.position;

        //ボタン処理
        GameObject swing_obj = GameObject.Find("joystick2");
        swing_button = swing_obj.GetComponent<SwingButton>();
        UnityEngine.UI.Button swingbutton = swing_button.button;
        
        swingbutton.onClick.AddListener(() =>
        {
            Debug.Log("swing!!");
            if (!swing_flag)
            {
                //オーディオ処理
                effect_audio.PlayOneShot(swing_audio);


                //タイミング解析
                GameObject ball_obj = GameObject.Find("Ball");
                ball_info = ball_obj.GetComponent<ballmoving>();
                throw_flag = ball_info.after_throw_flag;
                ball_pos = ball_info.ball_pos;
                swing_kind = "ダウンスイング";
                base_rb.position = new Vector3(pos.x, pos.y + 0.1f, pos.z);

                if (throw_flag)
                {
                    if (ball_pos.x > -4)
                    {
                        swing_timing = "早い !";
                    }
                    else if (ball_pos.x < -7)
                    {
                        swing_timing = "遅い !";
                    }
                    else
                    {
                        swing_timing = "グッドタイミング !!";
                    }
                    advise_text.text = swing_timing;
                    StartCoroutine(SetText(advise_text));
                    ball_info.after_throw_flag = false;
                }
                //スイング
                openBat((min_speed + max_speed) * 0.5, swing_kind);
                swing_flag = true;

                //スイング解析結果表示処理
                swing_text.text = swing_kind;
                StartCoroutine(SetText(swing_text));
                
            }

        });
    }

    // Update is called once per frame


    void FixedUpdate()
    {
        pos = base_rb.position;

        //振った後の処理
        if (swing_flag)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > swing_back_time && !close_flag)
            {
                Debug.Log("close");
                close_flag = true;
                closeBat();
            }
            if (timeElapsed > swing_back_time + 0.2f && close_flag)
            {
                timeElapsed = 0;
                close_flag = false;
                swing_flag = false;
            }
        }
        //ジョイコン処理
        m_pressedButtonL = null;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        foreach (var joy_button in m_buttons)
        {
            if (m_joyconL.GetButton(joy_button))
            {
                m_pressedButtonL = joy_button;
            }
        }

        foreach (var joycon in m_joycons)
        {
            var accel = joycon.GetAccel();
            var stick = joycon.GetStick();
            var gyro = joycon.GetGyro();
            var button = m_pressedButtonL;
            gravity[0] = alpha * gravity[0] + (1 - alpha) * accel.x;
            gravity[1] = alpha * gravity[1] + (1 - alpha) * accel.y;
            gravity[2] = alpha * gravity[2] + (1 - alpha) * accel.z;
            x_accel = accel.x - gravity[0];
            y_accel = accel.y - gravity[1];
            z_accel = accel.z - gravity[2];
            //var swing_speed = Math.Sqrt(accel.x * accel.x + accel.y * accel.y + accel.z * accel.z);
            float total_accel = (float)Math.Sqrt(x_accel * x_accel + y_accel * y_accel + z_accel * z_accel);

            if (total_accel > min_speed && !swing_flag)
            {
                //オーディオ処理
                effect_audio.PlayOneShot(swing_audio);

                //スイングの強さ解析
                if (total_accel < (max_speed - min_speed) / 3 + min_speed)
                {
                    swing_strength = "弱い";
                }
                else if (total_accel < 2 * (max_speed - min_speed) / 3 + min_speed)
                {
                    swing_strength = "普通の";
                }
                else
                {
                    swing_strength = "強い";
                }

                //スイングの角度解析
                power_angle = y_accel / total_accel;
                if (power_angle > 0.35f)
                {
                    //アッパースイングの場合、スイング前に沈み込む
                    swing_kind = "アッパースイング";
                    base_rb.position = new Vector3(pos.x, pos.y - 0.3f, pos.z);
                }
                else if (power_angle < -0.35f)
                {
                    //ダウンスイングの場合、スイング前に浮く
                    swing_kind = "ダウンスイング";
                    base_rb.position = new Vector3(pos.x, pos.y + 0.05f, pos.z);
                }
                else
                {
                    //レベルスイングの場合、スイング前に少し沈む
                    swing_kind = "レベルスイング";
                    base_rb.position = new Vector3(pos.x, pos.y - 0.075f, pos.z);
                }

                //タイミング解析
                GameObject ball_obj = GameObject.Find("Ball");
                ball_info = ball_obj.GetComponent<ballmoving>();
                throw_flag = ball_info.after_throw_flag;
                ball_pos = ball_info.ball_pos;
                //ball_num = ball_info.ball_num;
                
                if (throw_flag)
                {
                    if (ball_pos.x > -5)
                    {
                        swing_timing = "早い !";
                    }
                    else if (ball_pos.x < -8)
                    {
                        swing_timing = "遅い !";
                    }
                    else
                    {
                        swing_timing = "グッドタイミング !!";
                    }
                    //gravity = new float[3] { 0, 0, 0 };
                    power_angle_List.Add(power_angle);
                    ball_info.after_throw_flag = false;
                    advise_text.text = swing_timing;
                    StartCoroutine(SetText(advise_text));
                }
                swing_text.text = swing_strength + swing_kind;
                StartCoroutine(SetText(swing_text));
                openBat(total_accel < max_speed ? total_accel : max_speed, swing_kind);
                swing_flag = true;
            }

            //ボタンによるスイング処理
            if (button == Joycon.Button.DPAD_DOWN && !swing_flag)
            {
                //オーディオ処理
                effect_audio.PlayOneShot(swing_audio);
                //スイングの種類
                swing_kind = swing_kind_list[index % 3];
                index += 1;
                batterbase_moving(swing_kind);

                //スイングの強さ
                swing_strength = "強い";

                //タイミング解析
                GameObject ball_obj = GameObject.Find("Ball");
                ball_info = ball_obj.GetComponent<ballmoving>();
                throw_flag = ball_info.after_throw_flag;
                ball_pos = ball_info.ball_pos;
                if (throw_flag)
                {
                    if (ball_pos.x > -5)
                    {
                        swing_timing = "早い !";
                    }
                    else if (ball_pos.x < -8)
                    {
                        swing_timing = "遅い !";
                    }
                    else
                    {
                        swing_timing = "グッドタイミング !!";
                    }
                    //gravity = new float[3] { 0, 0, 0 };
                    //power_angle_List.Add(power_angle);
                    ball_info.after_throw_flag = false;
                    advise_text.text = swing_timing;
                    StartCoroutine(SetText(advise_text));
                }
                swing_text.text = swing_strength + swing_kind;
                StartCoroutine(SetText(swing_text));
                openBat((min_speed + max_speed) / 2, swing_kind);
                swing_flag = true;
            }

            if (stick[0] > 0.6f)
            {
                //右入力
                if (start_pos.z - 0.3f < pos.z)
                {
                    base_rb.position = new Vector3(pos.x, pos.y, pos.z - 0.03f);
                }

            }
            else if (stick[0] < -0.6f)
            {
                //左入力
                if (start_pos.z + 0.3f > pos.z)
                {
                    base_rb.position = new Vector3(pos.x, pos.y, pos.z + 0.03f);
                }
            }
            else if (stick[1] > 0.6f)
            {
                //上入力
                if (start_pos.x + 0.3f > pos.x)
                {
                    base_rb.position = new Vector3(pos.x + 0.03f, pos.y, pos.z);
                }
            }
            else if (stick[1] < -0.6f)
            {
                //下入力
                if (start_pos.x - 0.3f < pos.x)
                {
                    base_rb.position = new Vector3(pos.x - 0.03f, pos.y, pos.z);
                }
            }
        }
        

    }
    public void openBat(double speed, String swing_kind)
    {
        if (swing_kind == "アッパースイング")
        {
            hj.axis = new Vector3(0, upper_axis, 1);
        }
        else if (swing_kind == "ダウンスイング")
        {
            hj.axis = new Vector3(0, down_axis, 1);
        }
        else if (swing_kind == "レベルスイング")
        {
            hj.axis = new Vector3(0, 0, 1);
        }
        double patio = 2 * speed / (min_speed + max_speed);
        spr.spring = (float)patio * spring;
        spr.damper = damper;
        spr.targetPosition = openAngle;
        hj.spring = spr;
    }
    public void closeBat()
    {
        spr.spring = spring;
        spr.damper = damper;
        spr.targetPosition = closeAngle;
        hj.spring = spr;
        if (swing_kind == "アッパースイング")
        {
            base_rb.position = new Vector3(pos.x, pos.y + 0.3f, pos.z);
        }
        else if (swing_kind == "レベルスイング")
        {
            base_rb.position = new Vector3(pos.x, pos.y + 0.075f, pos.z);
        }
        else if (swing_kind == "ダウンスイング")
        {
            base_rb.position = new Vector3(pos.x, pos.y - 0.05f, pos.z);
        }
    }

    public void batterbase_moving(String the_swing_kind)
    {
        if (the_swing_kind == "アッパースイング")
        {
            base_rb.position = new Vector3(pos.x, pos.y - 0.3f, pos.z);
        }
        else if (the_swing_kind == "ダウンスイング")
        {
            base_rb.position = new Vector3(pos.x, pos.y + 0.05f, pos.z);
        }
        else if (the_swing_kind == "レベルスイング")
        {
            base_rb.position = new Vector3(pos.x, pos.y - 0.075f, pos.z);
        }
    }
    IEnumerator SetText(Text message)
    {
        yield return new WaitForSeconds(1.0f);
        message.text = "";
    }
}