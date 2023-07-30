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

    public List<GameObject> goals = new List<GameObject>();
    public GameObject goal1;
    public GameObject goal2;
    public GameObject goal3;
    public GameObject goal4;

    public GameObject deliveryText;
    public ParticleSystem gotIt;

    private AudioSource source;

    void Start()
    {
        AddGoals();

        scoreText.text = "Pages: " + 0 + "/6";
        deliveryText.SetActive(false);

        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "page")
        {
            UpdatePageCount();
            source.Play();
            CheckForWin();
            Destroy(other.gameObject);
            gotIt.Play();
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
            int goal = PickGoal();
            Instantiate(goals[goal]);
            goals[goal].SetActive(true);
            deliveryText.SetActive(true);
        }
    }

    void AddGoals()
    {
        goals.Add(goal1);
        goals.Add(goal2);
        goals.Add(goal3);
        goals.Add(goal4);
    }

    int PickGoal()
    {
        int n = Random.Range(0, goals.Count);
        return n;
    }
}
