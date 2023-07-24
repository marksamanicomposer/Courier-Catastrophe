using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollectPage : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int _pageCount = 0;

    public GameObject goal;

    void Start()
    {
        scoreText.text = "Pages: " + 0 + "/6";
        goal.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "page")
        {
            UpdatePageCount();
            CheckForWin();
            Destroy(other.gameObject);
        }

        if(other.tag == "Goal")
        {
            SceneManager.LoadScene(3);
            MusicPlayer.Instance.PlayEndingMusic();
        }
    }

    void UpdatePageCount()
    {
        _pageCount += 1;
        scoreText.text = "Pages: " + _pageCount + "/6";
    }

    private void CheckForWin()
    {
        if (_pageCount == 6)
        {
            goal.SetActive(true);
        }
    }
}
