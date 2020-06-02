using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    //private GameObject player = GameObject.Find("Player");
    //
    //Input control;
    //Vector2 movement;
    //Vector3 playerLoc;
    //
    //// Start is called before the first frame update
    //void Awake()
    //{
    //    control = new Input();
    //
    //    //ctx = context, can be named anything; lambda expression
    //    control.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
    //    control.Gameplay.Move.canceled += ctx => movement = Vector2.zero;
    //}
    //
    //void Update()
    //{
    //   
    //
    //}
    //
    //private void OnEnable()
    //{
    //    control.Enable();
    //}
    //
    //private void OnDisable()
    //{
    //    control.Disable();
    //}

    private GameObject player;
    private Vector3 pos;
    private Vector3 offset = new Vector3(0.0f, 4.0f, -8.0f);

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void LateUpdate()
    {
        pos = player.GetComponent<Transform>().position;
        transform.position = pos + offset;
    }

}
