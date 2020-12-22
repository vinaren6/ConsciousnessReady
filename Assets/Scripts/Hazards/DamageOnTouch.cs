using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    [SerializeField]
    private int damage = 40;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Health enemy = other.gameObject.GetComponent<Health>();

        if (enemy != null)
            enemy.TakeDamage(damage);
    }

}
