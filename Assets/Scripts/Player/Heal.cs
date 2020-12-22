using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField]
    private int healAmount = 5;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<AudioManager>().Play("Healing");
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Health player = other.gameObject.GetComponent<Health>();

        if (player != null)
            player.Heal(healAmount);
    }
    
}
