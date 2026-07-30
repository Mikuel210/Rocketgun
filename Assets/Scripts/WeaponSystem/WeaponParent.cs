using UnityEngine;
using System;

public class WeaponParent : Singleton<WeaponParent>
{

    // Fields
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float timeCount;

    public event Action? OnEquipped;
    private Camera _camera;
    private Quaternion _rotation;
    private Transform? _weapon;

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
      if (_weapon != null)
        Destroy(_weapon.gameObject);

      _weapon = Instantiate(weapon.prefab).transform;
      _weapon.parent = gameObject.transform;
      _weapon.localPosition = Vector3.zero;
      _weapon.localRotation = Quaternion.identity;

      OnEquipped?.Invoke();
    }

}
