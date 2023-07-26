using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool isPaused = false;

    public AudioMixer mixer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (isPaused)
                Resume();
            else
                Pause();
    }

    void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        isPaused = true;
        mixer.SetFloat("SfxVolume", -80f);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        isPaused = false;
        mixer.SetFloat("SfxVolume", 0f);
    }

    public void MainMenu()
    {
        Destroy(MusicPlayer.Instance.gameObject);
        SceneManager.LoadScene(0);
    }
}
