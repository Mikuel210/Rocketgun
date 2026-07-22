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

    Vector3 localRotation;
    Vector3 localPosition;
    Vector3 positionVelocity;
    Vector3 rotationVelocity;

    // Methods
    void Start() => localPosition = transform.localPosition;

    void Update()
    {
        // Update position
        Vector3 targetPosition = transform.InverseTransformPoint(player.velocity + player.transform.position);
        targetPosition.x *= positionMultiplier.x;
        targetPosition.y *= positionMultiplier.y;
        targetPosition.z *= positionMultiplier.z;

        localPosition = Vector3.SmoothDamp(localPosition, targetPosition, ref positionVelocity, positionSmoothTime);

        // Update rotation
        Vector3 targetRotation = new(camera.DeltaY * rotationMultiplier, camera.DeltaX * -rotationMultiplier, 0);
        localRotation = Vector3.SmoothDamp(localRotation, targetRotation, ref rotationVelocity, rotationSmoothTime);
        
        // Update transform
        transform.localPosition = localPosition;
        transform.localRotation = Quaternion.Euler(localRotation);
    }

}