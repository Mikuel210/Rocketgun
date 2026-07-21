using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField] private WeaponSO weapon;
    [SerializeField] private Transform bulletSpawn;
    private float _lastShootTime;

    void Update()
    {
        if (Input.GetMouseButton(0))
            Shoot();
    }

    public bool Shoot()
    {
        if (Time.timeSinceLevelLoad - _lastShootTime < weapon.fireRate) return false;
        _lastShootTime = Time.timeSinceLevelLoad;

        // Spawn bullet
        GameObject bullet = Instantiate(weapon.bullet);
        bullet.transform.position = bulletSpawn.position;
        bullet.transform.rotation = bulletSpawn.rotation;

        // Apply force
        // bullet.GetComponent<Rigidbody>().AddForce(weapon.bulletSpeed);


        return true;
    }

}