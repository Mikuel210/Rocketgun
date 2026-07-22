using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Fields
    [SerializeField] private float sensitivity = 100;
    [SerializeField] private Transform player;
    private float _xRotation = 0;
    private float _yRotation = 0;

    public float DeltaX { get; private set; }
    public float DeltaY { get; private set; }

    // Methods
    void Start() => Cursor.lockState = CursorLockMode.Locked;

    void Update()
    {
        DeltaX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        DeltaY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        _xRotation = Mathf.Clamp(_xRotation - DeltaY, -90, 90);
        _yRotation = player.eulerAngles.y + DeltaX;

        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        player.localRotation = Quaternion.Euler(0, _yRotation, 0);
    }
}
