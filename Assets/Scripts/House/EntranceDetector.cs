using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EntranceDetector : MonoBehaviour
{
    public bool IsEntered { get; private set; }
    public event Action CreatureDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rogue>())
        {
            IsEntered = true;

            CreatureDetected?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rogue>())
        {
            IsEntered = false;

            CreatureDetected?.Invoke();
        }
    }
}
