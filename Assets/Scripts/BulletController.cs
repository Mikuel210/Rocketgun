using UnityEngine;

public class BulletController : MonoBehaviour
{

    [field: SerializeField] public float Damage { get; }
    [SerializeField] private float movementSpeed;
    [SerializeField] private float lifetime;

    void Start() => Destroy(gameObject, lifetime);

    void Update() => transform.Translate(transform.forwards * movementSpeed * Time.deltaTime);

}