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

    [SerializeField] private InputAction boostInput;

    float boost = 1;
    [SerializeField] private float boostSpeed = 2.5f;
    
    [SerializeField] private float boostTimerLengt = 10;
    float boostTimer = 10;
    bool isBoost;
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
        boostTimer = boostTimerLengt;
        rotationX.Enable();
        rotationY.Enable();
        movementX.Enable();
        movementY.Enable();
        boostInput.Enable();
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
        rotationX.canceled += context => moveDirection.x = 0;
        rotationY.performed += context => {
            if (Mathf.Abs(rotationY.ReadValue<float>()) > deadSpaceRotation || Mathf.Abs(rotationX.ReadValue<float>()) > deadSpaceRotation)
            {
                moveDirection.y = context.ReadValue<float>();
            }
        }; ;
        rotationY.canceled += context => moveDirection.y = 0;
        boostInput.started += context => {
            if (boostTimer > 0)
            {
                isBoost = true;
                boost = boostSpeed;
               
            }
            else
            {
                isBoost = false;
            }
            Debug.Log(boostTimer);
        };
        boostInput.canceled += context => { boost = 1; isBoost = false;  };

    }
    private void Update()
    {
        
        if (isBoost)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0)
            {
                isBoost = false;
            }
        }
        else
        {
            boost = 1;
            if (boostTimer < boostTimerLengt)
            {
                boostTimer += Time.deltaTime;
            }
            
        }
    }



    private void FixedUpdate()
    {
    
        if (rb2d.velocity.magnitude < movement.magnitude * maxSpeed * boost)
            {
           // movement = movement.normalized;
           // Debug.Log(movement.x);
            rb2d.AddForce(movement.normalized * new Vector2(Mathf.Abs(movement.x), Mathf.Abs(movement.y)) * acceleration * boost * Time.fixedDeltaTime, ForceMode2D.Impulse);
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
            Vector2 RotationDirection = rb2d.velocity;
           

            if (movement != Vector2.zero)
            {
            
                
                float angle = Mathf.Atan2(-movement.y, -movement.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            }
        }

    }
    
}
