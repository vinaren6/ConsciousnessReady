using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 5.5f;
    [SerializeField]
    private float acceleration = 2f;
    Vector2 movement;
    Vector2 moveDirection;
    Quaternion moveDirectioJoyCon;
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        moveDirection.x = Input.GetAxisRaw("RightAnalogX") * -1;
        moveDirection.y = Input.GetAxisRaw("RightAnalogY");

        
    }

    private void FixedUpdate()
    {
    
        if (rb2d.velocity.magnitude < movement.magnitude * maxSpeed)
            {
           // movement = movement.normalized;
           // Debug.Log(movement.x);
            rb2d.AddForce(movement.normalized * new Vector2(Mathf.Abs(movement.x), Mathf.Abs(movement.y)) * acceleration * Time.fixedDeltaTime, ForceMode2D.Impulse);
            Debug.Log(rb2d.velocity.x);


        }
         
        if (moveDirection != Vector2.zero)
        {
            
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            Debug.Log(angle);
            moveDirectioJoyCon = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            transform.rotation = moveDirectioJoyCon;
            Debug.Log(Quaternion.AngleAxis(angle + 90, Vector3.forward).z + "  " + Quaternion.AngleAxis(angle + 90, Vector3.forward).w);
        }


        if (moveDirection == Vector2.zero)
        {
            moveDirection = rb2d.velocity;
           

            if (moveDirection != Vector2.zero)
            {

                
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            }
        }

    }
    
}
