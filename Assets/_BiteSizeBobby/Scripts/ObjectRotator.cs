using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    [SerializeField] float _rotateXspeed;
    [SerializeField] float _rotateYspeed;
    [SerializeField] float _rotateZspeed;

    private void Update() 
    {
        transform.Rotate(_rotateXspeed, _rotateYspeed, _rotateZspeed);
    }

}
