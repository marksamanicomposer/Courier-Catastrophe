using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;

    public AudioSource menu;
    public AudioSource intro;
    public AudioSource gameplay;
    public AudioSource outro;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayIntroMusic()
    {
        menu.Stop();
        intro.Play();
        gameplay.PlayScheduled(AudioSettings.dspTime + (intro.clip.length - 5));
    }
}
