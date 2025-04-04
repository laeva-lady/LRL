using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class RunControl : MonoBehaviour
{

    [SerializeField] PlayerController pcontrol;

    private float m_time_spent_running = 0;
    private float m_time_spent_cooldown = 0;
    private bool m_is_running = false;
    private bool m_is_cooldown = false;
    private float m_run_speed;
    [SerializeField] float m_run_speed_scale = 2.3f;
    [SerializeField] float m_walk_speed = 3.5f;
    [SerializeField] float m_run_duration_s = 10;
    [SerializeField] float m_run_cooldown_s = 20;
    [SerializeField] KeyCode m_run_key = KeyCode.LeftShift;

    NavMeshAgent m_agent;

    void Start()
    {
        m_agent = pcontrol.GetAgent();
        m_run_speed = m_walk_speed * m_run_speed_scale;
    }

    void Update()
    {
        HandleRun();
    }

    void HandleRun()
    {
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
            m_time_spent_running += Time.deltaTime;
        if (m_is_cooldown)
            m_time_spent_cooldown += Time.deltaTime;
    }

    
}
