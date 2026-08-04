using UnityEngine;
using System;

public class HealthController : MonoBehaviour
{

    // Fields
    [field: SerializeField] public float MaxHealth { get; private set; } = 100;
    [field: SerializeField] public float Health { get; private set; } = 100;

    public bool BulletDamage { get; private set; } = true;
    public event Action? OnDeath;

    // Methods
    public void Damage(float damage)
    {
        Health = Mathf.Clamp(Health - damage, 0, MaxHealth);
        if (Health != 0) return;

        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    public void Heal(float amount) => Health = Mathf.Clamp(Health + amount, 0, MaxHealth);

}
