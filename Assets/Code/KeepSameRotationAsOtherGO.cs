using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepSameRotationAsOtherGO : MonoBehaviour
{
    [SerializeField] GameObject go;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = go.transform.rotation;
    }
}
