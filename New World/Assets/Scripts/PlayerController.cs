using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 캐릭터 이동을 위한 변수
    private float moveSpeed = 1;
    private float turnSpeed = 200;
    // private float jumpForce = 4;

    // input
    Vector3 dir;
    private float inputX;
    private float inputZ;
    private float inputJ;
    private float currentV;

    [SerializeField] public Rigidbody rigidbody;
    [SerializeField] public Animator animator;

    // Animator 처리를 위한 변수
    private List<Collider> collisions = new List<Collider>();
    private bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!collisions.Contains(collision.collider))
                {
                    collisions.Add(collision.collider);
                }
                isGround = true;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
  
    }

    void FixedUpdate()
    {
        animator.SetBool("Grounded", isGround);
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
