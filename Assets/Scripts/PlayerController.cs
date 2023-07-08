using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //movment fields
    private Rigidbody rb;
    [SerializeField] private float maxPitch = 180f; //max angle (in degrees) the model can pitch in flight
    [SerializeField] private float maxRoll = 180f; //max angle (in degrees) the model can roll in flight
    [SerializeField] private float maxYaw = 180f; //max angle (in degrees) the model can yaw in flight (not currently used)
    [SerializeField] private float rollInterval = .5f; //how much the player will roll while holding down the corresponding key
    [SerializeField] private float pitchInterval = .5f; //how much the player will pitch up/down while holding down the corresponding key
    [SerializeField] private float forwardForce;
    [SerializeField] private float gravityForce = 9.8f;
    [SerializeField] private float flapDelay; //time in seconds between flaps
    private bool isLanded = true;
    private Vector3 playerHeading = Vector3.zero;

    //input fields
    private PlayerActions playerActions;
    private InputAction roll, pitch;

    private void Awake() {
        rb = this.GetComponent<Rigidbody>();
        playerActions = new PlayerActions();
    }

    private void OnEnable() {
        playerActions.Player.Coo.started += DoCoo;
        playerActions.Player.Flap.started += DoFlap;

        roll = playerActions.Player.Roll;
        pitch = playerActions.Player.Pitch;

        playerActions.Player.Enable();
    }

    private void OnDisable() {
        playerActions.Player.Coo.started -= DoCoo;
        playerActions.Player.Flap.started -= DoFlap;

        playerActions.Player.Disable();
    }

    private void FixedUpdate() {
        //TODO: cast a ray from front of bird (tip of beak maybe?) to create a heading, have the bird along that heading
        //do the ray casting when updating the bird's position possibly for better performance?
    }


    //flaps the player's wings or allows them to take off
    private void DoFlap(InputAction.CallbackContext obj) {
        if(isLanded) {
            Takeoff();
        } //TODO: else: have the flap animation play, raise the height of the player slightly relative to the orientation of the model, reset the flap delay timer
    }

    private void DoCoo(InputAction.CallbackContext obj) {
        throw new NotImplementedException();
    }

    private void Takeoff() {
        isLanded = false;
    }


}
