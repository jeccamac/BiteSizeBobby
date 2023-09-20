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
        // if (Input.GetButton("Fire1") && _cooldownTimer > _rayCooldown && playerController._canShoot == true)
        // {
        //     ShootPellet();
        //     if (_soundRay != null) { _soundRay.Play(); }
        //     //Debug.Log("shooting");
        // }

        _cooldownTimer += Time.deltaTime; //increment timer everyframe
    }

    public void ShootPellet()
    {
        Vector3 _rayDirection = playerController.transform.forward;

        if (_cooldownTimer > _rayCooldown && playerController._canShoot == true)
        {
            _cooldownTimer = 0; //reset cooldown timer
            if (_soundRay != null) { _soundRay.Play(); }
            Debug.Log("shooting");
        }

        // fire the Raycast
        if (Physics.Raycast(_projectileLaunch.position, _rayDirection, out _objectHit)) // out objectHit gets info we stored on what the Raycast hit
        {            
            Debug.Log("You Hit " + _objectHit.transform.name); // get name of object you hit            

            if (_objectHit.transform.tag == "Enemy")
                {
                    // apply EnemyTakeDamage
                    EnemyPatrol _enemy = _objectHit.transform.gameObject.GetComponent<EnemyPatrol>();
                    if (_enemy != null)
                    {
                        Debug.Log("Detected Enemy");
                        _enemy.EnemyTakeDamage(1); //int for weapon damage
                        
                    }
                }
        }

        //if shot 0-9 is disabled, re-enable it to shoot again
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
