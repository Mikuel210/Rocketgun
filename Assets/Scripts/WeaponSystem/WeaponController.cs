using UnityEngine;
using System;

public class WeaponController : Singleton<WeaponController>
{

    // Fields
    [field: SerializeField] public WeaponSO Weapon { get; private set; }
    [SerializeField] private Transform bulletSpawn;

    public event Action? OnShoot;
    private CharacterController _player;

    // Methods
    void Start() => _player = PlayerInputProvider.Instance.GetComponent<CharacterController>();

    void Update()
    {
        if (Input.GetMouseButton(0))
            Shoot(_player.velocity);
    }

    public bool Shoot(Vector3 shooterVelocity)
    {
        if (Time.timeSinceLevelLoad - Weapon.lastShootTime < Weapon.fireRate) return false;
        Weapon.lastShootTime = Time.timeSinceLevelLoad;

        // Spawn bullet
        GameObject bullet = Instantiate(Weapon.bullet);
        bullet.transform.position = bulletSpawn.position;
        bullet.transform.rotation = bulletSpawn.rotation;

        // Initialize
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.Initialize(shooterVelocity);

        OnShoot?.Invoke();
        return true;
    }

}
