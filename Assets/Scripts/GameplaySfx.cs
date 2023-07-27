using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySfx : MonoBehaviour
{
    public AudioClip[] wingFlaps;
    public AudioSource wingSource;

    float Modulate(float startVal, float range, float absoluteMin, float absoluteMax)
    {
        float min = 0f;
        float max = 0f;

        float low = startVal - range / 2f;
        float high = startVal + range / 2f;

        if (low < absoluteMin)
            min = absoluteMin;
        else
            min = low;

        if (high > absoluteMax)
            max = absoluteMax;
        else
            max = high;

        return Random.Range(min, max);
    }

    public void WingFlap()
    {
        float finalVol = Modulate(wingSource.volume, 0.05f, 0.2f, 0.3f);
        wingSource.volume = finalVol;
        float finalPit = Modulate(wingSource.pitch, 0.25f, 0.75f, 1.25f);
        wingSource.pitch = finalPit;

        int newIndex = Random.Range(0, wingFlaps.Length);
        wingSource.PlayOneShot(wingFlaps[newIndex]);
    }
}