using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [SerializeField] GameObject m_fireball_prefab;
    [SerializeField] GameObject m_spawn_point;
    [SerializeField] float m_initial_velocity;

    [SerializeField] float cooldown;
    float cooldownTimer;
    bool can_shoot = true;

    void Update()
    {
        if (!can_shoot)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldown)
            {
                can_shoot = true;
                cooldownTimer = 0f;
            }
        }
    }

    public void ___Shoot()
    {
        if (!can_shoot) return;
        
        GameObject fireballInstance =
            Instantiate(
                m_fireball_prefab,
                m_spawn_point.transform.position,
                m_spawn_point.transform.rotation
            );

        var rb = fireballInstance.GetComponent<Rigidbody>();

        rb.velocity = m_spawn_point.transform.forward * m_initial_velocity;

        can_shoot = false;
    }


}
