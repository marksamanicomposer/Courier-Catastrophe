using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //movment fields
    private Rigidbody rb;
    [SerializeField] [Tooltip("max angle (in degrees) the model can pitch in flight")] private float maxPitch = 90f; 
    [SerializeField] [Tooltip("max angle (in degrees) the model can roll in flight")] private float maxRoll = 90f;
    [SerializeField] [Tooltip("max angle (in degrees) the model can yaw in flight (not currently used)")] private float maxYaw = 90f; 
    [SerializeField] [Tooltip("how much the player will roll while holding down the corresponding key")] private float rollInterval = .5f; 
    [SerializeField] [Tooltip("how much the player will pitch up/down while holding down the corresponding key")] private float pitchInterval = .5f;
    [SerializeField] private float flapForce = 20f;
    [SerializeField] private float takeoffForce = 1000f;
    [SerializeField] [Tooltip("time in seconds between flaps")] private float flapDelay = 3f;
    private float flapTimer;
    private bool isLanded = false;

    //input fields
    private PlayerActionController playerAction;
    private InputAction roll, pitch;

    private void Awake() {
        rb = this.GetComponent<Rigidbody>();
        playerAction = new PlayerActionController();
    }

    private void OnEnable() {
        playerAction.Player.Coo.started += DoCoo;
        playerAction.Player.Flap.started += DoFlap;

        roll = playerAction.Player.Roll;
        pitch = playerAction.Player.Pitch;

        playerAction.Player.Enable();
    }

    private void OnDisable() {
        playerAction.Player.Coo.started -= DoCoo;
        playerAction.Player.Flap.started -= DoFlap;

        playerAction.Player.Disable();
    }

    private void FixedUpdate() {
        //TODO: pitch and roll code
    }


    //flaps the player's wings or allows them to take off
    private void DoFlap(InputAction.CallbackContext obj) {

        if(isLanded) {
            Takeoff();
        } else {
            rb.AddForce(transform.up * flapForce, ForceMode.Impulse);
        }

        flapTimer = flapDelay;

    }

    private void DoCoo(InputAction.CallbackContext obj) {
        Debug.Log("Coo!");
    }

    private void Takeoff() {
        rb.AddForce((new Vector3(0,1,1) * takeoffForce), ForceMode.Impulse);
        isLanded = false;
    }


}
