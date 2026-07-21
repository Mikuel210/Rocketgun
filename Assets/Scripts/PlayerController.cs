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

    private CharacterController controller;
    private Vector3 velocity;

    // Methods
    void Start() => controller = GetComponent<CharacterController>();

    void Update()
    {
        // Movement
        float xAxis = Input.GetAxisRaw("Horizontal");
        float zAxis = Input.GetAxisRaw("Vertical");

        Vector3 movement = transform.right * xAxis + transform.forward * zAxis;
        controller.Move(movement.normalized * movementSpeed * Time.deltaTime);

        // Gravity
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) velocity.y = 0;

        velocity += Vector3.down * gravity * 2 * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) velocity.y = jumpForce;
    }

}