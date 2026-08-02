using UnityEngine;

public class PlayerInputProvider : InputProvider
{

    // Fields
    [SerializeField] private float sensitivity = 100;

    public float DeltaX { get; private set; }
    public float DeltaY { get; private set; }
    private float _xRotation = 0;
    private float _yRotation = 0;

    public static PlayerInputProvider Instance { get; private set; }
    public PlayerInputProvider() => Instance = this;

    // Methods
    void Start() => Cursor.lockState = CursorLockMode.Locked;

    public override Vector2 GetMovement()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float zAxis = Input.GetAxisRaw("Vertical");
        return new(xAxis, zAxis);
    }

    public override Vector2 GetRotation()
    {
        DeltaX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        DeltaY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        _xRotation = Mathf.Clamp(_xRotation - DeltaY, -90, 90);
        _yRotation = transform.eulerAngles.y + DeltaX;

        return new(_xRotation, _yRotation);
    }

    public override bool GetJumping() => Input.GetKeyDown(KeyCode.Space);

}
