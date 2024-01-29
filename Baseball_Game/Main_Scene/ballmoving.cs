using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityEngine.SceneManagement;
using Cinemachine;
using Unity.VisualScripting;

public class ballmoving : MonoBehaviour
{
    //animator
    [SerializeField] private Animator animator;

    //camera
    [SerializeField] public CinemachineVirtualCamera mainvirtualCamera;
    [SerializeField] public CinemachineVirtualCamera leftvirtualCamera;
    [SerializeField] public CinemachineVirtualCamera rightvirtualCamera;
    [SerializeField] public CinemachineVirtualCamera centervirtualCamera;
    private CinemachineVirtualCamera[] camera_array;
    private int camera_distinguishd;
    private int defaultpriority = 5;
    private float camera_timeElapsed;
    private float camera_lagtime = 1.0f;
    private bool camera_change_flag = false;

    //Audio
    [SerializeField] private AudioClip batting_sound;
    [SerializeField] private AudioClip crash_sound;
    [SerializeField] private AudioClip out_sound;
    [SerializeField] private AudioClip hit_sound;
    [SerializeField] private AudioClip throw_sound;
    AudioSource effect_audio;
    
    //Phisical
    new_bat_swing bat_swing;
    private bool throw_animation_flag = false;
    public Rigidbody _rb;
    public float power;

    //ball direction
    private Vector3 direction = new Vector3(-0.8f, 0, 0);
    private Vector3 slider_direction = new Vector3(-0.8f, 0, 0.08f);
    private Vector3 shoot_direction = new Vector3(-0.8f, 0, -0.08f);
    private Vector3 based_position = new Vector3(7.5f, 0.55f, 0.4f);

    //time
    private float timeOut = 7;
    private float timeElapsed = 0.0f;
    private float throw_start_time = 8.5f;
    private float lag_timeElapsed = 0.0f;
    private float throw_lag_time = 2.1f;

    //runner
    private int[] runner_array = new int[3] { 0, 0, 0 };
    public int[] runner_state = new int[4] { 0, -1, -1, -1 };

    //management
    bool out_flag = false;
    public int total_point = 0;
    public static int expressed_total;
    public static int left_out = 8;
    private bool finish_flag = false;
    int new_run;
    int[] ball_array = new int[3]{1, 2, 3};
    public int ball_kind;
    private String high = "HIGH SCORE";
    public static int high_score;
    public int ball_ = 5;
    public bool throw_flag = false;
    public bool first_throw_flag = true;
    public string hit_kind;
    public int hit_num;
    public bool hit_express;
    public static int play_mode;
    public int play_style;
    public bool after_throw_flag = false;
    public Vector3 ball_pos;
    public int ball_num;

