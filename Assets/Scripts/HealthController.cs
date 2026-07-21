using UnityEngine;
using System;

public class HealthController : MonoBehaviour
{

    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    [SerializeField] private bool bulletDamage = true;
    public event Action? OnDeath;

    public void Damage(float damage)
    {
        Health = Mathf.Clamp(Health - damage, 0, MaxHealth);
        if (Health == 0) OnDeath?.Invoke();
    }

    public void Heal(float amount) => Health = Mathf.Clamp(Health + amount, 0, MaxHealth);

    void OnTriggerEnter(Collider other)
    {  
        bool isBullet = other.gameObject.TryGetComponent<BulletController>(out var bulletController);
        if (!bulletDamage || !isBullet) return;

        Damage(bulletController.Damage);
    }

}