using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float jumpHeight = 5f;
    private Rigidbody _rb = null;
    public CharacterController controller;

    [Header("Ground Check")]
    public float gravity = 9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    private bool isGrounded;
    private bool isFacingRight = true;

    GameManager gameManager;
    private void Awake() 
    {
        _rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update() 
    {
        //check ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //move player
        float hmove = Input.GetAxis("Horizontal");

        Vector3 moveRight = transform.right * hmove;
        controller.Move(moveRight * moveSpeed * Time.deltaTime);

        //face direction
        if (isFacingRight && hmove < 0f || !isFacingRight && hmove > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale; //get transform
            localScale.x *= -1f; //flip
            transform.localScale = localScale; //update
        }
        
        //jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * -gravity);
        }

        velocity.y += -gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    public void OnDeath()
    {
        //destroy player
        Debug.Log("oh no you died");
        this.gameObject.SetActive(false);
        gameManager.DeathReload();
    }
}
