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


}
