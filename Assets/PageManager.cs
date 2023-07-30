using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public List<GameObject> pages = new List<GameObject>();
    private List<int> options = new List<int>();

    public Transform pageParent;

    void Awake()
    {
        for (int n = 0; n < 10; n++)
            options.Add(n);
        //Debug.Log("Total: " + options.Count);
        SpawnPages();
    }

    void SpawnPages()
    {
        for (int n = 0; n < 6; n++)
        {
            int num = GenerateNumber();
            //Debug.Log(pages[num]);

            GameObject pageInstance = pages[num];
            Instantiate(pageInstance, pageParent);
            pages.Remove(pageInstance);
        }
    }

    int GenerateNumber()
    {
        int n = Random.Range(0, options.Count - 1);
        options.Remove(n);
        return n;
    }
}