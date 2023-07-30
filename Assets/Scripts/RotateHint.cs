using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHint : MonoBehaviour
{
    private bool up;
    public float maxHeight, minHeight;

    void Update()
    {
        transform.Rotate(0f, (40 * Time.deltaTime), 0f);

        if (transform.position.y < minHeight)
            up = true;
        if (transform.position.y > maxHeight)
            up = false;

        if(up)
            transform.Translate(Vector3.up * 6.5f * Time.deltaTime);
        else
            transform.Translate(Vector3.down * 6.5f * Time.deltaTime);
    }
}