using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerScript : NetworkBehaviour
{
    [SerializeField] private float speed = 3.0f;

    [Client] // Update is called once per frame
    void Update()
    {
        if (!hasAuthority) {return;}

        if (Input.GetKeyDown(KeyCode.W)) {
            transform.Translate(new Vector3(0, 0, speed));
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            transform.Translate(new Vector3(-speed, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            transform.Translate(new Vector3(0, 0, -speed));
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            transform.Translate(new Vector3(speed, 0, 0));
        }
        
    }
}
