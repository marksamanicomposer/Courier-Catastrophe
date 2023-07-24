using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHint : MonoBehaviour
{
    private bool up;

    void Update()
    {
        transform.Rotate(0f, (40 * Time.deltaTime), 0f);

        if (transform.position.y < 45)
            up = true;
        if (transform.position.y > 55)
            up = false;

        if(up)
            transform.Translate(Vector3.up * 7.5f * Time.deltaTime);
        else
            transform.Translate(Vector3.down * 7.5f * Time.deltaTime);
    }
}