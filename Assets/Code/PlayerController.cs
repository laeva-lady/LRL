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
    [SerializeField] GameObject player; // aka the 'tiger'

    [Header("Cam settings")]
    [SerializeField] Camera cam;

    [Header("Agent settings")]
    [SerializeField] NavMeshAgent agent;

    [Header("Transform for the Pointers")]
    [SerializeField] Transform pointer_mouse;
    [SerializeField] GameObject pointer_clicker;

    public ref GameObject GetPlayer() => ref player;
    public ref Camera GetCamera() => ref cam;
    public ref NavMeshAgent GetAgent() => ref agent;
    public ref Transform GetPointerMouse() => ref pointer_mouse;
    public ref GameObject GetPointerClicker() => ref pointer_clicker;

    
}
