using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrfootIK : MonoBehaviour
{
    private Animator animator;
    public Vector3 footOffset; // Correct the foot position so that the feet will not pass through ground
    [Range(0,1)]
    public float rightFootPosWeight = 1;
    [Range(0,1)]
        public float rightFootRotWeight = 1;
    [Range(0,1)]
    public float leftFootPosWeight = 1;
    [Range(0,1)]
        public float leftFootRotWeight = 1;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnAnimatorIK(int layerIndex)
    {
        // Get the position for the right feet
        Vector3 rightFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot);
        // Cast a ray to detect ground
        RaycastHit hit;

        bool hasHit = Physics.Raycast(rightFootPos+Vector3.up,Vector3.down,out hit);

        // Detect whether the right foot is on ground. If it is not, set it to ground.
        if (hasHit){
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPosWeight);
        animator.SetIKPosition(AvatarIKGoal.RightFoot,hit.point+footOffset);

            // Make the foot position behave better
            Quaternion rightFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward,hit.normal),hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,rightFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.RightFoot,rightFootRotation);
        }
        else{
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
        }

        Vector3 leftFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);

        hasHit = Physics.Raycast(leftFootPos+Vector3.up,Vector3.down,out hit);
        if (hasHit){
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPosWeight);
                animator.SetIKPosition(AvatarIKGoal.LeftFoot,hit.point+footOffset);

                Quaternion leftFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward,hit.normal),hit.normal);
                            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot,leftFootRotWeight);
                            animator.SetIKRotation(AvatarIKGoal.LeftFoot,leftFootRotation);
                }
                else{
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
                }
    }
}
