using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float health = 100f;

    [SerializeField]
    private Explosion explosion;


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
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
