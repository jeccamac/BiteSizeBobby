using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float _rotateXspeed;
    public float _rotateYspeed;
    public float _rotateZspeed;

    private void Update() 
    {
        transform.Rotate(_rotateXspeed, _rotateYspeed, _rotateZspeed);
    }

}
