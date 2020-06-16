using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private GameObject cam;

    //movements
    private Input control;
    private Vector2 movement;
    private float moveSpeed = 7.0f;
    private Vector3 direction;

    //dodge roll
    private bool isDashing = false;
    private float rollTimer = 0.0f;
    private float rollCooldown = 1.0f;

    //jump
    private bool isGrounded;
    [SerializeField]
    private Rigidbody body;

    private bool isJumping;
    private float jumpForce = 7.0f;
    RaycastHit hit;

    // Start is called before the first frame update
    void Awake()
    {
        control = new Input();

        //ctx = context, can be named anything; lambda expression
        control.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        control.Gameplay.Move.canceled += ctx => movement = Vector2.zero;

        control.Gameplay.Jump.performed += ctx => Jump();
        control.Gameplay.DodgeRoll.performed += ctx => DodgeRoll();
    }

    void Update()
    {
        direction = new Vector3(movement.x, 0.0f, movement.y);
        direction = cam.transform.TransformDirection(direction);
        direction.y = 0.0f;

        if (direction != Vector3.zero)
        {
            //immediate rotation
            transform.rotation = Quaternion.LookRotation(direction);

            //slow rotation
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
            //0.01f);
        }

    }

    void FixedUpdate()
    {

        body.MovePosition(transform.position + (direction * moveSpeed * Time.fixedDeltaTime));

        if (isJumping)
        {
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
        }
        
        if (isDashing)
        {
            body.AddForce(transform.forward * 8.0f, ForceMode.Impulse);
            rollTimer = Time.fixedTime + rollCooldown;
            isDashing = false;
        }
   
    }

    void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f)){
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }

    }

    void Jump()
    {
        GroundCheck();

        if (isGrounded && !isDashing && Time.time > rollTimer) 
        {
            isJumping = true;
        }
    }

    void DodgeRoll()
    {
        GroundCheck();

        if (isGrounded && !isJumping && !isDashing && Time.time > rollTimer)
        {
            isDashing = true;
        }
    }


    private void OnEnable()
    {
        control.Enable();
    }

    private void OnDisable()
    {
        control.Disable();
    }


}
