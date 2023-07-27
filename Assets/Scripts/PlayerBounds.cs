using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    private float yBound = 100;
    private float xMaxBound = 950;
    private float xMinBound = 100;
    private float zMaxBound = 900;
    private float zMinBound = 100;

    void Update()
    {
        if (transform.position.y >= yBound)
            transform.position = new Vector3(transform.position.x, yBound, transform.position.z);

        if (transform.position.x >= xMaxBound)
            transform.position = new Vector3(xMaxBound, transform.position.y, transform.position.z);

        if (transform.position.x <= xMinBound)
            transform.position = new Vector3(xMinBound, transform.position.y, transform.position.z);

        if (transform.position.z >= zMaxBound)
            transform.position = new Vector3(transform.position.x, transform.position.y, zMaxBound);

        if (transform.position.z <= zMinBound)
            transform.position = new Vector3(transform.position.x, transform.position.y, zMinBound);
    }
}
