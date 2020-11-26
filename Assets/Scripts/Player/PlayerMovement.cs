using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputAction movementX;
    [SerializeField] private InputAction movementY;
  
    [SerializeField] private InputAction rotationX;
    [SerializeField] private InputAction rotationY;


    [SerializeField]
    private float maxSpeed = 5.5f;
    [SerializeField]
    private float acceleration = 2f;
    Vector2 movement;
    Vector2 moveDirection;
    Quaternion moveDirectioJoyCon;
    Rigidbody2D rb2d;
    float deadSpaceRotation = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        rotationX.Enable();
        rotationY.Enable();
        movementX.Enable();
        movementY.Enable();
         rb2d = GetComponent<Rigidbody2D>();
        movementX.performed += context => movement.x = context.ReadValue<float>();
        movementX.canceled += context => movement.x = 0;

        movementY.performed += context => movement.y = context.ReadValue<float>();
        movementY.canceled += context => movement.y = 0;

        rotationX.performed += context => { 
            if (Mathf.Abs(rotationY.ReadValue<float>()) > deadSpaceRotation || Mathf.Abs(rotationX.ReadValue<float>()) > deadSpaceRotation)
            {
                moveDirection.x = context.ReadValue<float>();
            }  } ;
        rotationY.performed += context => {
            if (Mathf.Abs(rotationY.ReadValue<float>()) > deadSpaceRotation || Mathf.Abs(rotationX.ReadValue<float>()) > deadSpaceRotation)
            {
                moveDirection.y = context.ReadValue<float>();
            }
        }; ;
    }

    
    void Update()
    {
        

       // up.performed += context => Debug.Log("performed");
       // up.canceled += context => Debug.Log("canceled");
     
        // movement.x = Input.GetAxisRaw("Horizontal");
        //  movement.y = Input.GetAxisRaw("Vertical");

        // moveDirection.x = Input.GetAxisRaw("RightAnalogX") * -1;
        //  moveDirection.y = Input.GetAxisRaw("RightAnalogY") * -1;


    }

    private void FixedUpdate()
    {
    
        if (rb2d.velocity.magnitude < movement.magnitude * maxSpeed)
            {
           // movement = movement.normalized;
           // Debug.Log(movement.x);
            rb2d.AddForce(movement.normalized * new Vector2(Mathf.Abs(movement.x), Mathf.Abs(movement.y)) * acceleration * Time.fixedDeltaTime, ForceMode2D.Impulse);
          //  Debug.Log(rb2d.velocity.x);


        }
         
        if (moveDirection != Vector2.zero)
        {
            
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            //Debug.Log(angle);
            moveDirectioJoyCon = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            transform.rotation = moveDirectioJoyCon;
           // Debug.Log(Quaternion.AngleAxis(angle + 90, Vector3.forward).z + "  " + Quaternion.AngleAxis(angle + 90, Vector3.forward).w);
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
