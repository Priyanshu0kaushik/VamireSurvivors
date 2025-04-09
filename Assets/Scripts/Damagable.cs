using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int Health;
    public int MaxHealth;

    private void Start()
    {
        Health = MaxHealth;
    }
    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0) Death();
    }

    public virtual void Death()
    {
        gameObject.SetActive(false);
    }
}
