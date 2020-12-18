using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    private float health = 100f;

    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private Explosion explosion;

    public float PlayerHealth { get => health; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
    public void Heal(int healing)
    {
        health += healing;

        if (health > maxHealth) {
            health = maxHealth;
        }
    }


    void Die()
    {
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        FindObjectOfType<AudioManager>().Play("Explosion (High)");
        FindObjectOfType<AudioManager>().Play("Explosion (Low)");

        Destroy(gameObject);

    }
}
