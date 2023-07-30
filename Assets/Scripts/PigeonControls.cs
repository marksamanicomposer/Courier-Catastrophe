using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonControls : MonoBehaviour
{
    private float speed = 12.5f;
    private float boostedSpeed = 20;
    private float turnSpeed = 90f;
    public float pitch, yaw;
    Animator anim;
    private float multiplier = 0.5f;
    public bool isFlying = true;
    private float originalSpeed = 12.5f;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("flight");
    }

    void Update()
    {
                   
        transform.Translate(0, Mathf.Sin(Time.time * 2 - 2) * Time.deltaTime * multiplier, 0, Space.World); //hover up and down a bit

        pitch += turnSpeed * Input.GetAxis("Vertical") * Time.deltaTime;//*-1;   //turn direction up/down
        yaw += turnSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;        //turn direction left/right

        transform.eulerAngles = new Vector3(pitch, yaw, 0f); // tilt prevention

        transform.Translate(Vector3.back * Time.deltaTime * speed); //forward motion

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) && isFlying)
            speed = boostedSpeed;
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return) && isFlying)
            speed = originalSpeed;
        if (!isFlying)
            speed = originalSpeed;
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
