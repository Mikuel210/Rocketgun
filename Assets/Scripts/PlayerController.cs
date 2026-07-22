using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    // Fields
    [Header("Parameters")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundFriction;
    [SerializeField] private float gravity = 9.81f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundMask;

    [Header("References")]
    [SerializeField] private WeaponController weaponController;

    private CharacterController _controller;
    private Vector3 _velocity;

    // Methods
    void Start() 
    {
        _controller = GetComponent<CharacterController>();
        weaponController.OnShoot += OnShoot;
    }

    void Update()
    {
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Movement
        float xAxis = Input.GetAxisRaw("Horizontal");
        float zAxis = Input.GetAxisRaw("Vertical");

        Vector3 movement = transform.right * xAxis + transform.forward * zAxis;
        movement = movement.normalized * movementSpeed;

        // Gravity
        _velocity.y -= gravity * 2 * Time.deltaTime;
        if (isGrounded && _velocity.y < 0) _velocity.y = 0;

        // Jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) _velocity.y = jumpForce;

        // Friction
        if (isGrounded) 
        {
            Vector3 friction = Vector3.Lerp(_velocity, Vector3.zero, groundFriction * Time.deltaTime);
            _velocity = new(friction.x, _velocity.y, friction.z);
        }

        // Result
        float x = Mathf.Abs(movement.x) > Mathf.Abs(_velocity.x) ? movement.x : _velocity.x;
        float z = Mathf.Abs(movement.z) > Mathf.Abs(_velocity.z) ? movement.z : _velocity.z;
        _controller.Move(new Vector3(x, _velocity.y, z) * Time.deltaTime);
    }

    private void OnShoot() => _velocity = Camera.main.transform.forward * -weaponController.Weapon.recoil;

}