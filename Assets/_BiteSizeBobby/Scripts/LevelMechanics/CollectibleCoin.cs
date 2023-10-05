using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCoin : MonoBehaviour
{
    [Header("Collectible Settings")]
    [SerializeField] float _rotateSpeed = 0.5f;
    public int _coinAmount = 1;
    public int _scoreAmount = 100;

    [Header("Sound Settings")]
    [SerializeField] AudioSource _ambientCoin = null;
    [SerializeField] AudioSource _soundCollect = null;
    
    private GameManager gameManager;

    private void Start() 
    {
        gameManager = FindObjectOfType<GameManager>();
        _ambientCoin = GetComponent<AudioSource>();
        _soundCollect = GetComponent<AudioSource>();
    }
    private void Update() 
    {
        transform.Rotate(0, _rotateSpeed, 0);
        if (_ambientCoin != null ) { _ambientCoin.Play(); }
    }

    private void OnTriggerEnter(Collider other) 
    {
        PlayerController playerCharacter = other.gameObject.GetComponent<PlayerController>();

        if (playerCharacter != null)
        {
            if (_soundCollect != null) { _soundCollect.Play(); }

            gameManager.AddScore(_scoreAmount);
            gameManager.AddCoins(_coinAmount);
            
            Destroy(gameObject, 0.1f);
        }
    }
}
