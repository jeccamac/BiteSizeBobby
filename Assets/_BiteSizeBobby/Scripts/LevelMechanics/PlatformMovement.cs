using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformMovement : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _moveToPoint;
    private float _elapsedTime;
    [Tooltip("Move time in seconds")]
    [SerializeField] private float _moveTime = 1f;
    private float percentComplete;

    [Header("Interpolate with Animation Curve")]
    [SerializeField] private AnimationCurve moveCurve;

    private bool onPlatform = false;

    public void FixedUpdate() 
    {
        _elapsedTime += Time.deltaTime; //count how many seconds has elapsed

        if (onPlatform == true)
        {
            percentComplete = _elapsedTime / _moveTime * Time.deltaTime; //calculate percentage of duration elapsed
            transform.position = Vector3.Lerp(_startPoint.position, _moveToPoint.position, moveCurve.Evaluate(percentComplete));
            transform.rotation = Quaternion.Lerp(_startPoint.rotation, _moveToPoint.rotation, moveCurve.Evaluate(percentComplete));
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            onPlatform = true;
            other.transform.SetParent(transform, true); //set player as child of platform so that it moves with platform position
        }
        
    }

    private void OnTriggerExit(Collider other) 
    {
        //onPlatform = false;
        other.transform.SetParent(null); //detach player as a child component
        other.transform.rotation = Quaternion.identity;
    }
}
