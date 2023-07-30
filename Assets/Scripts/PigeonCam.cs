using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonCam : MonoBehaviour
{

    public GameObject transformTarget, pigeon;
    private float moveSpeed;
    private float boostedSpeed = 18.5f;
    private float maxDistance = 3;
    private float superMaxDistance = 6;
    private float originalSpeed = 12f;
    public float distance;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transformTarget.transform.position, moveSpeed * Time.deltaTime); //camera follows your position

        transform.LookAt(pigeon.transform); //camera looks at player

        distance = Vector3.Distance(transform.position, transformTarget.transform.position); //calculates distance between camera and player


        if (distance <= maxDistance)
        {
            moveSpeed = originalSpeed;
        }


        if (distance >= maxDistance && distance <=superMaxDistance)
        {
            moveSpeed = boostedSpeed; //camera speeds up if you get too far away
        }


        if (distance>= superMaxDistance)
        {
            moveSpeed = moveSpeed * 2; //if youre waay too far away
        }
    }
}
