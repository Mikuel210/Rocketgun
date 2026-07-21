using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity = 9.81f;
    private CharacterController controller;
    private Vector3 velocity;

    void Start() => controller = GetComponent<CharacterController>();

    void Update()
    {
        // Movement
        float xAxis = Input.GetAxisRaw("Horizontal");
        float zAxis = Input.GetAxisRaw("Vertical");

        Vector3 movement = transform.right * xAxis + transform.forward * zAxis;
        controller.Move(movement.normalized * movementSpeed * Time.deltaTime);

        // Gravity
        velocity += Vector3.down * gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Jumping
        // if (Input.GetKeyDown(KeyCode.Space)) rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

}