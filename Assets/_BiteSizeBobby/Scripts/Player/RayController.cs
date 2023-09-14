using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class RayController : MonoBehaviour
{
    [Header("Raygun Settings")]

    [Tooltip("The start position of the ray to fire projectile")]
    [SerializeField] private Transform _projectileLaunch;
    [Tooltip("Get array of shots, object pooling instead of instantiate/destroy for better performance")]
    [SerializeField] private GameObject[] _shotPellets;

    [Tooltip("How many seconds before firing the next shot")]
    [SerializeField] float _rayCooldown = 0.25f;
    [Tooltip("Check if enough time has passed before firing")]
    [SerializeField] float _cooldownTimer = Mathf.Infinity; //starts so the player can shoot right away
    RaycastHit _objectHit; //stores info about what object the raycast hit
    private float _lastShootTime;

    [Header("Sound Settings")]
    private AudioSource _soundRay = null;

    PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        _soundRay = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        if (Input.GetButton("Fire1") && _cooldownTimer > _rayCooldown && playerController._canShoot == true)
        {
            ShootPellet();
            if (_soundRay != null) { _soundRay.Play(); }
            //Debug.Log("shooting");
        }

        _cooldownTimer += Time.deltaTime; //increment timer everyframe
    }

    private void ShootPellet()
    {
        _cooldownTimer = 0; //reset cooldown timer

        _shotPellets[FindShot(_shotPellets)].transform.position = _projectileLaunch.position; //shot pellets from this position
        _shotPellets[FindShot(_shotPellets)].GetComponent<ShotPellet>().SetDirection(Mathf.Sign(transform.localScale.x)); //set direction the same as the player is facing
        
    }

    private int FindShot(GameObject[] shotType)
    {
        for (int i = 0; i < shotType.Length; i++)
        {
            if ( !shotType[i].activeInHierarchy ) //if the shots are not active in the hierarchy, then re-use them by re-activating it again
            {
                return i;
            }
        }
        return 0;
    }

}
