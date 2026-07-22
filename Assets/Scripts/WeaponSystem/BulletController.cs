using UnityEngine;

public class BulletController : MonoBehaviour
{

    [field: SerializeField] public float Damage { get; private set; }
    [SerializeField] private float lifetime;

    void Start() => Destroy(gameObject, lifetime);

}