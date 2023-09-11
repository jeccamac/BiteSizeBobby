using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int _health = 3;
    [SerializeField] int _maxHealth = 3;
    public GameObject _healthIcon1;
    public GameObject _healthIcon2;
    public GameObject _healthIcon3;

    private void Awake()
    {
        //get heart icons
    }
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
                    _healthIcon2.gameObject.SetActive(false);
                    _healthIcon3.gameObject.SetActive(false);
                    break;
                }

            case 1:
                {
                    _healthIcon1.gameObject.SetActive(true);
                    _healthIcon2.gameObject.SetActive(false);
                    _healthIcon3.gameObject.SetActive(false);
                    break;
                }

            case 2:
                {
                    _healthIcon1.gameObject.SetActive(true);
                    _healthIcon2.gameObject.SetActive(true);
                    _healthIcon3.gameObject.SetActive(false);
                    break;
                }
            
            case 3:
                {
                    _healthIcon1.gameObject.SetActive(true);
                    _healthIcon2.gameObject.SetActive(true);
                    _healthIcon3.gameObject.SetActive(true);
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
            OnDeath();
        }
    }

    public void AddHealth(int healAmt)
    {
        _health += healAmt;
        UpdateHealth();

        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }

    public void OnDeath()
    {
        //destroy player
        Debug.Log("oh no you died");
        this.gameObject.SetActive(false);
        //restart last checkpoint

    }
}
