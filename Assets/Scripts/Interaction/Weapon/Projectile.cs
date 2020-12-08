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

        if (!hasCollided)
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

            //Destroy(gameObject);
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
