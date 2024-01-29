using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartButton : MonoBehaviour
{
    public int play_mode;
    [SerializeField] private AudioClip start_sound;
    AudioSource effect_audio;
    ballmoving ballmoving;
    // Start is called before the first frame update
    void Start()
    {
        effect_audio = GetComponent<AudioSource>();
        var button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            effect_audio.PlayOneShot(start_sound);
            ballmoving.play_mode = this.play_mode;
            SceneManager.LoadScene("Main Scene");
        });
    }

    // Update is called once per frame
    
}