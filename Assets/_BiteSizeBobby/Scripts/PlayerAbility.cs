using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour
{
    [Header("Ability Settings")]
    [SerializeField] float _shieldDuration = 5f;
    [SerializeField] float _speedDuration = 5f;

    [Tooltip("This is a speed multiplier")]
    [SerializeField] float _boosterSpeed = 1.5f;

    [Header("Ability Visuals")]
    [SerializeField] MeshRenderer _shield = null;
    [SerializeField] TrailRenderer _boosters = null;
    public bool _speedActive = false;
    public bool _shieldActive = false;

    [Header("Sound Settings")]
    [SerializeField] AudioSource _soundShield = null;
    [SerializeField] AudioSource _soundSpeed = null;

    private PlayerController playerController;
    private PlayerStats playerStats;
    private GameManager gameManager;

    private void Start() 
    {
        gameManager = FindObjectOfType<GameManager>();
        playerController = GetComponent<PlayerController>();
        playerStats = GetComponent<PlayerStats>();
        _soundShield = GetComponent<AudioSource>();
        _soundSpeed = GetComponent<AudioSource>();
        _shield.enabled = false; //visual feedback for shield
        _boosters.enabled = false; //visual feedback for speed
    }

    public void ActivateShield()
    {
        StartCoroutine(ShieldSequence());
        if (_soundShield != null) { _soundShield.Play(); }
    }

    public void ActivateSpeed()
    {
        StartCoroutine(SpeedSequence());
        if (_soundSpeed != null) { _soundSpeed.Play(); }
    }

    IEnumerator ShieldSequence()
    {
        _shieldActive = true;
        gameManager.UpdateObjective("Shield Activated for " + _shieldDuration + " seconds", _shieldDuration);
        Debug.Log("start shield timer");
        ShieldActivated(true);

        //wait for the required duration
        yield return new WaitForSeconds(_shieldDuration);

        //then reset
        gameManager.UpdateObjective("Shield Deactivated", 1f);
        Debug.Log("end shield timer");
        ShieldDeactivated(false);
        
        //set boolean to release lockout
        _shieldActive = false;
    }

    IEnumerator SpeedSequence()
    {
        _speedActive = true;
        gameManager.UpdateObjective("Speed Boosters Activated for " + _speedDuration + " seconds", _speedDuration);
        Debug.Log("start speed timer");
        SpeedActivated(true);

        //wait for the required duration
        yield return new WaitForSeconds(_speedDuration);

        //then reset
        gameManager.UpdateObjective("Speed Boost Disabled", 1f);
        Debug.Log("end speed timer");
        SpeedDeactivated(false);

        //set boolean to release lockout
        _speedActive = false;
    }

    private void ShieldActivated(bool _activeState)
    {
        playerStats._invincibility = _activeState;
        if (_shield != null) { _shield.enabled = _activeState; }
        Debug.Log("shield activated");
    }

    private void ShieldDeactivated(bool _activeState)
    {
        playerStats._invincibility = _activeState;
        if (_shield != null) { _shield.enabled = _activeState; }
        Debug.Log("shield deactivated");
    }

    private void SpeedActivated(bool activeState)
    {
        playerController._moveSpeed *= _boosterSpeed;
        if (_boosters != null) { _boosters.enabled = activeState; }
        Debug.Log("movespeed: " + playerController._moveSpeed);
    }

    private void SpeedDeactivated(bool activeState)
    {
        playerController._moveSpeed = 12f; //reset
        if (_boosters != null) { _boosters.enabled = activeState; }
    }

}
