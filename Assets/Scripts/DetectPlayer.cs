using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectPlayer : MonoBehaviour
{
    public UnityEvent OnCollisionEnter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "HeadCollider")
        {
            OnCollisionEnter.Invoke();
        }
    }
}
