using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem.Interactions;

public class AbilityShooting : MonoBehaviour
{
    [Header("Base Settings")]

    [SerializeField]
    private Transform pointOfFire;

    [Header("Projectiles")]

    [Space(10)]

    [SerializeField]
    private Projectile normalProjectile;

    [SerializeField]
    private float normalKnockback = 0.15f;

    [SerializeField]
    private float coolDown = 0.2f;

    [Space(10)]

    [SerializeField]
    private Projectile chargeProjectile;

    [SerializeField]
    private float chargeKnockback = 5f;

    [Space(10)]

    [SerializeField]
    private Light2D leftLamp;

    [SerializeField]
    private Light2D rightLamp;

    [SerializeField]
    private float lampMultiplier = 0.01f;


    private InputActions inputActions;
    private float elapsedTime = 0f;

    private float lampIntensity;
    private bool isLampOn;



    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.Player.Shoot.started +=
        context =>
        {
            isLampOn = true;
        };

        inputActions.Player.Shoot.performed +=
        context =>
        {
            if (context.interaction is SlowTapInteraction)
            {
                Shoot(chargeProjectile, chargeKnockback);
                isLampOn = false;
            }
            else
                Shoot(normalProjectile, normalKnockback);


        };

        inputActions.Player.Shoot.canceled +=
        context =>
        {
            isLampOn = false;
        };


    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }


    private void Update()
    {
        elapsedTime += Time.deltaTime;

        LightUpLamps(isLampOn);

    }


    public void Shoot(Projectile projectile, float knockback)
    {
        if (elapsedTime > coolDown)
        {
            Instantiate(projectile, pointOfFire.position, transform.rotation);

            if (knockback != 0)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up * -knockback, ForceMode2D.Impulse);
            }

            elapsedTime = 0f;
        }
    }

    private void LightUpLamps(bool isLampOn)
    {
        if (isLampOn && lampIntensity < 12)
            lampIntensity += lampMultiplier;
        else
            lampIntensity = 0;

        leftLamp.intensity = lampIntensity;
        rightLamp.intensity = lampIntensity;
    }

}

