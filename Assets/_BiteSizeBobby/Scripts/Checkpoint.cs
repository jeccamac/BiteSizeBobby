using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager gameManager;
    private Collider _collider;
    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        _collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerCharacter = other.gameObject.GetComponent<PlayerController>();

        if (playerCharacter != null)
        {
            gameManager._lastCheckpoint = this.transform.position;
            _collider.enabled = false; //disable this checkpoint after reaching it
            Debug.Log("checkpoint");
        }
    }
}
