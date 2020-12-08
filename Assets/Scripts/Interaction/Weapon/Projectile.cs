using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField]
    private int damage = 40;

    [SerializeField]
    private float speed = 30f;

    [SerializeField]
    private float particlesLifetimeAfterCollision = 2.0f;

    [Header("Explosions")]

    [SerializeField]
    private Explosion impactExplosion;

    [SerializeField]
    private float explosionDelay = 5.0f;



    private bool hasCollided = false;
    Collision2D other;


    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
        PlayAudioDettached();
    }

    private void OnCollisionEnter2D(Collision2D collison)
    {

        other = collison;

        if (other.gameObject.tag == "Small Debris")
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }

        else if (!hasCollided)
        {
            transform.SetParent(other.transform);
            Destroy(GetComponent<Rigidbody2D>());

            Invoke("DecreaseParticles", particlesLifetimeAfterCollision);

            Health enemy = other.gameObject.GetComponent<Health>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }


            Invoke("Explode", explosionDelay);


            hasCollided = true;
        }

    }

    void DecreaseParticles()
    {
        if (transform.childCount > 0)
        {
            ParticleSystem particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
            var main = particleSystem.main;
            main.maxParticles = 20;
        }
    }

    void Explode()
    {
        var normalVector = other.GetContact(0).normal;

        Explosion explosion = Instantiate(impactExplosion, transform.position, Quaternion.Euler(normalVector));
        Destroy(gameObject);
    }

    void PlayAudioDettached()
    {
        var audioGameObject = transform.Find("Audio").gameObject;
        var audioSources = audioGameObject.GetComponents<AudioSource>();

        foreach (var audio in audioSources)
        {
            audio.Play();
        }

        audioGameObject.transform.parent = null;
    }

}
