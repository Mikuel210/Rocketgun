using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    // Fields
    [Header("Parameters")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementTime;
    [SerializeField] private float jumpForce;

    [Header("Physics")]
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float groundFriction;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundMask;

    [Header("References")]
    [SerializeField] private WeaponController weaponController;

    private CharacterController _controller;
    private Vector3 _movement;
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

        Vector3 movementTarget = (transform.right * xAxis + transform.forward * zAxis).normalized * movementSpeed;
        _movement = Vector3.Lerp(_movement, movementTarget, movementTime * Time.deltaTime);

        // Gravity
        _velocity.y -= gravity * Time.deltaTime;
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
        var localMovement = transform.InverseTransformDirection(_movement);
        var localVelocity = transform.InverseTransformDirection(_velocity);
        float x = Mathf.Abs(localMovement.x) > Mathf.Abs(localVelocity.x) ? localMovement.x : localVelocity.x;
        float z = Mathf.Abs(localMovement.z) > Mathf.Abs(localVelocity.z) ? localMovement.z : localVelocity.z;

        Vector3 result = transform.TransformDirection(new Vector3(x, _velocity.y, z));
        _controller.Move(result * Time.deltaTime);

        // TODO: Update velocity if hitting something
    }

    private void OnShoot() => _velocity = Camera.main.transform.forward * -weaponController.Weapon.recoil;

}