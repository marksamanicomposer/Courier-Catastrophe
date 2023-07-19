using UnityEngine;

public class CameraToggle : MonoBehaviour
{

    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public void CameraSwitch(bool landingState) {

        if (landingState)
            anim.Play("Free Look Camera");
        else if (!landingState)
            anim.Play("Player Camera");

    }
}