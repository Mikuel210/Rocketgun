using UnityEngine;

public class MovementController : MonoBehaviour
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

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundMask;

    [Header("References")]
    [SerializeField] private IInputProvider inputProvider;
    [SerializeField] private Transform camera;

    private CharacterController _controller;
    private Vector3 _velocity;
    private bool _applyMovement = true;

    // Methods
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        WeaponParent.Instance.OnEquipped += () => WeaponController.Instance.OnShoot += OnShoot;
    }

    void Update()
    {
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Rotation
        Vector2 rotation = inputProvider.GetRotation();
        camera.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        transform.localRotation = Quaternion.Euler(0, rotation.y, 0);

        // Movement
        Vector2 input = inputProvider.GetMovement();
        Vector3 movement = (transform.right * input.x + transform.forward * input.y).normalized;

        if (isGrounded)
        {
            if (input.x != 0 || input.y != 0)
                _applyMovement = true;
            if (input.x == 0 && input.y == 0 && _velocity.magnitude < 0.1f)
                _applyMovement = false;

            Vector3 targetVelocity = Vector3.Lerp(
                _velocity,
                movement * movementSpeed,
                movementTime * Time.deltaTime
            );

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
        if (isGrounded && _velocity.y < 0)
            _velocity.y = 0;

        // Jumping
        if (isGrounded && inputProvider.GetJumping())
            _velocity.y = jumpForce;

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
        Vector3 targetVelocity =
            WeaponController.Instance.transform.forward * -WeaponController.Instance.Weapon.recoil;

        _velocity = Vector3.Lerp(
            _velocity + targetVelocity,
            targetVelocity,
            WeaponController.Instance.Weapon.recoilTime
        );
    }
}
