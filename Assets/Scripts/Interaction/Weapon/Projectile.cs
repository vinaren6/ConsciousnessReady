using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    private int damage = 40;


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
