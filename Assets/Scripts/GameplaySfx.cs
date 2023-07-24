using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySfx : MonoBehaviour
{
    public AudioClip[] wingFlaps;
    public AudioSource wingSource;

    public void WingFlap()
    {
        int newIndex = Random.Range(0, wingFlaps.Length);
        wingSource.PlayOneShot(wingFlaps[newIndex]);
    }
}