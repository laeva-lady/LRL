using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField] PlayerController pcontrol;

    [SerializeField] float speed_rotation = 5f; // around 0.7 is a good middle spot me thinks
    [SerializeField] float speed_follow = 5f;
    [SerializeField] float distance = 10;
    [SerializeField] float distance_min = 0;
    [SerializeField] float distance_max = 0;
    [SerializeField] Vector3 offset = new(0, 2, 0);


    Camera cam;
    GameObject player;

    private float yaw = 0f;
    private float pitch = 0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cam = pcontrol.GetCamera();
        player = pcontrol.GetPlayer();

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void Update()
    {
        Rotation();
        Move();
        Distance();
    }

    void Rotation()
    {
        yaw += Input.GetAxis("Mouse X") * speed_rotation;
        pitch -= Input.GetAxis("Mouse Y") * speed_rotation;
        pitch = Mathf.Clamp(pitch, 0f, 80f);
    }

    void Move()
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = player.transform.position + offset + rotation * direction;

        // Smoothly move camera
        // cam.transform.position = Vector3.Lerp(cam.transform.position, desiredPosition, Time.deltaTime * speed_follow);
        cam.transform.position = desiredPosition;
        cam.transform.LookAt(player.transform.position + offset); // Always look at the player
    }

    void Distance()
    {
        var scroll = Input.mouseScrollDelta.y;
        if (Input.GetKey(KeyCode.LeftControl)) scroll *= 5;
        distance -= scroll;
        distance = Mathf.Clamp(distance, distance_min, distance_max);
    }
}
