using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonControls : MonoBehaviour
{
    private float speed = 10;
    private float turnSpeed = 1.5f;
    public float pitch, yaw;
    Animator anim;
    public float multiplier;
    public bool isFlying = true;
   

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("flight");
    }

    // Update is called once per frame
    void Update()
    {
                   
        transform.Translate(0, Mathf.Sin(Time.time * 2 - 2) * Time.deltaTime * multiplier, 0, Space.World); //hover up and down a bit

        pitch += turnSpeed * Input.GetAxis("Vertical");//*-1;   //turn direction up/down
        yaw += turnSpeed * Input.GetAxis("Horizontal");        //turn direction left/right

        transform.eulerAngles = new Vector3(pitch, yaw, 0f); // tilt prevention

        transform.Translate(Vector3.back * Time.deltaTime * speed); //forward motion

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) && isFlying)
            speed = 15f;
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return) && isFlying)
            speed = 10f;
        if (!isFlying)
            speed = 10f;
    }


   
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            anim.Play("walk");
            Debug.Log("touchingTerrain");
            isFlying = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            anim.Play("flight");
            isFlying = true;
        }
    }
}
