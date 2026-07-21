using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject prefab;
    public GameObject bullet;
    public flaot bulletSpeed;
    public float fireRate;
    public float recoil;
}
