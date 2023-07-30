using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;

    public AudioSource menu;
    public AudioSource intro;
    public AudioSource gameplay;
    public AudioSource outro;

    private bool checkScene = true;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (checkScene)
        {
            if (scene.name == "Menu")
            {
                menu.Play();
                checkScene = false;
                return;
            }
            if (scene.name == "Forest Level")
            {
                gameplay.Play();
                checkScene = false;
                return;
            }
            if (scene.name == "Intro")
            {
                PlayIntroMusic();
                checkScene = false;
                return;
            }
            if (scene.name == "Outro")
            {
                PlayEndingMusic();
                checkScene = false;
                return;
            }
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PlayIntroMusic()
    {
        menu.Stop();
        intro.Play();
        gameplay.PlayScheduled(AudioSettings.dspTime + (intro.clip.length - 5));
        checkScene = false;
    }

    public void PlayEndingMusic()
    {
        gameplay.Stop();
        outro.Play();
        checkScene = false;
    }
}
