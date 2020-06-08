using System.Collections;
using System.Collections.Generic;
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
    private float moveSpeed = 6.0f;
    private float rotSpeed = 90.0f;
    private Vector3 direction;

    //jump
    public bool isGrounded;
    public Rigidbody body;
    private Vector3 jumpHeight = new Vector3(0.0f, 8.0f, 0.0f);
    RaycastHit hit;

    // Start is called before the first frame update
    void Awake()
    {
        control = new Input();

        //ctx = context, can be named anything; lambda expression
        control.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        control.Gameplay.Move.canceled += ctx => movement = Vector2.zero;

        control.Gameplay.Jump.performed += ctx => Jump();
    }

    void Update()
    {

        Vector3 direction = new Vector3(movement.x, 0, movement.y) * Time.deltaTime * moveSpeed;
        direction = cam.transform.TransformDirection(direction);
        direction.y = 0.0f;
        transform.Translate(direction, Space.World);

        //Debug.Log(direction);
        if (direction != Vector3.zero)
        {
            //immediate rotation
            transform.rotation = Quaternion.LookRotation(direction);
            
            //slow rotation
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
            //0.01f);
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

        if (isGrounded)
        {
            body.AddForce(jumpHeight, ForceMode.Impulse);
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
