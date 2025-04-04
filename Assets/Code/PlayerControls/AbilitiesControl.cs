using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AbilitiesControl : MonoBehaviour
{
    [SerializeField] KeyCode[] m_abilities_keycodes;
    [SerializeField] UnityEvent[] m_abilities_events;

    void Update()
    {
        foreach (var (key, evnt) in m_abilities_keycodes.Zip(m_abilities_events, (a, b) => (a, b)))
        {
            if (Input.GetKeyDown(key))
                evnt.Invoke();
        }
    }
}
