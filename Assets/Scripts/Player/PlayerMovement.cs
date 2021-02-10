using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    static public GameObject playerObj;
    public float acceleration = 2f;

    [SerializeField] private float maxSpeed = 5.5f;
    [SerializeField] private float boostSpeed = 2.5f;
    [SerializeField] private float rotationSpeed = 25;
    [SerializeField] private float rotationAcceleration = 10;
    [SerializeField] private float dragWhileFloating = 1.5f;
    [SerializeField] private float dragWhileMoving = 4.0f;
    [SerializeField] private float pixelsBeforeMouseMoves = 2;
    [SerializeField] private float deadSpaceRotation = 0.2f;
    [SerializeField] private Animator propulsion;
    [SerializeField] private Animator ship;

    InputActions input;
    Vector2 movement;
    Vector2 moveDirection;
    Quaternion moveDirectioJoyCon;
    Rigidbody2D rigidBody;

    float boost = 1;
    bool boostAnimationState;
    float oldMouseX;
    float oldMouseY;
    bool isGamepad = false;


    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Awake()
    {
        input = new InputActions();

        playerObj = gameObject;
    }

    private void OnDestroy()
    {
        playerObj = null;
    }


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        #region Movement

        input.Player.MovementX.performed += context =>
        {
            if (context.control.displayName == "Left Stick Left")
            {
                isGamepad = true;
            }
            movement.x = context.ReadValue<float>();
            rigidBody.drag = dragWhileMoving;
        };

        input.Player.MovementX.canceled += context =>
        {
            movement.x = 0;
            if (movement.y == 0)
            {
                rigidBody.drag = dragWhileFloating;
            }
        };

        input.Player.MovementY.performed += context =>
        {
            if (context.control.displayName == "Left Stick Down")
            {
                isGamepad = true;
            }

            movement.y = context.ReadValue<float>();
            rigidBody.drag = dragWhileMoving;
        };

        input.Player.MovementY.canceled += context =>
        {
            movement.y = 0;
            if (movement.x == 0)
            {
                rigidBody.drag = dragWhileFloating;
            }
        };

        #endregion  

        #region Rotation

        input.Player.RotationX.performed += context =>
        {
            if (Mathf.Abs(input.Player.MovementY.ReadValue<float>()) > deadSpaceRotation || Mathf.Abs(input.Player.MovementX.ReadValue<float>()) > deadSpaceRotation)
            {
                moveDirection.x = context.ReadValue<float>();
            }
        };
        input.Player.RotationX.canceled += context => moveDirection.x = 0;


        input.Player.RotationY.performed += context =>
        {
            if (Mathf.Abs(input.Player.RotationY.ReadValue<float>()) > deadSpaceRotation || Mathf.Abs(input.Player.RotationX.ReadValue<float>()) > deadSpaceRotation)
            {
                moveDirection.y = context.ReadValue<float>();
            }
        };

        input.Player.RotationY.canceled += context => moveDirection.y = 0;

        #endregion

        #region Boost

        input.Player.Boost.started += context =>
        {
            boostAnimationState = true;
            boost = boostSpeed;
        };

        input.Player.Boost.canceled += context =>
        {
            boostAnimationState = false;
            boost = 1;
        };

        #endregion

        #region Slow

        input.Player.Slow.started += context =>
        {
            maxSpeed /= 2;
        };

        input.Player.Slow.canceled += context =>
        {
            maxSpeed *= 2;
        };

        #endregion
    }

    private void Update()
    {
        AnimationBasedOnVelocity();

        if (!MouseRotation())
        {
            if (moveDirection != Vector2.zero)
                AnalogRotation();

            else
            {
                propulsion.SetBool("MovingBackwards", false);

                if (movement != Vector2.zero)
                    MovementRotation();
            }
        }

        oldMouseY = input.Player.RotationY.ReadValue<float>();
        oldMouseX = input.Player.RotationX.ReadValue<float>();
        propulsion.SetBool("NitroBoost", boostAnimationState);
    }

    private void FixedUpdate()
    {
        if (rigidBody.velocity.magnitude < movement.magnitude * maxSpeed * boost)
        {
            rigidBody.AddForce(movement.normalized * new Vector2(Mathf.Abs(movement.x), Mathf.Abs(movement.y)) * acceleration * boost * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }

    private void AnimationBasedOnVelocity()
    {
        if (rigidBody.velocity.magnitude > 0.3)
            ship.SetBool("ShipMoving", true);
        else
            ship.SetBool("ShipMoving", false);

        propulsion.SetFloat("ShipVelocity", rigidBody.velocity.magnitude);
    }


    private bool MouseRotation()
    {
        if (Mathf.Abs(oldMouseX - input.Player.MousePositionX.ReadValue<float>()) > pixelsBeforeMouseMoves || Mathf.Abs(oldMouseY - input.Player.MousePositionY.ReadValue<float>()) > pixelsBeforeMouseMoves || input.Player.Shoot.ReadValue<float>() == 1)
        {
            isGamepad = false;
        }

        if (!isGamepad)
        {
            Vector2 testi = Camera.main.ScreenToWorldPoint(new Vector2(input.Player.MousePositionX.ReadValue<float>(), input.Player.MousePositionY.ReadValue<float>()));
            Vector2 direction = (testi - (Vector2)transform.position).normalized;
            Vector2 directionNormal = direction.normalized;

            float angleMouse = Mathf.Atan2(-directionNormal.y, -directionNormal.x) * Mathf.Rad2Deg;
            float rotationDirection = Camera.main.transform.eulerAngles.z + angleMouse + 90;

            transform.up = direction;
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
