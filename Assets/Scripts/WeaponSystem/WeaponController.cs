using UnityEngine;
using System;

public class WeaponController : MonoBehaviour
{

    // Fields
    [SerializeField] private WeaponSO weapon;
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
        if (Time.timeSinceLevelLoad - _lastShootTime < weapon.fireRate) return false;
        _lastShootTime = Time.timeSinceLevelLoad;

        // Spawn bullet
        GameObject bullet = Instantiate(weapon.bullet);
        bullet.transform.position = bulletSpawn.position;
        bullet.transform.rotation = bulletSpawn.rotation;

        // Apply force
        var bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.linearVelocity = shooterVelocity;
        bulletRigidbody.AddForce(bulletSpawn.forward * weapon.bulletSpeed, ForceMode.Impulse);

        OnShoot?.Invoke();
        return true;
    }

}