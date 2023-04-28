using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBodyControl : MonoBehaviour
{
    // Player body control

    // Plyar  body physics control
    public Transform playerHeadTrans;
    public CapsuleCollider bodyCollider;


    // Range of body collider
    public float minBodyHeight = 0.5f;
    public float maxBodyHeight = 2.5f;


    // Plyaer jump control

    [SerializeField] private InputActionReference jumpActionReference;
    [SerializeField] private float jumpForce = 500.0f;

    private Rigidbody _body;
    public GameObject detectTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        jumpActionReference.action.performed += Jump;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bodyCollider.height = Mathf.Clamp(playerHeadTrans.localPosition.y,minBodyHeight, maxBodyHeight);
        bodyCollider.center = new Vector3(playerHeadTrans.localPosition.x, bodyCollider.height / 2, playerHeadTrans.localPosition.z);
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        RaycastHit[] hit = Physics.RaycastAll(new Vector3(detectTarget.transform.position.x, detectTarget.transform.position.y, detectTarget.transform.position.z), Vector3.down, 1.5f);
        for (int i = 0; i < hit.Length; i++) {
            Debug.Log(hit[i].transform.name);
        }
        if (!IsGrounded()) { return; }
        _body.AddForce(Vector3.up * jumpForce);
    }

    private bool IsGrounded()
    {
        if (Physics.Raycast(new Vector3(detectTarget.transform.position.x,detectTarget.transform.position.y,detectTarget.transform.position.z), Vector3.down,1.5f) == true) { return true; }
        return false;
    }
}
