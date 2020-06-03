using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    private Input control;

    private GameObject player;
    private Vector3 targetPos;
    private Vector3 offset = new Vector3(0.0f, 4.0f, -8.0f);
    private Vector3 velocity = Vector3.one;

    //camera rotate
    private Vector2 cam;
    private float camSpeed = 90.0f;

    void Awake()
    {
        control = new Input();

        control.Gameplay.Camera.performed += ctx => cam = ctx.ReadValue<Vector2>();
        control.Gameplay.Camera.canceled += ctx => cam = Vector2.zero;

        player = GameObject.Find("Player");
        transform.LookAt(player.transform.position);

    }

    void LateUpdate()
    {

        //float rotate = cam.x;
        //transform.RotateAround(player.transform.position, Vector3.up, cam.x * Time.deltaTime * camSpeed);

        Quaternion rotAngle = Quaternion.AngleAxis(cam.x * Time.deltaTime * camSpeed, Vector3.up);
        offset = rotAngle * offset;
        
        targetPos = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.01f);
        transform.LookAt(player.transform.position);

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
