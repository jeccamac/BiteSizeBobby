using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour
{
    [Header("Ability Settings")]
    [SerializeField] float _shieldDuration = 5f;
    [SerializeField] Text _shieldText = null;
    [SerializeField] MeshRenderer _shield;
    [SerializeField] AudioSource _soundShield = null;
    public bool _speedActive = false;
    public bool _shieldActive = false;
    PlayerStats playerStats;

    private void Start() 
    {
        _soundShield = GetComponent<AudioSource>();
        _shield.enabled = false;
        playerStats = GetComponent<PlayerStats>();
    }

    public void ActivateShield()
    {
        StartCoroutine(ShieldSequence());
        if (_soundShield != null) { _soundShield.Play(); }
    }

    IEnumerator ShieldSequence()
    {
        _shieldActive = true;
        if (_shieldText != null) { _shieldText.enabled = true; }
        Debug.Log("start shield timer");
        ShieldActivated(true);

        //wait for the required duration
        yield return new WaitForSeconds(_shieldDuration);

        //then reset
        Debug.Log("end shield timer");
        ShieldDeactivated(false);
        
        //set boolean to release lockout
        _shieldActive = false;
        if (_shieldText != null) { _shieldText.enabled = false; }
    }

    private void ShieldActivated(bool _activeState)
    {
        playerStats._invincibility = _activeState;
        _shield.enabled = _activeState;
        Debug.Log("shield activated");
    }

    private void ShieldDeactivated(bool _activeState)
    {
        playerStats._invincibility = _activeState;
        _shield.enabled = _activeState;
        Debug.Log("shield deactivated");
    }

}
