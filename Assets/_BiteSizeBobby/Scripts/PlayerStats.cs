using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerStats : MonoBehaviour
{
    [Header("Health Stats")]
    [SerializeField] int _health = 3;
    [SerializeField] int _maxHealth = 3;
    public bool _invincibility = false;

    [Header("Health Display Assets")]
    public ImageAnimation _healthIcon1;
    public ImageAnimation _healthIcon1_empty;
    public ImageAnimation _healthIcon2;
    public ImageAnimation _healthIcon2_empty;
    public ImageAnimation _healthIcon3;
    public ImageAnimation _healthIcon3_empty;

    public void Update() 
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        //get health img components
        switch (_health)
        {
            case 0:
                {
                    _healthIcon1.gameObject.SetActive(false);
                    _healthIcon1_empty.gameObject.SetActive(true);
                    _healthIcon2.gameObject.SetActive(false);
                    _healthIcon2_empty.gameObject.SetActive(true);
                    _healthIcon3.gameObject.SetActive(false);
                    _healthIcon3_empty.gameObject.SetActive(true);
                    break;
                }

            case 1:
                {
                    _healthIcon1.gameObject.SetActive(true);
                    _healthIcon1_empty.gameObject.SetActive(false);
                    _healthIcon2.gameObject.SetActive(false);
                    _healthIcon2_empty.gameObject.SetActive(true);
                    _healthIcon3.gameObject.SetActive(false);
                    _healthIcon3_empty.gameObject.SetActive(true);
                    break;
                }

            case 2:
                {
                    _healthIcon1.gameObject.SetActive(true);
                    _healthIcon1_empty.gameObject.SetActive(false);
                    _healthIcon2.gameObject.SetActive(true);
                    _healthIcon2_empty.gameObject.SetActive(false);
                    _healthIcon3.gameObject.SetActive(false);
                    _healthIcon3_empty.gameObject.SetActive(true);
                    break;
                }
            
            case 3:
                {
                    _healthIcon1.gameObject.SetActive(true);
                    _healthIcon1_empty.gameObject.SetActive(false);
                    _healthIcon2.gameObject.SetActive(true);
                    _healthIcon2_empty.gameObject.SetActive(false);
                    _healthIcon3.gameObject.SetActive(true);
                    _healthIcon3_empty.gameObject.SetActive(false);
                    break;
                }
        }
    }

    public void TakeDamage(int damageAmt)
    {
        _health -= damageAmt;
        UpdateHealth();

        if (_health == 0)
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerController.OnDeath();
        }
    }

    public void AddHealth(int healAmt)
    {
        _health += healAmt;
        UpdateHealth();

        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }

}
