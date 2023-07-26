using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //movement fields
    private Rigidbody rb;
    [SerializeField] [Tooltip("max angle (in degrees, roughly) the model can pitch in flight")] private float maxPitch = 90f; 
    [SerializeField] [Tooltip("how much the player will roll while holding down the corresponding key")] private float pitchInterval = 2.53f;
    [SerializeField] [Tooltip("how much the player will adjust yaw while holding down the corresponding key")] private float yawInterval = .7f;
    [SerializeField] private float maxVelocity = 60f;
    [SerializeField] private float flapForce = 20f;
    [SerializeField] private float takeoffForce = 30f;
    [SerializeField] private float forwardForce = 10f;
    [SerializeField] [Tooltip("time between flaps")] private float flapDelay = 3f;
    [SerializeField] [Tooltip("how much the time between flaps counts down per tick (more is faster)")] private float flapInterval = .05f;
    [SerializeField] [Tooltip("how far away the player can be for a Takeoff to trigger")] private float TakeoffDistance = 1.5f;
    private float thrustDirection, yawDirection, pitchDirection;
    private bool isLanded = false;
    public bool getIsLanded() {
        return isLanded;
    }

    //input fields
    private PlayerAction playerAction;

    private void Awake() {
        rb = this.GetComponent<Rigidbody>();
        playerAction = new PlayerAction();
    }

    private void OnEnable() {
        playerAction.Player.Coo.started += DoCoo;
        playerAction.Player.Takeoff.started += DoTakeoffCheck;

        playerAction.Player.Enable();
    }

    private void OnDisable() {
        playerAction.Player.Coo.started -= DoCoo;
        playerAction.Player.Takeoff.started -= DoTakeoffCheck;

        playerAction.Player.Disable();
    }

    private void Update() {
        CheckInputs();
    }

    private void FixedUpdate()
    {
        Thrust();
        AdjustPitch();
        AdjustYaw();
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

    private void DoTakeoffCheck(InputAction.CallbackContext value) {
        if (isLanded)
            Takeoff();
    }

    private void Land() {
        isLanded = true;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        //camera switch goes here

    }

    private void Takeoff() {
        
        rb.constraints = RigidbodyConstraints.None;

        rb.AddForce(transform.up * takeoffForce, ForceMode.Force);
        isLanded = false;
        //camera switch goes here

    }

    private void CheckInputs() {
        thrustDirection = playerAction.Player.Thrust.ReadValue<float>();
        pitchDirection = playerAction.Player.Pitch.ReadValue<float>();
        yawDirection = playerAction.Player.Yaw.ReadValue<float>();
    }

    private void Thrust() {
        if (thrustDirection != 0)
            rb.AddForce((transform.forward * -thrustDirection * forwardForce), ForceMode.Force);
        else
            rb.AddForce((transform.forward * -thrustDirection * forwardForce * .5f), ForceMode.Force);

    }

    private void AdjustPitch() {
        if ((pitchDirection == 1 && transform.rotation.x * 120 < maxPitch) || (pitchDirection == -1 && transform.rotation.x * 120 > -maxPitch))
            transform.Rotate((new Vector3(1,0,0) * pitchInterval * pitchDirection));
    }

    private void AdjustYaw() {
        if (yawDirection != 0)
            transform.Rotate(new Vector3(0,1,0) * -yawInterval * yawDirection);

    }

    private void DoCoo(InputAction.CallbackContext value) {
        Debug.Log("Coo!");
    }


}
