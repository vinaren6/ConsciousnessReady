using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    public GameObject player;
    public float speedOfParallax = 60f;

    [HideInInspector]
    public Rigidbody2D playerRb2d;

    void Start()
    {
        playerRb2d = player.GetComponent<Rigidbody2D>();
    }

}
