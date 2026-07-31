using UnityEngine;

public class BulletController : MonoBehaviour
{

    // Fields
    [field: SerializeField] public float Damage { get; private set; }
    [SerializeField] private float lifetime;
    [SerializeField] private float speed;

    private Vector3 _shooterVelocity;

    // Methods
    void Start() => Destroy(gameObject, lifetime);

    void Update() 
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        transform.Translate(_shooterVelocity * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.TryGetComponent<HealthController>(out var health) && health.BulletDamage)
            health.Damage(Damage);

        Destroy(gameObject);
    }

    public void Initialize(Vector3 shooterVelocity) => _shooterVelocity = shooterVelocity;


}
