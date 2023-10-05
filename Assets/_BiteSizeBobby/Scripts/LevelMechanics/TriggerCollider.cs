using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TriggerCollider : MonoBehaviour
{
    [SerializeField] public UnityEvent invokeFunction;
    private void OnTriggerEnter(Collider other) 
    {
        invokeFunction.Invoke();
    }
}
