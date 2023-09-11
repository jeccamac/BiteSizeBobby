using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardStationary : MonoBehaviour
{
    [SerializeField] int _damageHit = 1;
    [SerializeField] int _damageScore = 100;
    //[SerializeField] public GameObject playerCharacter;

    public GameManager gameManager;
    public PlayerStats playerStats;
    

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other) {
        PlayerController playerCharacter = other.gameObject.GetComponent<PlayerController>();
        //Debug.Log("object is colliding");

        if (playerCharacter != null) 
        {
            playerStats.TakeDamage(_damageHit);

            gameManager.SubtractScore(_damageScore);
            //Debug.Log("is colliding with player");
        }
    }
}