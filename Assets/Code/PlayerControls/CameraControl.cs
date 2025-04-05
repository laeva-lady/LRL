using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField] PlayerController pcontrol;

    [SerializeField] float speed_rotation = 5f; // around 0.7 is a good middle spot me thinks
    [SerializeField] float distance = 10;
    [SerializeField] float distance_min = 0;
    [SerializeField] float distance_max = 0;
    [SerializeField] float vertical_offset = 2;

    [SerializeField] float min_camera_angle = -30;
    [SerializeField] float max_camera_angle = 89;


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
        pitch = Mathf.Clamp(pitch, min_camera_angle, max_camera_angle);
    }

    void Move()
    {
        Vector3 direction = new(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = player.transform.position + Vector3.up * vertical_offset + rotation * direction;

        // Smoothly move camera
        // cam.transform.position = Vector3.Lerp(cam.transform.position, desiredPosition, Time.deltaTime * speed_follow);
        cam.transform.position = desiredPosition;
        cam.transform.LookAt(player.transform.position + Vector3.up * vertical_offset); // Always look at the player
    }

    void Distance()
    {
        var scroll = Input.mouseScrollDelta.y;
        if (Input.GetKey(KeyCode.LeftControl)) scroll *= 5;
        distance -= scroll;
        distance = Mathf.Clamp(distance, distance_min, distance_max);
    }
}
