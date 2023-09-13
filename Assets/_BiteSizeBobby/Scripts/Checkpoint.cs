using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager gameManager;
    private Collider _collider;

    [Header("Sound Settings")]
    [SerializeField] AudioSource _soundCheckpoint;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        _collider = GetComponent<Collider>();
        _soundCheckpoint = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerCharacter = other.gameObject.GetComponent<PlayerController>();

        if (playerCharacter != null)
        {
            gameManager._lastCheckpoint = this.transform.position;
            if (_soundCheckpoint != null) { _soundCheckpoint.Play(); }
            _collider.enabled = false; //disable this checkpoint after reaching it
            Debug.Log("checkpoint");
        }
    }
}
