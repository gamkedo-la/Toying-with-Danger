using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public delegate void HealthHitZero();
    public HealthHitZero healthHitZero;
    public float CurrentHealth 
    {
        get 
        {
            return currentHealth;
        } 
        private set
        {
            currentHealth = value;
            if(currentHealth <=0 )
            {
                healthHitZero.Invoke();
            }
        }
    }
    float currentHealth;

    public void RemoveHealth(float amount)
    {
        CurrentHealth -= amount;
    }
    public void AddHealth(float amount)
    {
        CurrentHealth += amount;
    }
}
