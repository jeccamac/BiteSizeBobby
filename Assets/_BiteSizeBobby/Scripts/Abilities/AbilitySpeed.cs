using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpeed : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        PlayerAbility playerAbility = other.gameObject.GetComponent<PlayerAbility>();

        if (playerAbility != null && playerAbility._speedActive == false)
        {
            //reference coroutine for player ability
            playerAbility.ActivateSpeed();

            //destroy this object
            Destroy(gameObject, 0.1f);
        }
    }

}
