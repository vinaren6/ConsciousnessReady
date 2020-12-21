using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    private float health = 100f;

    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private Explosion explosion;

    public float GetHealth { get => health; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    private SpriteRenderer sr = null;

    private void Start()
    {
        enabled = false;
        try {
            sr = GetComponent<SpriteRenderer>();
        } catch {
            Debug.LogWarning("no SpriteRenderer found");
        }
    }

    private void Update()
    {
        if (sr.color.b >= 1 || sr == null) {
            enabled = false;
            return;
        }

        float color = sr.color.g + Time.deltaTime * 2f;

        sr.color = new Color(1, color, color);

    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) {
            Die();
        } else {
            sr.color = new Color(1, 0, 0);
            enabled = true;
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
        if (explosion != null) {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        FindObjectOfType<AudioManager>().Play("Explosion (High)");
        FindObjectOfType<AudioManager>().Play("Explosion (Low)");

        Destroy(gameObject);

    }
}
