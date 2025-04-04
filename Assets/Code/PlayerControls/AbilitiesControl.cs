using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AbilitiesControl : MonoBehaviour
{
    [SerializeField] List<KeyEvent> keyEvents = new();

    void Update()
    {
        foreach (var ke in keyEvents) {
            if (Input.GetKey(ke.key)) {
                ke.evnt.Invoke();
            }
        }
    }

    [Serializable]
    struct KeyEvent {
        public KeyCode key;
        public UnityEvent evnt;
    }
}
