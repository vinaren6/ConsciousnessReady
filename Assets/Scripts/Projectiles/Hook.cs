using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        player.transform.SetParent(other.transform);
    }


}
