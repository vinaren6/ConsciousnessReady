using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    static public GameObject playerObject;

    [SerializeField] private float maxSpeed = 5.5f;
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float boostSpeed = 2.5f;
    [SerializeField] private float dragWhileMoving = 4.0f;
    [SerializeField] private float dragWhileFloating = 1.5f;
    [SerializeField] private float pixelsBeforeMouseMoves = 2;
    [SerializeField] private float deadSpaceRotation = 0.2f;
    [SerializeField] private Animator propulsion;
    [SerializeField] private Animator ship;

    InputActions input;
    Vector2 movement;
    Vector2 moveDirection;
    Rigidbody2D rigidBody;

    float boost = 1;
    bool boostAnimationState;
    float oldMouseX;
    float oldMouseY;
    bool isGamepad;


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

        playerObject = gameObject;
    }

    private void OnDestroy()
    {
        playerObject = null;
    }


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        #region Movement

        input.Player.MovementX.performed += context =>
        {
            GetInputMethod(context);
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
            GetInputMethod(context);

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
            moveDirection.x = context.ReadValue<float>();

        };
        input.Player.RotationX.canceled += context => moveDirection.x = 0;


        input.Player.RotationY.performed += context =>
        {

            moveDirection.y = context.ReadValue<float>();

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

    private void GetInputMethod(InputAction.CallbackContext context)
    {
        if (context.control.displayName == "Left Stick Left" || context.control.displayName == "Left Stick Right" ||
            context.control.displayName == "Right Stick Left" || context.control.displayName == "Right Stick Right")
        {
            isGamepad = true;
        }
        else
        {
            isGamepad = false;
        }
    }

    private void Update()
    {
        AnimationBasedOnVelocity();

        if (!isGamepad)
        {
            MouseRotation();
        }
        else
        {
            GamepadRotation();
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


    private void MouseRotation()
    {

        Vector2 mouseScreenCoordinate = Camera.main.ScreenToWorldPoint(new Vector2(input.Player.MousePositionX.ReadValue<float>(), input.Player.MousePositionY.ReadValue<float>()));
        Vector2 direction = (mouseScreenCoordinate - (Vector2)transform.position).normalized;

        transform.up = direction;

        // Set Propulsion Animation State
        float rotationDirection = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg + 90;
        float movementDirection = Mathf.Atan2(-movement.y, -movement.x) * Mathf.Rad2Deg + 90;

        if (movementDirection < 0)
        {
            movementDirection = 360 + movementDirection;
        }

        if ((movementDirection - rotationDirection <= -90 && movementDirection - rotationDirection >= -270) ||
            (movementDirection - rotationDirection >= 90 && movementDirection - rotationDirection <= 270))
        {
            propulsion.SetBool("MovingBackwards", true);
        }
        else
        {
            propulsion.SetBool("MovingBackwards", false);
        }
    }

    private void GamepadRotation()
    {
        float rotationDirection = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 90;
        float movementDirection = Mathf.Atan2(-movement.y, -movement.x) * Mathf.Rad2Deg + 90;

        transform.rotation = Quaternion.AngleAxis(rotationDirection, Vector3.forward);

        // Set Propulsion Animation State

        if (rotationDirection < 0)
        {
            rotationDirection = 360 + rotationDirection;
        }

        if (movementDirection < 0)
        {
            movementDirection = 360 + movementDirection;
        }

        if ((movementDirection - rotationDirection <= -90 && movementDirection - rotationDirection >= -270) ||
            (movementDirection - rotationDirection >= 90 && movementDirection - rotationDirection <= 270))
        {
            propulsion.SetBool("MovingBackwards", true);
        }
        else
        {
            propulsion.SetBool("MovingBackwards", false);
        }

    }

}
