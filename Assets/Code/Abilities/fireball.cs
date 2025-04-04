using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    [SerializeField] GameObject m_fireball_prefab;
    [SerializeField] GameObject m_spawn_point;
    [SerializeField] float m_initial_velocity;

    public void ___Shoot()
    {
        GameObject fireballInstance =
            Instantiate(
                m_fireball_prefab,
                m_spawn_point.transform.position,
                m_spawn_point.transform.rotation
            );

        var rb = fireballInstance.GetComponent<Rigidbody>();

        rb.velocity = m_spawn_point.transform.forward * m_initial_velocity;
    }


}
