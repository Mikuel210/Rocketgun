using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    // Fields
    [Header("Parameters")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementTime;
    [SerializeField] private float airSpeed;
    [SerializeField] private float jumpForce;

    [Header("Physics")]
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float groundFriction;
    [SerializeField] private float airFriction;
    [SerializeField, Range(0, 1)] private float recoilTime;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundMask;

    [Header("References")]
    [SerializeField] private WeaponController weaponController;

    private CharacterController _controller;
    private Vector3 _velocity;
    private bool _applyMovement = true;

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
        Vector3 movement = (transform.right * xAxis + transform.forward * zAxis).normalized;

        if (isGrounded) 
        {
            if (xAxis != 0 || zAxis != 0) _applyMovement = true;
            if (xAxis == 0 && zAxis == 0 && _velocity.magnitude < 0.1f) _applyMovement = false;

            Vector3 targetVelocity = Vector3.Lerp(_velocity, movement * movementSpeed, movementTime * Time.deltaTime);

            _velocity = new(
                _applyMovement ? targetVelocity.x : _velocity.x, 
                _velocity.y, 
                _applyMovement ? targetVelocity.z : _velocity.z
            );
        } 
        else 
        {
            _applyMovement = false;
            _velocity += movement * airSpeed * Time.deltaTime;
        }

        // Gravity
        _velocity.y -= gravity * Time.deltaTime;
        if (isGrounded && _velocity.y < 0) _velocity.y = 0;

        // Jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) _velocity.y = jumpForce;

        // Friction
        float frictionTime = isGrounded ? groundFriction : airFriction;
        Vector3 friction = Vector3.Lerp(_velocity, Vector3.zero, frictionTime * Time.deltaTime);

        _velocity = new(
            _applyMovement ? _velocity.x : friction.x, 
            isGrounded ? _velocity.y : friction.y, 
            _applyMovement ? _velocity.z : friction.z
        );

        // Result
        _controller.Move(_velocity * Time.deltaTime);

        // TODO: Update velocity if hitting something
    }

    private void OnShoot() 
    {
        Vector3 targetVelocity = Camera.main.transform.forward * -weaponController.Weapon.recoil;
        _velocity = Vector3.Lerp(_velocity + targetVelocity, targetVelocity, recoilTime);
    }

}