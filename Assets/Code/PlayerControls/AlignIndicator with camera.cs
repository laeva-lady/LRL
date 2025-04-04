using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignIndicatorwithcamera : MonoBehaviour
{
    [SerializeField] PlayerController pcontrol;
    [SerializeField] GameObject indicator;

    Camera cam;
    GameObject player;

    void Start()
    {
        cam = pcontrol.GetCamera();
        player = pcontrol.GetPlayer();
    }

    void Update()
    {
        var direction = cam.transform.rotation;
        var indicator_direction = direction * Quaternion.Euler(0, 360, 0);;
        var indangle = indicator_direction.eulerAngles;
        indangle.x = 0;
        indangle.z = 0;
        indicator.transform.rotation = Quaternion.Euler(indangle);
    }
}