    // Start is called before the first frame update
    void Start()
    {
        _rb.position = based_position;
        _rb.velocity = new Vector3(0, 0, 0);
        _rb.rotation = Quaternion.identity;
        effect_audio = GetComponent<AudioSource>();
        effect_audio.volume = 0.65f;
        high_score = PlayerPrefs.GetInt(high);
        power = 1250f;
        left_out = 8;
        hit_express = false;
        animator.SetBool("pitch_flag", throw_animation_flag);
        mainvirtualCamera.Priority = 10;
        leftvirtualCamera.Priority = defaultpriority;
        rightvirtualCamera.Priority = defaultpriority;
        centervirtualCamera.Priority = defaultpriority;
        camera_array = new CinemachineVirtualCamera[]
         {
         leftvirtualCamera,
         rightvirtualCamera,
         centervirtualCamera
         };
        play_style = play_mode;
        ball_pos = _rb.position;
        throw_flag = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ball_pos = _rb.position;
        new_run = -1;
        if (finish_flag)
        {
            //残りアウトが０個になった場合、終了画面に遷移する。
            timeElapsed += Time.deltaTime;
            if (timeElapsed > 3.0)
            {
                replay_button.play_mode = play_mode;
                SceneManager.LoadScene("Game Over Scene");
            }
        }
        if (!finish_flag)
        {
            timeElapsed += Time.deltaTime;
            
            if (timeElapsed >= timeOut && left_out != 0)
            {
                //ボールをもとの位置に戻す
                throw_animation_flag = false;
                throw_flag = false;
                after_throw_flag = false;
                animator.SetBool("pitch_flag", throw_animation_flag);
                if (out_flag == true)
                {
                    //ボールがどの壁にも当たっていない場合、強制的にアウト判定にする。
                    effect_audio.PlayOneShot(out_sound);
                    Debug.Log("Out!");
                    hit_kind = "OUT";
                    hit_num = 0;
                    hit_express = true;
                    left_out -= 1;
                    out_flag = false;
                }
                _rb.position = based_position;
                _rb.velocity = new Vector3(0, 0, 0);
                _rb.rotation = Quaternion.identity;
               
            }
            if (timeElapsed > throw_start_time)
            {
                //投げるモーションを、開始させる。
                lag_timeElapsed += Time.deltaTime;
                throw_animation_flag = true;
                animator.SetBool("pitch_flag", throw_animation_flag);
            }
            if (lag_timeElapsed >= throw_lag_time && left_out != 0)
            {
                //実際にボールを投じ始める。
                effect_audio.PlayOneShot(throw_sound);
                lag_timeElapsed = 0.0f;
                hit_express = false;
                first_throw_flag = false;
                timeElapsed = 0.0f;
                out_flag = true;
                if (play_mode == 0)
                {
                    //イージーモードの場合,球種はストレート
                    ball_kind = 1;
                }
                else
                {
                    //ハードモードの場合、球種をランダムに選択。
                    System.Random random = new System.Random();
                    int rnd = random.Next(ball_array.Length);
                    ball_kind = ball_array[rnd];
                }
                if (ball_kind == 1)//ストレート
                {
                    throw_ball(direction);
                }
                if (ball_kind == 2)//シュート
                {
                    throw_ball(shoot_direction);
                }
                if (ball_kind == 3)//スライダー
                {
                    throw_ball(slider_direction);
                }
                ball_num += 1;
            }
            if (left_out == 0)
            {
                //残りアウト数が０になったとき、スコアを表示する。このとき、ハイスコアを更新したかも確認。
                Debug.Log(total_point);
                _rb.position = based_position;
                _rb.velocity = new Vector3(0, 0, 0);
                if (total_point > high_score)
                {
                    PlayerPrefs.SetInt(high, total_point);
                    PlayerPrefs.Save();
                    Debug.Log("New Score !!");
                    high_score = total_point;
                }
                expressed_total = total_point;
                finish_flag = true;
            }
           
        }
        if (camera_change_flag && !out_flag)
        {
            camera_timeElapsed += Time.deltaTime;

            if (camera_timeElapsed > camera_lagtime)
            {
                camera_array[camera_distinguishd].Priority = defaultpriority;
                camera_change_flag = false;
                camera_timeElapsed = 0;
            }
        }

        void throw_ball(Vector3 direction)
        {
            _rb.AddForce(direction * power);
            throw_flag = true;
            after_throw_flag = true;
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bat")
        {
            //バットと衝突したときの処理。効果音と、ジョイコンに振動が走る。
            GameObject obj = GameObject.Find("bat");
            bat_swing = obj.GetComponent<new_bat_swing>();
            Joycon m_joyconL = bat_swing.m_joyconL;
            effect_audio.PlayOneShot(batting_sound);
            m_joyconL.SetRumble(160, 320, 0.6f, 200);
        }
        if (other.gameObject.tag == "protect")
        {
            //守備や、壁とぶつかったときの処理。効果音が鳴る。
            effect_audio.PlayOneShot(crash_sound);
        }

        if ((other.gameObject.tag == "3pt" || other.gameObject.tag == "poll") && out_flag)
        {
            //3ベースヒットの壁に衝突したときの処理。
            wall_crash_dispose(3);

            //3ベース時の、ランナーの動きを管理。
            for (int i = runner_array.Length - 1; i >= 0; i--)
            {
                if (runner_array[i] == 1)
                {
                    total_point += 1;
                    runner_array[i] = 0;
                }
            }
            runner_array[2] = 1;
            for (int i = runner_state.Length - 1; i >= 0; i--)
            {
                if (runner_state[i] == -1)
                {
                    new_run = i;
                    break;
                }
            }
            for (int i = 0; i < runner_state.Length; i++)
            {
                if (runner_state[i] >= 0)
                {
                    runner_state[i] += 3;
                    if (runner_state[i] >= 4)
                    {
                        runner_state[i] = -1;
                    }
                }
            }
            if (new_run == -1)
            {
                for (int i = runner_state.Length - 1; i >= 0; i--)
                {
                    if (runner_state[i] == -1)
                    {
                        runner_state[i] = 0;
                        break;
                    }
                }
            }
            else
            {
                runner_state[new_run] = 0;
            }
        }

        if (other.gameObject.tag == "2pt" && out_flag == true)
        {
            //2ベースヒットの壁に衝突したときの処理
            wall_crash_dispose(2);

            //2ベース時の、ランナーの動きを管理
            for (int i = runner_array.Length - 1; i >= 0; i--)
            {
                if (runner_array[i] == 1 && i > 0)
                {
                    total_point += 1;
                    runner_array[i] = 0;
                }
                else if (runner_array[i] == 1 && i == 0)
                {
                    runner_array[i] = 0;
                    runner_array[i + 2] = 1;
                }
            }
            runner_array[1] = 1;
            for (int i = runner_state.Length - 1; i >= 0; i--)
            {
                if (runner_state[i] == -1)
                {
                    new_run = i;
                    break;
                }
            }
            for (int i = 0; i < runner_state.Length; i++)
            {
                if (runner_state[i] >= 0)
                {
                    runner_state[i] += 2;
                    if (runner_state[i] >= 4)
                    {
                        runner_state[i] = -1;
                    }
                }
            }
            if (new_run == -1)
            {
                for (int i = runner_state.Length - 1; i >= 0; i--)
                {
                    if (runner_state[i] == -1)
                    {
                        runner_state[i] = 0;
                        break;
                    }
                }
            }
            else
            {
                runner_state[new_run] = 0;
            }
        }

        if (other.gameObject.tag == "1pt" && out_flag == true)
        {
            //ヒットの壁に衝突したときの処理
            wall_crash_dispose(1);

            //ヒット時の、ランナーの動きを管理
            for (int i = runner_array.Length - 1; i >= 0; i--)
            {
                if (runner_array[i] == 1 && i == 2)
                {
                    total_point += 1;
                    runner_array[i] = 0;
                }
                else if (runner_array[i] == 1 && i < 2)
                {
                    runner_array[i] = 0;
                    runner_array[i + 1] = 1;
                }
            }
            runner_array[0] = 1;
            for (int i = runner_state.Length - 1; i >= 0; i--)
            {
                if (runner_state[i] == -1)
                {
                    new_run = i;
                    break;
                }
            }
            for (int i = 0; i < runner_state.Length; i++)
            {
                if (runner_state[i] >= 0)
                {
                    runner_state[i] += 1;
                    if (runner_state[i] >= 4)
                    {
                        runner_state[i] = -1;
                    }
                }
            }
            if (new_run == -1)
            {
                for (int i = runner_state.Length - 1; i >= 0; i--)
                {
                    if (runner_state[i] == -1)
                    {
                        runner_state[i] = 0;
                        break;
                    }
                }
            }
            else
            {
                runner_state[new_run] = 0;
            }
        }

        if (other.gameObject.tag == "4pt" && out_flag == true)
        {
            //ホームランの壁に衝突したときの処理
            wall_crash_dispose(4);

            //ホームラン時の、ランナーの動きを管理
            for (int i = runner_array.Length - 1; i >= 0; i--)
            {
                if (runner_array[i] == 1)
                {
                    total_point += 1;
                    runner_array[i] = 0;
                }
            }
            total_point += 1;
            for (int i = runner_state.Length - 1; i >= 0; i--)
            {
                if (runner_state[i] == -1)
                {
                    new_run = i;
                    break;
                }
            }
            for (int i = 0; i < runner_state.Length; i++)
            {
                if (runner_state[i] >= 0)
                {
                    runner_state[i] += 4;
                    if (runner_state[i] >= 4)
                    {
                        runner_state[i] = -1;
                    }
                }
            }
            if (new_run == -1)
            {
                for (int i = runner_state.Length - 1; i >= 0; i--)
                {
                    if (runner_state[i] == -1)
                    {
                        runner_state[i] = 0;
                        break;
                    }
                }
            }
            else
            {
                runner_state[new_run] = 0;
            }
        }

        if (other.gameObject.tag == "out" && out_flag == true)
        {
            //アウトの壁に衝突したときの処理
            wall_crash_dispose(0);
            left_out -= 1;
        }

        void wall_crash_dispose(int hit_num)
        {
            //壁に衝突したときの、共通の処理を行う関数
            effect_audio.PlayOneShot(crash_sound);
            this.hit_num = hit_num;
            hit_express = true;
            out_flag = false;
            if (hit_num == 0)
            {
                effect_audio.PlayOneShot(out_sound);
                Debug.Log("Out!");
                hit_kind = "OUT";
            }
            else
            {
                effect_audio.PlayOneShot(hit_sound);
                if (hit_num == 1)
                {
                    Debug.Log("HIT!");
                    hit_kind = "HIT!";
                }
                else if (hit_num == 2)
                {
                    Debug.Log("2 BASE HIT!");
                    hit_kind = "2 BASE HIT!";
                }
                else if (hit_num == 3)
                {
                    Debug.Log("3 BASE HIT!");
                    hit_kind = "3 BASE HIT!";
                }
                else if (hit_num == 4)
                {
                    Debug.Log("HOME RUN!");
                    hit_kind = "HOME RUN!";
                }

            }


        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "homerun" && out_flag == true)
        {
            //ホームランゾーン(柵を超えたとき)の処理。
            effect_audio.PlayOneShot(hit_sound);
            Debug.Log("HOME RUN!");
            hit_kind = "HOME RUN!";
            hit_num = 4;
            hit_express = true;
            out_flag = false;

            //ホームラン時の、ランナーの動きを管理
            for (int i = runner_array.Length - 1; i >= 0; i--)
            {
                if (runner_array[i] == 1)
                {
                    total_point += 1;
                    runner_array[i] = 0;
                }
            }
            total_point += 1;
            for (int i = runner_state.Length - 1; i >= 0; i--)
            {
                if (runner_state[i] == -1)
                {
                    new_run = i;
                    break;
                }
            }
            for (int i = 0; i < runner_state.Length; i++)
            {
                if (runner_state[i] >= 0)
                {
                    runner_state[i] += 4;
                    if (runner_state[i] >= 4)
                    {
                        runner_state[i] = -1;
                    }
                }
            }
            if (new_run == -1)
            {
                for (int i = runner_state.Length - 1; i >= 0; i--)
                {
                    if (runner_state[i] == -1)
                    {
                        runner_state[i] = 0;
                        break;
                    }
                }
            }
            else
            {
                runner_state[new_run] = 0;
            }
        }

        if (other.gameObject.tag == "left camera scope" && camera_change_flag == false)
        {
            camera_dispose(0);
        }

        else if (other.gameObject.tag == "right camera scope" && camera_change_flag == false)
        {
            camera_dispose(1);
        }

        else if (other.gameObject.tag == "center camera scope" && camera_change_flag == false)
        {
            camera_dispose(2);
        }

        void camera_dispose(int camera_kind)
        {
            centervirtualCamera.Priority = 100;
            camera_change_flag = true;
            camera_distinguishd = camera_kind;
        }
    }

}