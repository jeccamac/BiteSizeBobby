using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [Header("Scoreboard Settings")]
    [SerializeField] Text _scoreText = null;
    [SerializeField] Text _coinText = null;
    public int _score = 0;
    public int _coins = 0;

    [Header("Restart Settings")]
    [SerializeField] float _restartDelay = 2f;
    public Vector3 _lastCheckpoint;

    [Header("Sound Settings")]
    [SerializeField] AudioSource _soundReload;
    
    private PlayerController playerController;
    private PlayerStats playerStats;

    private void Awake() //initialize this instance
    {
        if (instance == null)
        {
            instance = this; //create a game manager
            DontDestroyOnLoad(instance); //don't destroy itself in btwn scenes
        } else { Destroy(gameObject); } //don't make multiple game managers in the scene

    }

    private void Start() //initialize other intances when this instance is done initializing
    {
        playerController = FindObjectOfType<PlayerController>();
        playerStats = FindObjectOfType<PlayerStats>();
        _soundReload = GetComponent<AudioSource>();
    }
    public void Update()
    {
        UpdateScore();
        UpdateCollectible();

        if (Input.GetKeyDown(KeyCode.Backspace)) //pressing backspace is temporary, just for testing. Add actual pause menu in future
        {
            if (_soundReload != null) { _soundReload.Play(); }
            RestartLevel();
        }
    }

    public void UpdateScore()
    {
        if (_scoreText != null) { _scoreText.text = "Score: " + _score; }
    }

    public void UpdateCollectible()
    {
    if (_coinText != null) { _coinText.text = $"{_coins}"; }
    }

    public void AddScore(int addScore)
    {
        _score += addScore;
    }

    public void SubtractScore(int subScore)
    {
        _score -= subScore;
        _score = Mathf.Clamp(_score, 0, 100000); //clamp so it only shows 0 to 100,000 points and no negative numbers
        Debug.Log("subtracting score");
    }

    public void AddCoins(int addCoins)
    {
        _coins += addCoins;
        _coins = Mathf.Clamp(_coins, 0, 100000);
        Debug.Log("collected coins");
    }

    public void DeathReload()
    {
        StartCoroutine(DeathSequence());

        IEnumerator DeathSequence()
        {            
            //wait for seconds before restarting
            yield return new WaitForSeconds(_restartDelay);
            
            //reload level with some stats reset
            Debug.Log("load from last checkpoint");
            if (_soundReload != null) { _soundReload.Play(); }
            playerController.transform.position = _lastCheckpoint; //load from last checkpoint
            playerStats.AddHealth(3);//reset health stats
            playerController.gameObject.SetActive(true); //load player character
            //RestartLevel();
        }
    }
    public void RestartLevel() //restart the CURRENT level
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
        Debug.Log("sceneName to load: " + activeSceneIndex);
    }
}

