using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    private int damage = 40;

    [SerializeField]
    private float speed = 20f;

    [SerializeField]
    private Rigidbody2D rigidBody2D;


    void Start()
    {
        rigidBody2D.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health enemy = other.GetComponent<Health>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        //Destroy(gameObject);
    }

}
