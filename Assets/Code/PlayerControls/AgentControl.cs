using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentControl : MonoBehaviour
{
    [SerializeField] PlayerController pcontrol;

    Camera m_cam;
    NavMeshAgent m_agent;
    Transform m_pointer_mouse;

    const int mconst_indicators_layer = 15;

    void Start()
    {
        m_cam = pcontrol.GetCamera();
        m_agent = pcontrol.GetAgent();
        m_pointer_mouse = pcontrol.GetPointerMouse();
    }

    void Update()
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
}
