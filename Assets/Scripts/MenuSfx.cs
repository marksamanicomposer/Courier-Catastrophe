using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSfx : MonoBehaviour
{
    public AudioClip[] UISelects;
    public AudioClip UISubmit;

    public AudioSource source;

    public void SelectSfx()
    {
        int newIndex = Random.Range(0, UISelects.Length);
        source.PlayOneShot(UISelects[newIndex], 0.5f);
    }

    public void SubmitSfx()
    {
        source.PlayOneShot(UISubmit);
    }
}

