using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float _moveSpeed = 12f;
    [SerializeField] public float _jumpHeight = 5f;
    [SerializeField] private float _jumpBuffer = 0.1f;
    private float _hmove;
    private float _lastJumpPressed;
    private bool _bufferedJump => _isGrounded && _lastJumpPressed + _jumpBuffer > Time.time;
    //private Rigidbody _rb = null;
    public CharacterController _controller;

    [Header("Ground Check")]
    public float _gravity = 9.81f;
    public Transform _groundCheck;
    public float _groundDistance = 0.4f;
    public LayerMask _groundMask;
    Vector3 _velocity;
    private bool _isGrounded;
    private bool _isMoving = false;
    private bool _isFacingRight = true;
    public bool _canShoot = false;

    [Header("Sound Settings")]
    [SerializeField] AudioSource _soundMove;
    [SerializeField] AudioSource _soundJump;
    [SerializeField] AudioSource _soundDeath;

    GameManager gameManager;

    private void Awake() //initialize this instance
    {
        gameManager = FindObjectOfType<GameManager>();
        //_rb = GetComponent<Rigidbody>();
        _soundMove = GetComponent<AudioSource>();
        _soundJump = GetComponent<AudioSource>();
        _soundDeath = GetComponent<AudioSource>();

    }

    private void Update() 
    {
        // //check ground
        // _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        CollisionCheck();

        // if (_isGrounded && _velocity.y < 0) //if on the ground, stick player to ground unless jumping
        // {
        //     _velocity.y = -2f;
        // }
        GravityCheck();


        if (_isMoving == true)
        {
            Vector3 _moveLeft = transform.right * _hmove;
            _controller.Move(_moveLeft * _moveSpeed * Time.deltaTime);
        }
        
        //if in air, affected by gravity
        _velocity.y -= _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
        //Jump();
    }

    private void CollisionCheck()
    {
        //check ground
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
    }

    private void GravityCheck()
    {

        if (_isGrounded && _velocity.y < 0) //if on the ground, stick player to ground unless jumping
        {
            _velocity.y = -2f;
        }

    }

    public void MoveRight()
    {
        _isMoving = true;
        //move player
        _hmove = 1;
        // Vector3 _moveRight = transform.right * _hmove;
        // _controller.Move(_moveRight * _moveSpeed * Time.deltaTime);
        if (_soundMove != null) { _soundMove.Play(); }
        //face direction
        if (!_isFacingRight && _hmove > 0f)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale; //get transform
            localScale.x *= -1f; //flip
            transform.localScale = localScale; //update
        }

        //can shoot if player is NOT moving and is not jumping
        if ( _isGrounded && _hmove == 0) { _canShoot = true; }
    }

    public void MoveLeft()
    {
        _isMoving = true;
        //move player
        _hmove = -1;
        if (_soundMove != null) { _soundMove.Play(); }
        //face direction
        if (_isFacingRight && _hmove < 0f)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale; //get transform
            localScale.x *= -1f; //flip
            transform.localScale = localScale; //update
        }

        //can shoot if player is NOT moving and is not jumping
        if ( _isGrounded && _hmove == 0) { _canShoot = true; }
    }

    public void Jump()
    {
        _lastJumpPressed = Time.time;

        if (_isGrounded || _bufferedJump)
        {
            //_velocity.y = Mathf.Sqrt(_jumpHeight * -2f * -_gravity);
            _velocity.y = _jumpHeight;
            
            if (_soundJump != null) { _soundJump.Play(); }
        }
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
