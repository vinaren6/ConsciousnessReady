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
    [SerializeField] private InputAction slowInput;
    [SerializeField] private InputAction mousePosX;
    [SerializeField] private InputAction mousePosY;
    [SerializeField] private InputAction shoot;

    float boost = 1;
    [SerializeField] private float boostSpeed = 2.5f;

    [SerializeField] private float boostTimerLengt = 10;
    [SerializeField] private float rotationAcceleration = 10;
    [SerializeField] private float rotationSpeed = 25;
    float boostTimer = 10;
    bool isBoost;
    [SerializeField] private float maxSpeedValue = 5.5f;
    private float maxSpeed = 5.5f;
    [SerializeField] float dragSlow = 1.5f;
    [SerializeField] float dragFast = 4.0f;
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float slowSpeed = 2f;
    [SerializeField] private bool newRotation;
    [SerializeField] private bool alwaysBoost;
    [SerializeField] private float mouseMoving = 2;
    bool mouseControl;
    float mouseControlTimer;
    [SerializeField] float mouseControlTimerLength;
    float oldMouseX;
    float oldMouseY;
    public bool cameraSmoothing;
    [SerializeField] private float cameraSmoothingMaxSpeed = 1;
    Vector2 movement;
    Vector2 moveDirection;
    Quaternion moveDirectioJoyCon;
    Rigidbody2D rb2d;
    float deadSpaceRotation = 0.2f;
    [SerializeField] Animator propulsion;
    [SerializeField] Animator ship;
    //statice refrence to the player obj.
    static public GameObject playerObj;



    private void Awake()
    {
        playerObj = gameObject;
    }

    void Start()
    {
        maxSpeed = maxSpeedValue;
        boostTimer = boostTimerLengt;
        rotationX.Enable();
        rotationY.Enable();
        movementX.Enable();
        movementY.Enable();
        boostInput.Enable();
        slowInput.Enable();
        mousePosX.Enable();
        mousePosY.Enable();
        shoot.Enable();
        rb2d = GetComponent<Rigidbody2D>();
        movementX.performed += context =>
        {
            movement.x = context.ReadValue<float>();
            rb2d.drag = dragFast;
        };
        movementX.canceled += context =>
        {
            movement.x = 0;
            if (movement.y == 0)
            {
                rb2d.drag = dragSlow;
            }
        };
        movementY.performed += context =>
        {
            movement.y = context.ReadValue<float>();
            rb2d.drag = dragFast;
        };
        movementY.canceled += context =>
        {
            movement.y = 0;
            if (movement.x == 0)
            {
                rb2d.drag = dragSlow;
            }
        };
        rotationX.performed += context =>
        {
            if (Mathf.Abs(rotationY.ReadValue<float>()) > deadSpaceRotation || Mathf.Abs(rotationX.ReadValue<float>()) > deadSpaceRotation)
            {
                moveDirection.x = context.ReadValue<float>();
            }
        };
        rotationX.canceled += context => moveDirection.x = 0;
        rotationY.performed += context =>
        {
            if (Mathf.Abs(rotationY.ReadValue<float>()) > deadSpaceRotation || Mathf.Abs(rotationX.ReadValue<float>()) > deadSpaceRotation)
            {
                moveDirection.y = context.ReadValue<float>();
            }
        };
        rotationY.canceled += context => moveDirection.y = 0;
        boostInput.started += context =>
        {
            if (boostTimer > 0)
            {
                isBoost = true;
                boost = boostSpeed;
            }
            else
            {
                isBoost = false;
            }
        };
        boostInput.canceled += context => { boost = 1; isBoost = false; };
        slowInput.started += context =>
        {
            maxSpeed = slowSpeed;
        };
        slowInput.canceled += context => { maxSpeed = maxSpeedValue; };
    }
    private void Update()
    {
        float oldRotation = transform.rotation.eulerAngles.z;
        if (oldRotation < 0)
        {
            oldRotation = 360 + oldRotation;
        }
        AnimationBasedOnVelocity();
        Boost();
        if (!MouseRotation())
        {
            if (moveDirection != Vector2.zero)
            {
                AnalogRotation();
            }
            else
            {
                propulsion.SetBool("MovingBackwards", false);
                if (movement != Vector2.zero)
                {
                    MovementRotation();
                }
            }
        }
        float newRotation = transform.rotation.eulerAngles.z;
        if (newRotation < 0)
        {
            newRotation = 360 + newRotation;
        }
        if (Mathf.Abs(oldRotation - newRotation) > cameraSmoothingMaxSpeed)
        {
            cameraSmoothing = true;
        }
        else
        {
            cameraSmoothing = false;
        }
        oldMouseY = mousePosY.ReadValue<float>();
        oldMouseX = mousePosX.ReadValue<float>();
        propulsion.SetBool("NitroBoost", isBoost);
    }

    private void FixedUpdate()
    {
        if (rb2d.velocity.magnitude < movement.magnitude * maxSpeed * boost)
        {
            rb2d.AddForce(movement.normalized * new Vector2(Mathf.Abs(movement.x), Mathf.Abs(movement.y)) * acceleration * boost * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }

    private void AnimationBasedOnVelocity()
    {
        if (rb2d.velocity.magnitude > 0.3)
        {
            ship.SetBool("ShipMoving", true);
        }
        else
        {
            ship.SetBool("ShipMoving", false);
        }
        propulsion.SetFloat("ShipVelocity", rb2d.velocity.magnitude);
    }

    private void Boost()
    {
        if (isBoost)
        {
            if (!alwaysBoost)
            {
                boostTimer -= Time.deltaTime;
            }
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

    private bool MouseRotation()
    {
        if (Mathf.Abs(oldMouseX - mousePosX.ReadValue<float>()) > mouseMoving || Mathf.Abs(oldMouseY - mousePosY.ReadValue<float>()) > mouseMoving || shoot.ReadValue<float>() == 1)
        {
            mouseControl = true;
            mouseControlTimer = mouseControlTimerLength;
        }
        if (mouseControl)
        {
            Vector2 testi = Camera.main.ScreenToWorldPoint(new Vector2(mousePosX.ReadValue<float>(), mousePosY.ReadValue<float>()));
            Vector2 direction = (testi - (Vector2)transform.position).normalized;
            Vector2 directionNormal = direction.normalized;
            float angleMouse = Mathf.Atan2(-directionNormal.y, -directionNormal.x) * Mathf.Rad2Deg;
            float rotationDirection = Camera.main.transform.eulerAngles.z + angleMouse + 90;
            transform.up = direction;
            mouseControlTimer -= Time.deltaTime;
            float angleMovement = Mathf.Atan2(-movement.y, -movement.x) * Mathf.Rad2Deg;
            float movementDirection = Camera.main.transform.eulerAngles.z + angleMovement + 90;
            if (movementDirection < 0)
            {
                movementDirection = 360 + movementDirection;
            }
            if ((movementDirection - rotationDirection <= -90 && movementDirection - rotationDirection >= -270) || (movementDirection - rotationDirection >= 90 && movementDirection - rotationDirection <= 270))
            {
                propulsion.SetBool("MovingBackwards", true);
            }
            else
            {
                propulsion.SetBool("MovingBackwards", false);
            }
            if (mouseControlTimer <= 0)
            {
                mouseControl = false;
            }
            return true;
        }
        return false;
    }
    private void AnalogRotation()
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

        moveDirectioJoyCon = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        transform.rotation = moveDirectioJoyCon;
        float rotationDirection = Camera.main.transform.eulerAngles.z + angle + 90;
        if (rotationDirection < 0)
        {
            rotationDirection = 360 + rotationDirection;
        }
        float angleMovement = Mathf.Atan2(-movement.y, -movement.x) * Mathf.Rad2Deg;
        float movementDirection = Camera.main.transform.eulerAngles.z + angleMovement + 90;
        if (movementDirection < 0)
        {
            movementDirection = 360 + movementDirection;
        }
        if ((movementDirection - rotationDirection <= -90 && movementDirection - rotationDirection >= -270) || (movementDirection - rotationDirection >= 90 && movementDirection - rotationDirection <= 270))
        {
            propulsion.SetBool("MovingBackwards", true);
        }
        else
        {
            propulsion.SetBool("MovingBackwards", false);
        }
    }
    private void MovementRotation()
    {
        float angle = Mathf.Atan2(-movement.y, -movement.x) * Mathf.Rad2Deg;
        float newRotation = Camera.main.transform.eulerAngles.z + angle + 90;

        if (newRotation < 0)
        {
            newRotation = 360 + newRotation;
        }

        Vector3 eulerRotation = transform.rotation.eulerAngles;
        if (newRotation < eulerRotation.z - 0.5 || newRotation > eulerRotation.z + 0.5)
        {

            if (eulerRotation.z > 359)
            {
                transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
                eulerRotation = transform.rotation.eulerAngles;
            }
            if (eulerRotation.z < 0)
            {
                transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 358);
                eulerRotation = transform.rotation.eulerAngles;
            }
            if ((newRotation - eulerRotation.z <= 180 && newRotation - eulerRotation.z >= 0) || newRotation - eulerRotation.z <= -180)
            {
                float rotationSpeedBasedOnRotationLength = newRotation - eulerRotation.z;

                if (rotationSpeedBasedOnRotationLength < -180)
                {
                    rotationSpeedBasedOnRotationLength = 1 + (180 - Mathf.Abs(rotationSpeedBasedOnRotationLength + 180)) / 100;
                }
                else
                {
                    rotationSpeedBasedOnRotationLength = rotationSpeedBasedOnRotationLength / 100 + 1;
                }

                if (rotationSpeedBasedOnRotationLength > cameraSmoothingMaxSpeed)
                {
                    // cameraSmoothing = true;
                }
                else
                {
                    // cameraSmoothing = false;
                }
                if (eulerRotation.z + rotationSpeed * rotationSpeedBasedOnRotationLength * Time.deltaTime - newRotation > -0.5f && eulerRotation.z < newRotation + 180)
                {
                    transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, newRotation);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, eulerRotation.z + rotationSpeed * rotationSpeedBasedOnRotationLength * rotationAcceleration * Time.deltaTime);
                }
            }
            else if (newRotation - eulerRotation.z >= 180 || (newRotation - eulerRotation.z < 0 && newRotation - eulerRotation.z > -180))
            {
                float rotationSpeedBasedOnRotationLength = Mathf.Abs(newRotation - eulerRotation.z);

                rotationSpeedBasedOnRotationLength = rotationSpeedBasedOnRotationLength / 100 + 1;

                if (rotationSpeedBasedOnRotationLength > cameraSmoothingMaxSpeed)
                {
                    // cameraSmoothing = true;
                }
                else
                {
                    // cameraSmoothing = false;
                }
                if (eulerRotation.z - rotationSpeed * rotationSpeedBasedOnRotationLength * Time.deltaTime - newRotation < 0.5f && eulerRotation.z > newRotation - 180)
                {
                    transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, (int)newRotation);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, eulerRotation.z - rotationSpeed * rotationSpeedBasedOnRotationLength * rotationAcceleration * Time.deltaTime);
                }
            }
        }
    }
}
