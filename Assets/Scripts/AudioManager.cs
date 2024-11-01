using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource myAudioSource;
    [SerializeField] public AudioClip mainMenuAudio;
    [SerializeField] public AudioClip beachAudio;
    [SerializeField] public AudioClip mountainAudio;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (myAudioSource == null) {
            myAudioSource = this.GetComponent<AudioSource>();
        }
    }

    public void UpdateAudio(string currentScene)
    {
        if (currentScene == "BeachScene") {
            myAudioSource.PlayOneShot(beachAudio);
        }
        else if (currentScene == "MountainScene") {
            myAudioSource.PlayOneShot(mountainAudio);
        }
        else {
            myAudioSource.PlayOneShot(mainMenuAudio);
        }
    }
}
