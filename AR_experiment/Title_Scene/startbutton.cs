using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startbutton : MonoBehaviour
{
    Button button;
    AudioSource audiosource;
    [SerializeField] string scene;
    [SerializeField] AudioClip push_audio;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            audiosource.PlayOneShot(push_audio);
            SceneManager.LoadScene(scene);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}