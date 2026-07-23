using UnityEngine;
using System;

public class HealthController : MonoBehaviour
{

    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    public event Action? OnDeath;
    public bool BulletDamage { get; private set; } = true;

    public void Damage(float damage)
    {
        Health = Mathf.Clamp(Health - damage, 0, MaxHealth);
        if (Health == 0) OnDeath?.Invoke();
    }

    public void Heal(float amount) => Health = Mathf.Clamp(Health + amount, 0, MaxHealth);

}