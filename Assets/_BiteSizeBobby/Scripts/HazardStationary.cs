using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class HazardStationary : MonoBehaviour
{
    [Header("Hazard Settings")]
    [SerializeField] int _damageHit = 1;
    [SerializeField] int _damageScore = 100;

    [Header("Sound Settings")]
    [SerializeField] AudioSource _ambientHazard;
    [SerializeField] AudioSource _soundDamage;
    
    private GameManager gameManager;
    private PlayerStats playerStats;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerStats = FindObjectOfType<PlayerStats>();
        _ambientHazard = GetComponent<AudioSource>();
        _soundDamage = GetComponent<AudioSource>();
    }
    private void Start() 
    {
        if (_ambientHazard != null) { _ambientHazard.Play(); }
    }
    private void OnTriggerEnter(Collider other) {
        PlayerController playerCharacter = other.gameObject.GetComponent<PlayerController>();
        //Debug.Log("object is colliding");

        if (playerCharacter != null ) 
        {
            if (playerStats._invincibility == false)
            {
                playerStats.TakeDamage(_damageHit);
                if (_soundDamage != null) { _soundDamage.Play(); }
                gameManager.SubtractScore(_damageScore);

            } else { Debug.Log("invincible!"); }
            
        }
    }
}
