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
    [SerializeField] [Tooltip("how much the player will adjust yaw while holding down the corresponding key")] private float yawInterval = .7f;
    [SerializeField] private float flapForce = 20f;
    [SerializeField] private float takeoffForce = 30f;
    [SerializeField] private float forwardForce = 10f;
    [SerializeField] private float turningForce = 10f;
    [SerializeField] [Tooltip("time between flaps")] private float flapDelay = 3f;
    [SerializeField] [Tooltip("how much the time between flaps counts down per tick (more is faster)")] private float flapInterval = .05f;
    [SerializeField] [Tooltip("how far away the player can be for a landing to trigger")] private float landingDistance = 1.5f;
    private float flapTimer;
    private bool isLanded = false;
    public bool getIsLanded() {
        return isLanded;
    }

    //camera prefab gameobject
   // [SerializeField] [Tooltip("Cinemachine state camera prefab")] private GameObject stateDrivenCamera;

    //input fields
    private PlayerAction playerAction;
    private InputAction roll, pitch, flap, yaw;

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
        yaw = playerAction.Player.Yaw;

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
        //AdjustPitch();
        AdjustYaw();

        //flap up/down
        DoFlap();

        if (!isLanded){
            if (pitch.ReadValue<float>() > 0)
                rb.AddForce(-transform.forward * forwardForce, ForceMode.Force);
            else if (pitch.ReadValue<float>() < 0)
                rb.AddForce(transform.forward * forwardForce, ForceMode.Force);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //check if player collided with an object they can land on
        if (col.gameObject.tag == "Terrain")
            Land();
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Terrain")
            Takeoff();
    }
    private void AdjustRoll()
    {
        if (roll.ReadValue<float>() < 0) { 
            if (transform.rotation.z * 120 < maxRoll) { 
                transform.Rotate(new Vector3(0,0,rollInterval));
            }
            Turn(Vector3.left);
        }         
        else if (roll.ReadValue<float>() > 0) {
            if (transform.rotation.z * 120 > -maxRoll) {
                transform.Rotate(new Vector3(0,0,-rollInterval));
            }
            Turn(Vector3.right);
        }
    }

    /*private void AdjustPitch() {
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
    }*/

    private void AdjustYaw() {
        if (Mathf.Abs(yaw.ReadValue<float>()) > 0) 
        {
            transform.Rotate(new Vector3(0,(yawInterval * yaw.ReadValue<float>()), 0)); //turn left or right
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
            
            //rb.AddForce(Vector3.down * flapForce, ForceMode.Impulse);

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

        rb.constraints = RigidbodyConstraints.None;


        //unlock movement and switch to normal camera
        //stateDrivenCamera.GetComponent<CameraToggle>().CameraSwitch(isLanded);
    }

    private void Land() { 
        isLanded = true;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.constraints = RigidbodyConstraints.FreezeRotation;


        //TODO: lock movement and switch to free look camera
        //stateDrivenCamera.GetComponent<CameraToggle>().CameraSwitch(isLanded);
    }

    private void Turn(Vector3 direction) {
        rb.AddForce(direction * turningForce, ForceMode.Force);

    }

}
