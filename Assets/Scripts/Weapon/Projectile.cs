using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    private float damage;

    [SerializeField]
    private float speed = 20f;

    [SerializeField]
    private Rigidbody2D rigidBody2D;


    void Start()
    {
        rigidBody2D.velocity = transform.up * speed;
    }

}
