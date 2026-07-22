using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    // Fields
    [Header("Parameters")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity = 9.81f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundMask;

    private CharacterController _controller;
    private float _velocity;

    // Methods
    void Start() => _controller = GetComponent<CharacterController>();

    void Update()
    {
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Movement
        float xAxis = Input.GetAxisRaw("Horizontal");
        float zAxis = Input.GetAxisRaw("Vertical");

        Vector3 movement = transform.right * xAxis + transform.forward * zAxis;
        movement = movement.normalized * movementSpeed;

        // Gravity
        _velocity -= gravity * 2 * Time.deltaTime;
        if (isGrounded && _velocity < 0) _velocity = 0;

        // Jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) _velocity = jumpForce;
        movement += Vector3.up * _velocity;
        
        _controller.Move(movement * Time.deltaTime);
    }

}