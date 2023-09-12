using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour
{
    [Header("Ability Settings")]
    [SerializeField] float _shieldDuration = 5f;
    //[SerializeField] Text _shieldText = null;
    [SerializeField] MeshRenderer _shield;
    //[SerializeField] AudioSource _soundShield = null;
    public bool _speedActive = false;
    public bool _shieldActive = false;
    PlayerStats playerStats;

    private void Awake() 
    {
        //_soundShield = GetComponent<AudioSource>();
        _shield.enabled = false;
        playerStats = GetComponent<PlayerStats>();
    }

    public void ActivateShield()
    {
        StartCoroutine(ShieldSequence());
        //_soundShield.Play();
    }

    IEnumerator ShieldSequence()
    {
        _shieldActive = true;
        //_shieldText.enabled = true;
        Debug.Log("start shield timer");
        ShieldActivated(true);

        //wait for the required duration
        yield return new WaitForSeconds(_shieldDuration);

        //then reset
        Debug.Log("end shield timer");
        ShieldDeactivated(false);
        
        //set boolean to release lockout
        _shieldActive = false;
        //_shieldText.enabled = false;
    }

    private void ShieldActivated(bool activeState)
    {
        playerStats._invincibility = activeState;
        _shield.enabled = activeState;
        Debug.Log("shield activated");
    }

    private void ShieldDeactivated(bool activeState)
    {
        playerStats._invincibility = activeState;
        _shield.enabled = activeState;
        Debug.Log("shield deactivated");
    }

}
