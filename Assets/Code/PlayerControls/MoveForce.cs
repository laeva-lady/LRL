using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForce : MonoBehaviour
{
    [SerializeField] PlayerController pcontrol;
    [SerializeField] float force_strength;

    Camera cam;
    GameObject player;
    Rigidbody prigid;

    readonly List<(KeyCode, Vector3)> kvap = new List<(KeyCode, Vector3)>{
        (KeyCode.W, Vector3.forward),
        (KeyCode.A, Vector3.left),
        (KeyCode.S, Vector3.back),
        (KeyCode.D, Vector3.right),
    };

    void Start()
    {
        cam = pcontrol.GetCamera();
        player = pcontrol.GetPlayer();
        prigid = player.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 force = Vector3.zero;

        foreach (var (k, direction) in kvap)
        {
            if (Input.GetKey(k))
            {
                Vector3 camForward = cam.transform.forward;
                Vector3 camRight = cam.transform.right;

                // Ignore vertical tilt of the camera
                camForward.y = 0;
                camRight.y = 0;

                camForward.Normalize();
                camRight.Normalize();

                // Compute movement relative to camera
                Vector3 relativeDirection = direction.z * camForward + direction.x * camRight;

                force += relativeDirection;
            }
        }

        // prigid.AddForce(force_strength * force, ForceMode.Acceleration);
        if (Input.GetKey(KeyCode.LeftShift))
            prigid.AddForce(3 * force_strength * force);
        else
            prigid.AddForce(force_strength * force);


    }

}
