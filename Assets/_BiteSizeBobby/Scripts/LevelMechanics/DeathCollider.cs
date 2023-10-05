using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    GameManager gameManager;
    private int _deathScore = 300;

    private void Start() 
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.OnDeath();
            gameManager.SubtractScore(_deathScore);
        }
    }
}
