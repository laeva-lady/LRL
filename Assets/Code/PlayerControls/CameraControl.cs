using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField] PlayerController pcontrol;

    [SerializeField] float speed_angle = 0.7f; // around 0.7 is a good middle spot me thinks
    [SerializeField] float distance = 10;
    [SerializeField] float distance_min = 0;
    [SerializeField] float distance_max = 0;

    Camera cam;
    GameObject player;

    Vector3 offset;
    Vector3 top_of_player;

    void Start()
    {
        cam = pcontrol.GetCamera();
        player = pcontrol.GetPlayer();

        cam.transform.rotation = Quaternion.Euler(90, 0, 0);

        offset = Vector3.up * distance;
        top_of_player = player.transform.position + offset;
        cam.transform.position = top_of_player;
    }

    void Update()
    {
        Rotation();
        Move();
        Distance();
    }

    void Rotation() {
        cam.transform.LookAt(player.transform);
    }

    void Move()
    {
        top_of_player = player.transform.position + offset;
        cam.transform.position = Vector3.Lerp(cam.transform.position, top_of_player, speed_angle * Time.deltaTime);
    }

    void Distance()
    {
        var scroll = Input.mouseScrollDelta.y;
        if (Input.GetKey(KeyCode.LeftControl))
            scroll *= 5;
        distance -= scroll;
        if (distance < distance_min) distance = distance_min;
        if (distance > distance_max) distance = distance_max;

        offset = Vector3.up * distance;
    }
}
