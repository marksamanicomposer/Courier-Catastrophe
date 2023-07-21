using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{

    private Animator anim;
    private PlayerController controller;

    private void Awake() {
        anim = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (controller.getIsLanded())
            anim.Play("idle");
        else if (!controller.getIsLanded())
            anim.Play("flight");
    }
}
