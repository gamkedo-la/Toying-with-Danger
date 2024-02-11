using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    //public delegate void HealthHitZero();
    //public HealthHitZero healthHitZero;
    [SerializeField]private float startingHealth;

    private void Awake()
    {
        currentHealth = startingHealth;
    }
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
                //healthHitZero.Invoke();
                EventManagerScript.InvokeEnemyGotDestroyedEvent(gameObject);
                Destroy(gameObject);
            }
        }
    }
    float currentHealth;

    public void RemoveHealth(float amount)
    {
        CurrentHealth -= amount;
        AudioManagerScript.Instance.PlaySfx("EnemyHit");
    }
    public void AddHealth(float amount)
    {
        CurrentHealth += amount;
    }
}
