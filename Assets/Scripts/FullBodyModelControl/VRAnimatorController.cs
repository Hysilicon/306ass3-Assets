using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimatorController : MonoBehaviour
{
    // Connect the animation with movement control

    public float speedTreshold = 0.1f;
    [Range(0,1)]
    public float smoothing = 1;

    private Animator animator;
    private Vector3 previousPos;
    private VRRig vrRig;
    // Start is called before the first frame update
    void Start()
    {
        animator =GetComponent<Animator>();
        vrRig = GetComponent<VRRig>();
        previousPos = vrRig.head.vrTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Compute the speed
        Vector3 headsetSpeed = (vrRig.head.vrTarget.position - previousPos) / Time.deltaTime;
        headsetSpeed.y=0;

        // Local speed
        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        previousPos = vrRig.head.vrTarget.position;

        // Smooth the moving
        float previousDirectionX = animator.GetFloat("DirectionX");
        float previousDirectionY = animator.GetFloat("DirectionY");

        // Connect the parameters in animator with controller.
        animator.SetBool("IsMoving",headsetLocalSpeed.magnitude > speedTreshold);
        animator.SetFloat("DirectionX",Mathf.Lerp(previousDirectionX,Mathf.Clamp(headsetLocalSpeed.x,-1,1),smoothing));
        animator.SetFloat("DirectionY",Mathf.Lerp(previousDirectionY,Mathf.Clamp(headsetLocalSpeed.z,-1,1),smoothing));
    }
}
