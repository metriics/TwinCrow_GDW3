using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private Input control;
    private Vector2 movement;
    private float moveSpeed = 4.0f;

    // Start is called before the first frame update
    void Awake()
    {
        control = new Input();

        //ctx = context, can be named anything; lambda expression
        control.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        control.Gameplay.Move.canceled += ctx => movement = Vector2.zero;
    }

    void Update()
    {
       // Vector2 direction = new Vector2(movement.x, movement.y) * Time.deltaTime * moveSpeed;
        Vector3 direction = new Vector3(movement.x, 0, movement.y) * Time.deltaTime * moveSpeed;
        transform.Translate(direction, Space.World);
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
