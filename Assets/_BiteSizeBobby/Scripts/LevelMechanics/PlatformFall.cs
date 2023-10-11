using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    [Header("Platform Fall Settings")]

    [Tooltip("Delay in Seconds")]
    [SerializeField] private float _destroyDelay = 2f;
    private bool onPlatform = false;

    [Header ("Animation Settings")]
    private Animator animator;
    private void Start() 
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update() 
    {
        if (onPlatform == true)
        {
            //drop platform
            animator.SetBool("canFall", true);
            //this.gameObject.SetActive(false);
            Destroy(gameObject, _destroyDelay);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onPlatform = true;
            other.transform.SetParent(transform); //set player as child of platform so that it moves with platform position & rotation
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        //onPlatform = false;
        other.transform.SetParent(null); //detach player as a child component
    }
}
