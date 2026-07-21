using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    private RigidBody rigidbody;

    void Start() => rigidbody = GetComponent<RigidBody>();

    void Update()
    {
        // Movement
        float xAxis = Input.GetAxis("Horizontal") * Time.deltaTime;
        float zAxis = Input.GetAxis("Vertical") * Time.deltaTime;

        transform.Translate(new Vector3(xAxis, 0, zAxis), TransformSpace.Local);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

}