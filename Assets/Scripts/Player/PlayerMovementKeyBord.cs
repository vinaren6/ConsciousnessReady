using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementKeyBord : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 0.1f;
    [SerializeField]
    private float acceleration = 0f;
    Vector2 movement;
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {

        if (rb2d.velocity.magnitude < movement.magnitude * maxSpeed)
        {
            movement = movement.normalized;
            rb2d.AddForce(movement * acceleration * Time.fixedDeltaTime, ForceMode2D.Impulse);



        }
    }

}
