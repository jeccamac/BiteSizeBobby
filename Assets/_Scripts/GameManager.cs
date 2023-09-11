using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int _score = 0;
    [SerializeField] Text _scoreText = null;

    public void Update()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        //get text and update
        _scoreText.text = "Score: " + _score;
    }

    public void AddScore(int addScore)
    {
        _score += addScore;
    }

    public void SubtractScore(int subScore)
    {
        _score -= subScore;
        _score = Mathf.Clamp(_score, 0, 100000);
        Debug.Log("subtracting score");
    }
}
