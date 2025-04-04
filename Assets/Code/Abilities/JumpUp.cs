using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUp : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float force;
    [SerializeField] float groundCheckDistance = 0.1f;
    [SerializeField] LayerMask groundLayer;

    public void Jump()
    {
        if (!IsGrounded()) return;
        player.GetComponent<Rigidbody>().AddForce(new Vector3(0, force, 0));
    }

    bool IsGrounded()
    {
        return Physics.Raycast(player.transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
}
