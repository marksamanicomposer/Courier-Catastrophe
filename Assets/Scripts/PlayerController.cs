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
    [SerializeField] private float takeoffForce = 30f;
    [SerializeField] [Tooltip("time between flaps")] private float flapDelay = 3f;
    [SerializeField] [Tooltip("how much the time between flaps counts down per tick (more is faster)")] private float flapInterval = .05f;
    [SerializeField] [Tooltip("how far away the player can be for a landing to trigger")] private float landingDistance = 1.5f;
    private float flapTimer;
    private bool isLanded = false;

    //camera prefab gameobject
    [SerializeField] [Tooltip("Cinemachine state camera prefab")] private GameObject stateDrivenCamera;


    //input fields
    private PlayerAction playerAction;
    private InputAction roll, pitch, flap;

    private void Awake() {
        rb = this.GetComponent<Rigidbody>();
        playerAction = new PlayerAction();
    }

    private void OnEnable() {
        playerAction.Player.Coo.started += DoCoo;
        playerAction.Player.Landing.started += DoLandingCheck;

        roll = playerAction.Player.Roll;
        pitch = playerAction.Player.Pitch;
        flap = playerAction.Player.Flap;

        playerAction.Player.Enable();
    }

    private void OnDisable() {
        playerAction.Player.Coo.started -= DoCoo;
        playerAction.Player.Landing.started -= DoLandingCheck;

        playerAction.Player.Disable();
    }

    private void FixedUpdate()
    {
        //pitch and roll code
        AdjustRoll();
        AdjustPitch();

        //flap up/down
        DoFlap();

        Debug.Log(Physics.Raycast(transform.position, Vector3.down, .5f));
        //check if player has physically landed without pressing the landing key, then call the landing method
        if (Physics.Raycast(transform.position, Vector3.down, .5f))
            Land();
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

    //determine if player wants to take off or land when pressing the "landing" key
    private void DoLandingCheck(InputAction.CallbackContext obj) {
        Debug.Log("Left shift pressed");

        if (isLanded)
            Takeoff();
        else if (!isLanded) {

            //determine if it is possible for player to land
            if (Physics.Raycast(transform.position, Vector3.down, landingDistance)) {
            Debug.Log("I can land!");
            Land();

            /*TODO: actually getting the model to land, 
            switching the camera to a free look based on mouse movement (break this out into a second script, prob attached to the cam gameobj),
            disabling (or ignoring) the pitch/roll/flap actions*/

            }

        }
            
    }
    private void Takeoff() {
        isLanded = false;
        rb.AddForce((transform.forward + transform.up) * takeoffForce, ForceMode.Impulse);

        //unlock movement and switch to normal camera
        stateDrivenCamera.GetComponent<CameraToggle>().CameraSwitch(isLanded);
    }

    private void Land() { 
        isLanded = true;
        rb.velocity = Vector3.zero;

        //TODO: lock movement and switch to free look camera
        stateDrivenCamera.GetComponent<CameraToggle>().CameraSwitch(isLanded);
    }


}
