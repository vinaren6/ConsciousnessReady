using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float health = 100f;

    [SerializeField]
    private float deathEffect;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0 )
        {
            Die();
        }
    }


    void Die ()
    {
        Destroy(gameObject);
    }
}
