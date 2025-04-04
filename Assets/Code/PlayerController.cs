using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Player gameobgect")]
    [SerializeField] GameObject player;

    [Header("Cam settings")]
    [SerializeField] Camera cam;


    public ref GameObject GetPlayer() => ref player;
    public ref Camera GetCamera() => ref cam;
}
