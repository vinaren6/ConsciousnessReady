using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 0.1f;
    [SerializeField]
    private float acceleration = 0f;
    [SerializeField]
    private float dampening = 0f;
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
        slowPlayer();
        

        if (rb2d.velocity.magnitude < maxSpeed)
            {
            movement = movement.normalized;
            if (rb2d.velocity.y < 1 && rb2d.velocity.y > 0)
            {
                movement.y *= 2;
            }
                //velocity += movement.normalized * acceleration * Time.fixedDeltaTime;
            rb2d.AddForce(movement * acceleration * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
        Debug.Log(rb2d.velocity.y);
    }
    private void slowPlayer()
    {
        if (movement.y > 0 && rb2d.velocity.y < 0)
        {
            Vector2 oldMovement = rb2d.velocity;
            oldMovement.y += dampening * 1.5f;
            rb2d.velocity = oldMovement;
        }
        if (movement.y < 0 && rb2d.velocity.y > 0)
        {
            Vector2 oldMovement = rb2d.velocity;
            oldMovement.y -= dampening * 1.5f;
            rb2d.velocity = oldMovement;
        }
        if (movement.x > 0 && rb2d.velocity.x < 0)
        {
            Vector2 oldMovement = rb2d.velocity;
            oldMovement.x += dampening * 1.5f;
            rb2d.velocity = oldMovement;
        }
        if (movement.x < 0 && rb2d.velocity.x > 0)
        {
            Vector2 oldMovement = rb2d.velocity;
            oldMovement.x -= dampening * 1.5f;
            rb2d.velocity = oldMovement;
        }


        if (movement.y == 0)
        {
            if (rb2d.velocity.y < 0.1 && rb2d.velocity.y > -0.1)
            {
                Vector2 oldMovement = rb2d.velocity;
                oldMovement.y = 0;
                rb2d.velocity = oldMovement;
            }
            if (rb2d.velocity.y > 0)
            {
                Vector2 oldMovement = rb2d.velocity;
                oldMovement.y -= dampening;
                rb2d.velocity = oldMovement;
            }
            if (rb2d.velocity.y < 0)
            {
                Vector2 oldMovement = rb2d.velocity;
                oldMovement.y += dampening;
                rb2d.velocity = oldMovement;
            }
        }
        if (movement.x == 0)
        {
            if (rb2d.velocity.x < 0.1 && rb2d.velocity.x > -0.1)
            {
                Vector2 oldMovement = rb2d.velocity;
                oldMovement.x = 0;
                rb2d.velocity = oldMovement;
            }
            if (rb2d.velocity.x > 0)
            {
                Vector2 oldMovement = rb2d.velocity;
                oldMovement.x -= dampening;
                rb2d.velocity = oldMovement;
            }
            if (rb2d.velocity.x < 0)
            {
                Vector2 oldMovement = rb2d.velocity;
                oldMovement.x += dampening;
                rb2d.velocity = oldMovement;
            }
        }
    }
}
