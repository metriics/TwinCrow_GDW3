using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private Input control;
    private Vector2 movement;
    private float moveSpeed = 4.0f;

    //height
    private float jump;
    private float jumpHeight = 3.0f;

    private Vector2 cam;
    private float rotSpeed = 90.0f;

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
        transform.Translate(direction, Space.World);
    }

    void Jump()
    {
        Debug.Log("Jump");
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
