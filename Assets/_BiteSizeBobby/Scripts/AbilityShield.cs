using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShield : MonoBehaviour
{
    private AudioSource _soundItemUse;

    private void Awake()
    {
        _soundItemUse = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        PlayerAbility playerAbility = other.gameObject.GetComponent<PlayerAbility>();

        if (playerAbility != null && playerAbility._shieldActive == false)
        {
            //reference coroutine for player ability
            playerAbility.ActivateShield();
            
            //_soundItemUse.Play();

            //destroy this object
            Destroy(gameObject, 0.1f);
        }
    }

}
