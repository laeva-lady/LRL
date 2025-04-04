using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    [Header("Player settings")]
    [SerializeField] GameObject player; // aka the 'tiger'

    [Header("Cam settings")]
    [SerializeField] Camera m_cam;
    [SerializeField] KeyCode[] m_rotate_keys = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    [SerializeField] float m_speed_angle = 30;
    [SerializeField] float m_distance = 10;
    [Range(5, 995)]
    [SerializeField] float m_distance_min = 0;
    [Range(10, 1000)]
    [SerializeField] float m_distance_max = 0;
    [SerializeField] Vector3 m_offset = new(4, 1, 0);

    [Header("Agent settings")]
    [SerializeField] NavMeshAgent m_agent;
    [SerializeField] float m_walk_speed = 3.5f;
    [SerializeField] float m_run_speed_scale = 2.3f;
    private float m_run_speed;

    [Header("Run settings")]
    [SerializeField] KeyCode m_run_key = KeyCode.LeftShift;
    [SerializeField] float m_run_cooldown_s = 20;
    [SerializeField] float m_run_duration_s = 10;
    private float m_time_spent_running = 0;
    private float m_time_spent_cooldown = 0;
    private bool m_is_running = false;
    private bool m_is_cooldown = false;

    [Header("Transform for the Pointers")]
    [SerializeField] Transform m_pointer_mouse;
    [SerializeField] GameObject m_pointer_clicker;
    [SerializeField] float m_pointer_clicker_max_distance = 5;

    [Header("Abilities Events")]
    [SerializeField] KeyCode[] m_abilities_keycodes;
    [SerializeField] UnityEvent[] m_abilities_events;

    const int mconst_indicators_layer = 15;

    void Start()
    {
        m_run_speed = m_walk_speed * m_run_speed_scale;
        m_cam.transform.position = player.transform.position + m_offset + (-m_cam.transform.forward * m_distance);
    }

    void Update()
    {
        var deltaTime = Time.deltaTime;

        local_move_agent();
        local_move_camera();
        local_rotate_camera();
        local_change_camera_distance();
        local_handle_run();
        local_update_click_pointer();
        local_move_click_pointer();
        local_handle_abilities();


        void local_change_camera_distance()
        {
            var scroll = Input.mouseScrollDelta.y;
            if (Input.GetKey(KeyCode.LeftControl))
                scroll *= 5;
            m_distance -= scroll;
            if (m_distance < m_distance_min) m_distance = m_distance_min;
            if (m_distance > m_distance_max) m_distance = m_distance_max;
        }
        void local_rotate_camera()
        {
            if (Input.GetKey(m_rotate_keys[0])) // w
                m_cam.transform.RotateAround(player.transform.position + m_offset, m_cam.transform.right, -m_speed_angle * deltaTime);
            if (Input.GetKey(m_rotate_keys[1])) // a
                m_cam.transform.RotateAround(player.transform.position + m_offset, Vector3.up, -m_speed_angle * deltaTime);
            if (Input.GetKey(m_rotate_keys[2])) // s
                m_cam.transform.RotateAround(player.transform.position + m_offset, m_cam.transform.right, m_speed_angle * deltaTime);
            if (Input.GetKey(m_rotate_keys[3])) // d
                m_cam.transform.RotateAround(player.transform.position + m_offset, Vector3.up, m_speed_angle * deltaTime);
            m_cam.transform.LookAt(player.transform.position + m_offset);
        }
        void local_move_camera()
        {
            m_cam.transform.position = player.transform.position + m_offset + (-m_cam.transform.forward * m_distance);
        }
        void local_move_agent()
        {
            if (Input.GetMouseButtonDown(1) &&
                Physics.Raycast(
                        m_cam.ScreenPointToRay(Input.mousePosition),
                        out RaycastHit hit,
                        Mathf.Infinity,
                        ~(1 << mconst_indicators_layer)
                    )
                )
            {
                m_agent.SetDestination(hit.point);
                m_pointer_mouse.position = hit.point;
            }
        }
        void local_update_click_pointer()
        {
            if (Physics.Raycast(
                        m_cam.ScreenPointToRay(Input.mousePosition),
                        out RaycastHit hit,
                        Mathf.Infinity,
                        ~(1 << mconst_indicators_layer)
                    ))
            {
                m_pointer_clicker.transform.position = hit.point;
            }
        }
        void local_move_click_pointer()
        {
            var pointA = player.transform.position;
            var pointB = m_pointer_clicker.transform.position;
            Vector3 direction = (pointB - pointA).normalized;
            var distanceAB = Vector3.Distance(pointA, pointB);

            var distance_to_use =
                distanceAB > m_pointer_clicker_max_distance ?
                    m_pointer_clicker_max_distance : distanceAB;

            // Compute the new position
            Vector3 targetPosition = pointA + direction * distance_to_use;

            // Set object's position
            m_pointer_clicker.transform.SetPositionAndRotation(targetPosition, Quaternion.identity);
        }
        void local_handle_run()
        { // doesn't seem to work after first use 🤔
            if (Input.GetKeyDown(m_run_key) && !m_is_cooldown)
            {
                m_is_running = true;

                m_agent.speed = m_run_speed;
            }

            if (m_time_spent_running > m_run_duration_s)
            {
                m_is_running = false;
                m_time_spent_running = 0;
                m_is_cooldown = true;

                m_agent.speed = m_walk_speed;
            }
            if (m_time_spent_cooldown > m_run_cooldown_s)
            {
                m_is_cooldown = false;
                m_time_spent_cooldown = 0;
            }

            if (m_is_running)
                m_time_spent_running += deltaTime;
            if (m_is_cooldown)
                m_time_spent_cooldown += deltaTime;
        }
        void local_handle_abilities()
        {
            foreach (var (key, evnt) in m_abilities_keycodes.Zip(m_abilities_events, (a, b) => (a, b)))
            {
                if (Input.GetKeyDown(key))
                    evnt.Invoke();
            }
        }

        // TODO : change movement and camera to use this :

        // void local_move_but_navmesh_with_vectors_stuff()
        // {
        //     Vector3 movement = Vector3.zero;

        //     // Check for movement keys
        //     for (int i = 0; i < keys.Length; i++)
        //     {
        //         if (Input.GetKey(keys[i]))
        //         {
        //             movement += directions[i];
        //         }
        //     }

        //     if (movement != Vector3.zero)
        //     {
        //         // Normalize movement to prevent faster diagonal movement
        //         movement.Normalize();

        //         // Convert movement to camera relative direction
        //         Vector3 cameraForward = cameraTransform.forward;
        //         Vector3 cameraRight = cameraTransform.right;

        //         // Ignore vertical components (if the game is 2D or flat)
        //         cameraForward.y = 0;
        //         cameraRight.y = 0;

        //         cameraForward.Normalize();
        //         cameraRight.Normalize();

        //         // Calculate camera-relative movement direction
        //         Vector3 cameraRelativeMovement = (cameraForward * movement.z + cameraRight * movement.x).normalized;
        //         Vector3 targetPosition;

        //         if (Input.GetKey(KeyCode.LeftShift))
        //             targetPosition = transform.position + cameraRelativeMovement * 2 * speed * Time.deltaTime;

        //         else
        //             targetPosition = transform.position + cameraRelativeMovement * speed * Time.deltaTime;

        //         // Check if target position is on NavMesh
        //         NavMeshHit navHit;
        //         if (NavMesh.SamplePosition(targetPosition, out navHit, 1.0f, NavMesh.AllAreas))
        //         {
        //             // Move the player to the valid NavMesh position
        //             transform.position = navHit.position;

        //             // Rotate the player to face the movement direction
        //             Quaternion targetRotation = Quaternion.LookRotation(cameraRelativeMovement);
        //             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        //         }
        //         else
        //         {
        //             print("Target position is not on the NavMesh.");
        //         }
        //     }
        // }
    }
}
