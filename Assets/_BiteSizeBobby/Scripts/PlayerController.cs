using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float _moveSpeed = 12f;
    [SerializeField] float _jumpHeight = 5f;
    private Rigidbody _rb = null;
    public CharacterController _controller;

    [Header("Ground Check")]
    public float _gravity = 9.81f;
    public Transform _groundCheck;
    public float _groundDistance = 0.4f;
    public LayerMask _groundMask;
    Vector3 _velocity;
    private bool _isGrounded;
    private bool _isFacingRight = true;

    [Header("Sound Settings")]
    [SerializeField] AudioSource _soundMove;
    [SerializeField] AudioSource _soundJump;
    [SerializeField] AudioSource _soundDeath;

    GameManager gameManager;

    private void Awake() //initialize this instance
    {
        gameManager = FindObjectOfType<GameManager>();
        _rb = GetComponent<Rigidbody>();
        _soundMove = GetComponent<AudioSource>();
        _soundJump = GetComponent<AudioSource>();
        _soundDeath = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        //check ground
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0) //if on the ground, stick player to ground unless jumping
        {
            _velocity.y = -2f;
        }

        //move player
        float _hmove = Input.GetAxis("Horizontal");

        Vector3 _moveRight = transform.right * _hmove;
        _controller.Move(_moveRight * _moveSpeed * Time.deltaTime);

        //face direction
        if (_isFacingRight && _hmove < 0f || !_isFacingRight && _hmove > 0f)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale; //get transform
            localScale.x *= -1f; //flip
            transform.localScale = localScale; //update
        }
        
        //jump
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * -_gravity);
            if (_soundJump != null) { _soundJump.Play(); }
        }

        _velocity.y += -_gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
        if (_soundMove != null) { _soundMove.Play(); }
    }

    public void OnDeath()
    {
        //destroy player
        Debug.Log("oh no you died");
        if (_soundDeath != null) { _soundDeath.Play(); }
        this.gameObject.SetActive(false);
        //reload from last checkpoint
        gameManager.DeathReload();
    }
}
