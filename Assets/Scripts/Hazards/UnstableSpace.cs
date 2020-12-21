using UnityEngine;

public class UnstableSpace : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player;

    [SerializeField]
    private int damage = 2;

    [SerializeField]
    private float slowDown = 2f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        player.maxSpeed /= slowDown;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Health Player = other.gameObject.GetComponent<Health>();

            if (Player != null)
                Player.TakeDamage(damage);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        player.maxSpeed *= slowDown;
    }


}
