using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //movment fields
    private Rigidbody rb;
    [SerializeField] [Tooltip("max angle (in degrees, roughly) the model can pitch in flight")] private float maxPitch = 90f; 
    [SerializeField] [Tooltip("max angle (in degrees, roughly) the model can roll in flight")] private float maxRoll = 90f;
    [SerializeField] [Tooltip("how much the player will roll while holding down the corresponding key")] private float rollInterval = .5f; 
    [SerializeField] [Tooltip("how much the player will pitch up/down while holding down the corresponding key")] private float pitchInterval = .5f;
    [SerializeField] private float flapForce = 20f;
    [SerializeField] private float takeoffForce = 1000f;
    [SerializeField] [Tooltip("time between flaps")] private float flapDelay = 3f;
    [SerializeField] [Tooltip("float representing how quick the time between flaps counts down (more is faster)")] private float flapInterval = .05f;
    private float flapTimer;
    private bool isLanded = false;

    //input fields
    private PlayerAction playerAction;
    private InputAction roll, pitch, flap;

    private void Awake() {
        rb = this.GetComponent<Rigidbody>();
        playerAction = new PlayerAction();
    }

    private void OnEnable() {
        playerAction.Player.Coo.started += DoCoo;

        roll = playerAction.Player.Roll;
        pitch = playerAction.Player.Pitch;
        flap = playerAction.Player.Flap;

        playerAction.Player.Enable();
    }

    private void OnDisable() {
        playerAction.Player.Coo.started -= DoCoo;

        playerAction.Player.Disable();
    }

    private void FixedUpdate()
    {
        //pitch and roll code
        AdjustRoll();
        AdjustPitch();

        //flap up/down
        DoFlap();
    }

    private void AdjustRoll()
    {
        if (roll.ReadValue<float>() < 0)
        {
            if (this.transform.localRotation.z * 120 < maxRoll)
            {
                this.transform.Rotate(new Vector3(0,0,rollInterval));
            }
        }
        else if (roll.ReadValue<float>() > 0)
        {
            if (this.transform.localRotation.z * 120 > -maxRoll)
            {
                this.transform.Rotate(new Vector3(0,0,-rollInterval));
            }
        }
    }

    private void AdjustPitch() {
        if (pitch.ReadValue<float>() > 0)
        {
            if (this.transform.rotation.x * 120 < maxPitch)
            {
                this.transform.Rotate(new Vector3(pitchInterval,0,0));
            }
        }
        else if (pitch.ReadValue<float>() < 0)
        {
            if (this.transform.rotation.x * 120 > -maxPitch)
            {
                this.transform.Rotate(new Vector3(-pitchInterval,0,0));
            }
        }
    }


    //flaps the player's wings
    private void DoFlap() {

        //update the countdown time between flaps
        if (flapTimer > 0)
            flapTimer -= flapInterval;

        //if the player is able to flap and is pressing/holding the go up or go down button, do a flap in the appropriate direction
        if (Mathf.Abs(flap.ReadValue<float>()) > 0 && flapTimer <= 0) { 
            
            rb.AddForce(transform.up * flapForce * flap.ReadValue<float>(), ForceMode.Impulse);
            flapTimer = flapDelay;
        }

    }

    private void DoCoo(InputAction.CallbackContext obj) {
        Debug.Log("Coo!");
    }

    private void Takeoff() {
        //rb.AddForce((new Vector3(0,1,1) * takeoffForce), ForceMode.Impulse);
        isLanded = false;
    }


}
