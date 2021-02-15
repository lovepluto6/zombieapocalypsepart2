using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
 //   public float movementForce;
 //   public Rigidbody rigidbody;
    public bool isGrounded;
    public CharacterController controller;
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;
    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;
    public Vector3 velocity;
 //   public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
       
       // rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame - once every 16.666ms
    //1000ms for each second
    //approximately updates 60 times per second = 60fps
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if(isGrounded&& velocity.y < 0)
        {
            velocity.y = -2.0f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * maxSpeed * Time.deltaTime);

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        //if (isGrounded) { 
        //    if(Input.GetAxisRaw("Horizontal") > 0)
        //    {
        //        //move right
        //        rigidbody.AddForce(Vector3.right * movementForce);
        //    }
        //    if (Input.GetAxisRaw("Horizontal") < 0)
        //    {
        //        //move left
        //        rigidbody.AddForce(Vector3.left * movementForce);
        //    }
        //    if (Input.GetAxisRaw("Vertical") > 0)
        //    {
        //        //move forward
        //        rigidbody.AddForce(Vector3.forward * movementForce);
        //    }
        //    if (Input.GetAxisRaw("Vertical") < 0)
        //    {
        //        //move back
        //        rigidbody.AddForce(Vector3.back * movementForce);
        //    }
        //    if (Input.GetAxisRaw("Jump") > 0)
        //    {
        //        //jump
        //        rigidbody.AddForce(Vector3.up * jumpForce);
        //    }
        //}
        if (Input.GetButton("Cancel"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
    //void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //    }

    //}

    //void OnCollisionStay(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //    }
    //}
    //void OnCollisionExit(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = false;
    //    }
    //}

}
