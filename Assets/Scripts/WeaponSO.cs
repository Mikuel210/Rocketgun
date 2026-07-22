using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject prefab;
    public GameObject bullet;
    public float bulletSpeed;
    public float fireRate;
    public float recoil;
}
