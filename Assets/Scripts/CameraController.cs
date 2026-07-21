using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float sensitivity = 100;
    [SerializeField] private Transform player;
    private float _xRotation = 0;
    private float _yRotation = 0;

    void Start() => Cursor.lockState = CursorLockMode.Locked;

    void Update()
    {
        float deltaX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float deltaY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        _xRotation = Mathf.Clamp(_xRotation - deltaY, -90, 90);
        _yRotation = player.eulerAngles.y + deltaX;

        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        player.localRotation = Quaternion.Euler(0, _yRotation, 0);
    }
}
