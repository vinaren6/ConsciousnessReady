using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    [SerializeField]
    private int damage = 40;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Health player = other.gameObject.GetComponent<Health>();

        if (player != null && player.CompareTag("Player"))
            player.TakeDamage(damage);
    }

}
