using UnityEngine;

public class PlayerInputProvider : Singleton<PlayerInputProvider>, IInputProvider
{

    // Fields
    [SerializeField] private float sensitivity = 100;

    public float DeltaX { get; private set; }
    public float DeltaY { get; private set; }
    private float _xRotation = 0;
    private float _yRotation = 0;

    // Methods
    void Start() => Cursor.lockState = CursorLockMode.Locked;

    public Vector2 GetMovement()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float zAxis = Input.GetAxisRaw("Vertical");
        return new(xAxis, zAxis);
    }

    public Vector2 GetRotation()
    {
        DeltaX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        DeltaY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        _xRotation = Mathf.Clamp(_xRotation - DeltaY, -90, 90);
        _yRotation = transform.eulerAngles.y + DeltaX;

        return new(_xRotation, _yRotation);
    }

    public bool GetJumping() => Input.GetKeyDown(KeyCode.Space);

}
