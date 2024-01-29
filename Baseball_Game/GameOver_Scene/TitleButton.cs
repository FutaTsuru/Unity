using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TitleButton : MonoBehaviour
{
    [SerializeField] private AudioClip button_sound;
    AudioSource effect_audio;
    // Start is called before the first frame update
    void Start()
    {
        effect_audio = GetComponent<AudioSource>();
        var button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            effect_audio.PlayOneShot(button_sound);
            SceneManager.LoadScene("Title Scene");
        });
    }

    // Update is called once per frame

}