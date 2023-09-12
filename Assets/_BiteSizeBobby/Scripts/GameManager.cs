using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Point Settings")]
    [SerializeField] Text _scoreText = null;
    [SerializeField] Text _coinText = null;
    public int _score = 0;
    public int _coins = 0;

    [Header("Restart Settings")]
    [SerializeField] float _restartDelay = 2f;

    public void Update()
    {
        UpdateScore();
        UpdateCollectible();

        if (Input.GetKeyDown(KeyCode.Backspace)) //this is temporary, just for testing. Add actual pause menu in future
        {
            ReloadLevel();
        }
    }

    public void UpdateScore()
    {
        _scoreText.text = "Score: " + _score;
    }

    public void UpdateCollectible()
    {
        _coinText.text = $"{_coins}";
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
            
            //restart level with all stats reset
            Debug.Log("restarting level");
            ReloadLevel(); //load the current level
        }
    }
    public void ReloadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
        Debug.Log("sceneName to load: " + activeSceneIndex);
    }
}

