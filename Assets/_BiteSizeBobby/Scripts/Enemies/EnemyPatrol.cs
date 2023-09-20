using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] int _enemyHealthMax = 5;
    [SerializeField] int _enemyHealthCurrent;
    [SerializeField] int _damageHit = 1;
    [SerializeField] int _damageScore = 100;
    [SerializeField] Transform[] _patrolPoints;
    [SerializeField] float _patrolSpeed = 1f;
    [SerializeField] float _chaseSpeed = 2f;
    private float _lookRadius = 20f; // range at which enemy detects player
    private Transform _target; // dectects player to target
    private int _currentPoint;

    [Header("Sound Settings")]
    [SerializeField] AudioSource _ambientEnemy;

    private GameManager gameManager;
    private PlayerStats playerStats;

    private void Start() 
    {
        gameManager = FindObjectOfType<GameManager>();
        playerStats = FindObjectOfType<PlayerStats>();
        _enemyHealthCurrent = _enemyHealthMax;
        _target = PlayerManager.instance.player.transform;
    }

    private void Update() 
    {
        // detect player within certain range
        float distance = Vector3.Distance(_target.position, transform.position);

        // if sees player, transform
        if (distance <= _lookRadius)
        {
            ChaseTarget(); 
        } else { PatrolArea(); }

        if (_enemyHealthCurrent <= 0)
        {
            Destroy(gameObject, 0.2f);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        PlayerController playerCharacter = other.gameObject.GetComponent<PlayerController>();

        if (playerCharacter != null)
        {
            if (playerStats._invincibility == false)
            {
                playerStats.TakeDamage(_damageHit);
                gameManager.SubtractScore(_damageScore);
            }
        }

    }

    private void ChaseTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _chaseSpeed * Time.deltaTime);
    }

    private void PatrolArea()
    {
        if (transform.position != _patrolPoints[_currentPoint].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _patrolPoints[_currentPoint].position, _patrolSpeed * Time.deltaTime);
        } else { _currentPoint = (_currentPoint +1)%_patrolPoints.Length; }
    }

    public void EnemyTakeDamage (int takeDamage)
    {
        _enemyHealthCurrent -= takeDamage;
        Debug.Log("enemy health: " + _enemyHealthCurrent);
    }
}
