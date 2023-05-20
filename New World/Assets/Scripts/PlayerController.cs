using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 2;
    public float turnSpeed = 200;
    public float jumpForce = 4;

    // input
    Vector3 dir;
    private float inputX;
    private float inputZ;
    private float inputJ;
    private float currentV;

    public Rigidbody rigidbody;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    void FixedUpdate()
    {
        GetInput();
        Walk();
        Jump();

    }

    void GetInput()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        inputJ = Input.GetAxis("Jump");
    }

    void Walk()
    {
        dir = new Vector3(inputX, 0, inputZ);
        
        if(!(inputX == 0 && inputZ == 0))
        {
            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * turnSpeed);
        }
        currentV = Mathf.Lerp(currentV, inputX, Time.deltaTime * 10);

        animator.SetFloat("MoveSpeed", currentV);
        animator.SetFloat("MoveSpeed", dir.magnitude);
    }

    void Jump()
    {

    }
}
