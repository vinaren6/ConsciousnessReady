using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 0.1f;
    [SerializeField]
    private float acceleration = 0f;
    Vector2 movement;
    Vector2 moveDirection;
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
      
        moveDirection.x = Input.GetAxisRaw("RightAnalogX") * -1;
        moveDirection.y = Input.GetAxisRaw("RightAnalogY");
    }

    private void FixedUpdate()
    {
    
        if (rb2d.velocity.magnitude < movement.magnitude * maxSpeed)
            {
            movement = movement.normalized;
            rb2d.AddForce(movement * acceleration * Time.fixedDeltaTime, ForceMode2D.Impulse);

            

            }
      //  Debug.Log(moveDirection.x);
        if (Mathf.Abs(moveDirection.x) > 0.4 || Mathf.Abs(moveDirection.y) > 0.4)
        {

            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
           // Debug.Log(angle);
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            //transform.rotation += 90;
        }

    }
    
}
