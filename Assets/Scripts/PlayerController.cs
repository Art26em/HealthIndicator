using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<float> OnPlayerTakeDamage;
    public event Action<float> OnPlayerGetHeal;
    
    [SerializeField] private float health = 100;
    [SerializeField] private float maxHealth = 100;

    public float GetHealth()
    {
        return health;
    }
    
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        OnPlayerTakeDamage?.Invoke(damage);
    }

    public void GetHeal(float heal)
    {
        health += heal;
        OnPlayerGetHeal?.Invoke(heal);
    }
    
}
