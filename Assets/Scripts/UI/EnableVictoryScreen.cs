
using UnityEngine;

public class EnableVictoryScreen : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        VictoryUIRefrence.instance.SetActive(true);
    }
}
