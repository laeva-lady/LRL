using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchBetween2Points : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] Vector3 offset = new(0, 2, 0);

    void Update()
    {
        var pa = pointA.position + offset;
        var pb = pointB.position + offset;
        // position cube
        transform.position = (pa + pb) / 2;

        // change direction
        var direction = pb - pa;
        transform.rotation = Quaternion.LookRotation(direction);

        // adjust scale
        transform.localScale = new(transform.localScale.x, transform.localScale.y, direction.magnitude);
    }
}
