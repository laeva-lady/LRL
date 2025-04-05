using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] GameObject m_fireball_prefab;
    [SerializeField] GameObject m_spawn_point;
    [SerializeField] float m_initial_velocity;

    GameObject fireball_instance;
    Rigidbody fireball_rigidbody;

    public void ___Shoot()
    {
        fireball_instance =
            Instantiate(
                m_fireball_prefab,
                m_spawn_point.transform.position,
                m_spawn_point.transform.rotation
            );

        fireball_rigidbody = 
            fireball_instance.GetComponent<Rigidbody>();

        fireball_rigidbody.velocity = 
            player.GetComponent<Rigidbody>().velocity + m_spawn_point.transform.forward * m_initial_velocity;
    }

    // TODO : make fireball immobile when they touch the ground
    // void OnCollisionStay(Collision collision)
    // {
    //     if (collision.collider.gameObject.layer == 20) {
    //         fireball_rigidbody.velocity = Vector3.zero;
    //         fireball_rigidbody.useGravity = false;
    //     }
    // }


}
