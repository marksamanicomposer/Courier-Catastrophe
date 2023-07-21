using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject button;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            button.SetActive(true);
        if (button.activeInHierarchy && Input.GetKeyDown(KeyCode.KeypadEnter))
            MainMenu();
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            button.SetActive(false);
    }

    public void MainMenu()
    {
        Destroy(MusicPlayer.Instance);
        SceneManager.LoadScene(0);
    }
}
