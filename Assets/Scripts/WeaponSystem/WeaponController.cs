using UnityEngine;
using System;

public class WeaponController : MonoBehaviour
{

    // Fields
    [field: SerializeField] public WeaponSO Weapon { get; private set; }
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private CharacterController player;

    public event Action? OnShoot;
    private float _lastShootTime;

    // Methods
    void Update()
    {
        if (Input.GetMouseButton(0))
            Shoot(player.velocity);
    }

    public bool Shoot(Vector3 shooterVelocity)
    {
        if (Time.timeSinceLevelLoad - _lastShootTime < Weapon.fireRate) return false;
        _lastShootTime = Time.timeSinceLevelLoad;

        // Spawn bullet
        GameObject bullet = Instantiate(Weapon.bullet);
        bullet.transform.position = bulletSpawn.position;
        bullet.transform.rotation = bulletSpawn.rotation;

        // Apply force
        var bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.linearVelocity = shooterVelocity;
        bulletRigidbody.AddForce(bulletSpawn.forward * Weapon.bulletSpeed, ForceMode.Impulse);

        OnShoot?.Invoke();
        return true;
    }

}