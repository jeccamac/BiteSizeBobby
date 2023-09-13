using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShield : MonoBehaviour
{
    private AudioSource _sndItemUse;

    private void Awake()
    {
        _sndItemUse = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        PlayerAbility playerAbility = other.gameObject.GetComponent<PlayerAbility>();

        if (playerAbility != null && playerAbility._shieldActive == false)
        {
            //reference coroutine for player ability
            playerAbility.ActivateShield();
            if (_sndItemUse != null) { _sndItemUse.Play(); }

            //destroy this object
            Destroy(gameObject, 0.1f);
        }
    }

}
