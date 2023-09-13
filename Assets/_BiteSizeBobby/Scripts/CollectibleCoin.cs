using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCoin : MonoBehaviour
{
    [SerializeField] AudioSource _sndCollect = null;
    [SerializeField] float _rotateSpeed = 0.5f;
    public int _coinAmount = 1;
    public int _scoreAmount = 100;

    private GameManager gameManager;

    private void Start() 
    {
        _sndCollect = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update() 
    {
        transform.Rotate(0, _rotateSpeed, 0);
    }

    private void OnTriggerEnter(Collider other) 
    {
        PlayerController playerCharacter = other.gameObject.GetComponent<PlayerController>();

        if (playerCharacter != null)
        {
            //_sndCollect.Play();

            gameManager.AddScore(_scoreAmount);
            gameManager.AddCoins(_coinAmount);

            Destroy(gameObject, 0.1f);
        }
    }
}
