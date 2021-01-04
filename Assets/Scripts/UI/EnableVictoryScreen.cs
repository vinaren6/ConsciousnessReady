
using UnityEngine;

public class EnableVictoryScreen : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            VictoryUIRefrence.instance.SetActive(true);
        }      
    }
}
