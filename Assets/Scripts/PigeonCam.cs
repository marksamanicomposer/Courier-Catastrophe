using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonCam : MonoBehaviour
{

    public GameObject transformTarget, pigeon;
    public float moveSpeed, distance, maxDistance, superMaxDistance;
    
    // Start is called before the first frame update
    void Start()
    {
       // moveSpeed = .17f; //these values feel pretty good
       // maxDistance = 15;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transformTarget.transform.position, moveSpeed); //camera follows your position

        transform.LookAt(pigeon.transform); //camera looks at player

        distance = Vector3.Distance(transform.position, transformTarget.transform.position); //calculates distance between camera and player


        if (distance >= maxDistance && distance <=superMaxDistance)
        {
            moveSpeed = .3f; //camera speeds up if you get too far away
        }


        if (distance>= superMaxDistance)
        {
            moveSpeed = .5f; //if youre waay too far away
        }

        if(distance<= maxDistance)
        {
            moveSpeed = .15f;
        }
       
    }
}
