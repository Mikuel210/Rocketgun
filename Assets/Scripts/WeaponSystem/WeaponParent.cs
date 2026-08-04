using UnityEngine;
using System;

public class WeaponParent : MonoBehaviour
{

    // Fields
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float timeCount;

    public WeaponController? WeaponController { get; private set; }
    public event Action? OnEquipped;
    private Camera _camera;
    private Quaternion _rotation;

    // Methods
    void Start() => _camera = Camera.main;

    void Update()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (!Physics.Raycast(ray, out var hit, maxDistance, layerMask))
            _rotation = transform.parent.rotation;
        else
            _rotation = Quaternion.LookRotation(hit.point - transform.position, Vector3.forward);

        _rotation = Quaternion.Slerp(transform.rotation, _rotation, timeCount * Time.deltaTime);
        _rotation = Quaternion.Euler(_rotation.eulerAngles.x, _rotation.eulerAngles.y, 0);

        transform.rotation = _rotation;
    }

    public void Equip(WeaponSO weapon)
    {
        if (WeaponController != null)
            Destroy(WeaponController.gameObject);

        WeaponController = Instantiate(weapon.prefab).GetComponent<WeaponController>();
        WeaponController.transform.parent = gameObject.transform;
        WeaponController.transform.localPosition = Vector3.zero;
        WeaponController.transform.localRotation = Quaternion.identity;

        OnEquipped?.Invoke();
    }

}
