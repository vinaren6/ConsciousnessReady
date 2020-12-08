using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    private int damage = 40;

    [SerializeField]
    private float speed = 30f;

    [SerializeField]
    private float particlesLifetimeAfterCollision = 2.0f;

    private bool hasCollided = false;


    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Small Debris")
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }

        else if (!hasCollided)
        {
            transform.SetParent(other.transform);
            Destroy(GetComponent<Rigidbody2D>());

            Invoke("DisableParticles", particlesLifetimeAfterCollision);

            Health enemy = other.gameObject.GetComponent<Health>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            hasCollided = true;
        }

    }

    void DisableParticles()
    {
        if (transform.childCount > 0)
        {
            ParticleSystem particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
            particleSystem.Stop();
        }
    }

}
