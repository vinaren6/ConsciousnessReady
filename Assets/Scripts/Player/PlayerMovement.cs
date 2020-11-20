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
        Debug.Log(movement + " test 1");
        Vector2 testMovement = movement.normalized;
        movement.x = Mathf.Abs(movement.x) * testMovement.x;
        movement.y = Mathf.Abs(movement.y) * testMovement.y;
        Debug.Log(movement + " test 2");
        if (rb2d.velocity.magnitude < movement.magnitude * maxSpeed)
            {
            movement = movement.normalized;
            if (rb2d.velocity.y < 4 && rb2d.velocity.y > 4)
            {
                
                
                movement.y *= 15;
             
            }
            if (rb2d.velocity.x < 4 && rb2d.velocity.x > 4)
            {


                movement.x *= 15;
             
            }
            //velocity += movement.normalized * acceleration * Time.fixedDeltaTime;
            rb2d.AddForce(movement * acceleration * Time.fixedDeltaTime, ForceMode2D.Impulse);
            Vector2 moveDirection = rb2d.velocity;
       
            if (moveDirection.y < 0.3 && moveDirection.y > -0.3 && moveDirection.x == 0)
            {
                moveDirection = Vector2.zero;
            }
            if (moveDirection.x < 0.3 && moveDirection.x > -0.3 && moveDirection.y == 0)
            {
                moveDirection = Vector2.zero;
            }
            if (moveDirection != Vector2.zero )
            {
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                //transform.rotation += 90;
            }
            }
        
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
                //oldMovement.y = 0;
               // rb2d.velocity = oldMovement;
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
                //oldMovement.x = 0;
                //rb2d.velocity = oldMovement;
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
