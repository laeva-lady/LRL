using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPointerControl : MonoBehaviour
{

    [SerializeField] PlayerController pcontrol;
    [SerializeField] float m_pointer_clicker_max_distance = 5;
    const int mconst_indicators_layer = 15;

    Camera cam;
    GameObject player;
    GameObject pointer_clicker;

    void Start() {
        cam = pcontrol.GetCamera();
        player = pcontrol.GetPlayer();
        pointer_clicker = pcontrol.GetPointerClicker();
    }
    
    void Update()
    {
        UpdatePoint();
        MovePointer();
    }

    void UpdatePoint() {
        if (Physics.Raycast(
                        cam.ScreenPointToRay(Input.mousePosition),
                        out RaycastHit hit,
                        Mathf.Infinity,
                        ~(1 << mconst_indicators_layer)
                    ))
            {
                pointer_clicker.transform.position = hit.point;
            }
    }

    void MovePointer() {
        var pointA = player.transform.position;
            var pointB = pointer_clicker.transform.position;
            Vector3 direction = (pointB - pointA).normalized;
            var distanceAB = Vector3.Distance(pointA, pointB);

            var distance_to_use =
                distanceAB > m_pointer_clicker_max_distance ?
                    m_pointer_clicker_max_distance : distanceAB;

            // Compute the new position
            Vector3 targetPosition = pointA + direction * distance_to_use;

            // Set object's position
            pointer_clicker.transform.SetPositionAndRotation(targetPosition, Quaternion.identity);
    }
}
