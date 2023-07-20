using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CollectPage : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int _pageCount = 0;

    void Start()
    {
        scoreText.text = "Scraps: " + 0 + "/5";
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            UpdatePageCount();
            Destroy(gameObject);
        }
    }

    void UpdatePageCount()
    {
        _pageCount += 1;
        scoreText.text = "Scraps: " + _pageCount + "/5";
    }

    private void Update()
    {
        if (_pageCount == 5)
            //Load final scene
            return;
    }
}
