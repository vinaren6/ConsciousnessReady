using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Base Settings")]

    [SerializeField]
    private int damage = 40;

    [SerializeField]
    private float speed = 30f;

    [SerializeField]
    private float selfDestructTimer = 1f;

    [Header("Explosions")]

    [SerializeField]
    private bool willExplode = false;

    [SerializeField]
    private bool destroyOnCollision = false;

    [SerializeField]
    private Explosion impactExplosion;

    [SerializeField]
    private float explosionDelay = 5.0f;


    private bool hasCollided = false;
    private Collision2D other;



    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
        PickSoundEffect();
        Destroy(gameObject, selfDestructTimer);
    }

    private void PickSoundEffect()
    {
        Debug.Log(gameObject.tag);


        if (gameObject.CompareTag("Projectile (Ice Small)"))
        {
            FindObjectOfType<AudioManager>().Play("GunshotLow");
            FindObjectOfType<AudioManager>().Play("GunshotHigh");
        }

        if (gameObject.CompareTag("Projectile (Ice Large)"))
        {
            FindObjectOfType<AudioManager>().Play("GunshotLow");
            FindObjectOfType<AudioManager>().Play("GunshotLower");
        }

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

            Health enemy = other.gameObject.GetComponent<Health>();

            if (enemy != null)
                enemy.TakeDamage(damage);

            if (willExplode)
                Invoke("Explode", explosionDelay);

            hasCollided = true;
        }

    }


    void Explode()
    {
        float angle = (Mathf.Atan2(-other.contacts[0].normal.y, -other.contacts[0].normal.x) * Mathf.Rad2Deg) - 90;

        Explosion explosion = Instantiate(impactExplosion, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));

        if (destroyOnCollision)
        {
            DettachParticles();
            Destroy(gameObject);
        }
    }

    void DettachParticles()
    {
        var particleSystemObject = transform.Find("Ice Particles (Projectiles)").gameObject;
        var particleSystem = particleSystemObject.GetComponent<ParticleSystem>();
        particleSystem.transform.parent = null;

        Destroy(particleSystem, selfDestructTimer);
    }


}
