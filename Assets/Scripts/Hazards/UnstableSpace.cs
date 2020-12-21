using UnityEngine;

public class UnstableSpace : MonoBehaviour
{
    [Header("Base Settings")]

    [SerializeField]
    private float damage = 0.001f;


    [SerializeField]
    [Range(0f, 1f)]
    private float slowDown = 0f;



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Health Player = collision.gameObject.GetComponent<Health>();

            if (Player != null)
                Player.TakeDamage(damage);
        }
    }

}
