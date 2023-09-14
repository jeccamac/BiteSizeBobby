using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShotPellet : MonoBehaviour
{
    [SerializeField] private float _shotSpeed = 20f;
    [SerializeField] private float _hitDelay = 0.2f; //how long until shot disappears
    private float _shotDirection;
    private bool _shotHit;
    [Tooltip("How many seconds projectile is active before deactivating")]
    [SerializeField] private float _shotLifetime = 5f;
    private float _lifetimeTimer;
    private Collider _shotCollider;

    private void Awake() 
    {
        _shotCollider = GetComponent<Collider>();
    }

    private void Update() 
    {
        if (_shotHit) return;
        float _movementSpeed = _shotSpeed * Time.deltaTime * _shotDirection;
        transform.Translate(_movementSpeed, 0, 0); //move shot on the x axis

        _lifetimeTimer += Time.deltaTime; //timer
        if (_lifetimeTimer > _shotLifetime) { gameObject.SetActive(false); }
    }

    private void OnTriggerEnter(Collider other) 
    {
        _shotHit = true;
        _shotCollider.enabled = false; //disable collider
        //visual feedback here for when shot hits something
        Destroy(gameObject, _hitDelay);
    }

    public void SetDirection (float _direction) //fire left or right
    {
        //reset the state of the projectile
        _lifetimeTimer = 0;
        _shotDirection = _direction;
        gameObject.SetActive(true);
        _shotHit = false;
        _shotCollider.enabled = true;
    }

    private void DeactivatePellet()
    {
        gameObject.SetActive(false);
    }
}
