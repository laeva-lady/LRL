using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField] PlayerController pcontrol;

    [SerializeField] Vector3 m_offset = new(4, 1, 0);
    [SerializeField] KeyCode[] m_rotate_keys = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    [SerializeField] float m_distance = 10;
    [SerializeField] float m_speed_angle = 30;
    [SerializeField] float m_distance_min = 0;
    [SerializeField] float m_distance_max = 0;

    Camera cam;
    GameObject player;

    void Start()
    {
        cam = pcontrol.GetCamera();
        player = pcontrol.GetPlayer();

        cam.transform.position = player.transform.position + m_offset + (-cam.transform.forward * m_distance);
    }

    void Update()
    {
        MoveCamera();
        RotateCamera();
        UpdateDistanceCamera();
    }

    void MoveCamera()
    {
        cam.transform.position = player.transform.position + m_offset + (-cam.transform.forward * m_distance);
    }

    void RotateCamera()
    {
        if (Input.GetKey(m_rotate_keys[0])) // w
            cam.transform.RotateAround(player.transform.position + m_offset, cam.transform.right, -m_speed_angle * Time.deltaTime);
        if (Input.GetKey(m_rotate_keys[1])) // a
            cam.transform.RotateAround(player.transform.position + m_offset, Vector3.up, -m_speed_angle * Time.deltaTime);
        if (Input.GetKey(m_rotate_keys[2])) // s
            cam.transform.RotateAround(player.transform.position + m_offset, cam.transform.right, m_speed_angle * Time.deltaTime);
        if (Input.GetKey(m_rotate_keys[3])) // d
            cam.transform.RotateAround(player.transform.position + m_offset, Vector3.up, m_speed_angle * Time.deltaTime);
        cam.transform.LookAt(player.transform.position + m_offset);
    }

    void UpdateDistanceCamera()
    {
        var scroll = Input.mouseScrollDelta.y;
        if (Input.GetKey(KeyCode.LeftControl))
            scroll *= 5;
        m_distance -= scroll;
        if (m_distance < m_distance_min) m_distance = m_distance_min;
        if (m_distance > m_distance_max) m_distance = m_distance_max;
    }
}
