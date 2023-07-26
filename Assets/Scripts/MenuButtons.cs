using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject controlsMenu;
    public Button closeControlsButton;
    public Button controlButton;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenControls()
    {
        controlsMenu.SetActive(true);
        menuScreen.SetActive(false);
        closeControlsButton.Select();
    }

    public void CloseControls()
    {
        controlsMenu.SetActive(false);
        menuScreen.SetActive(true);
        controlButton.Select();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}