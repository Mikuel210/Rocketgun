using UnityEngine;

public class WeaponHolder : MonoBehaviour
{

    // Fields
    [Header("Parameters")]
    [SerializeField] private float positionSmoothTime;
    [SerializeField] private Vector3 positionMultiplier;
    [Space, SerializeField] private float rotationSmoothTime;
    [SerializeField] private float rotationMultiplier;

    [Header("References")]
    [SerializeField] private CameraController camera;
    [SerializeField] private CharacterController player;

    private Vector3 _localRotation;
    private Vector3 _localPosition;
    private Vector3 _positionVelocity;
    private Vector3 _rotationVelocity;

    // Methods
    void Start() => _localPosition = transform.localPosition;

    void Update()
    {
        // Update position
        Vector3 targetPosition = transform.InverseTransformPoint(player.velocity + player.transform.position);
        targetPosition.x *= positionMultiplier.x;
        targetPosition.y *= positionMultiplier.y;
        targetPosition.z *= positionMultiplier.z;

        _localPosition = Vector3.SmoothDamp(_localPosition, targetPosition, ref _positionVelocity, positionSmoothTime * Time.deltaTime);

        // Update rotation
        Vector3 targetRotation = new(camera.DeltaY * rotationMultiplier, camera.DeltaX * -rotationMultiplier, 0);
        _localRotation = Vector3.SmoothDamp(_localRotation, targetRotation, ref _rotationVelocity, rotationSmoothTime * Time.deltaTime);
        
        // Update transform
        transform.localPosition = _localPosition;
        transform.localRotation = Quaternion.Euler(_localRotation);
    }

}