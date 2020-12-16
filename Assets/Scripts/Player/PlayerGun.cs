﻿using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerGun : MonoBehaviour
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


    private InputActions inputActions;
    private float elapsedTime = 0f;


    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.Player.Shoot.performed +=
        context =>
        {
            if (context.interaction is SlowTapInteraction)
                Shoot(chargeProjectile, chargeKnockback);
            else
                Shoot(normalProjectile, normalKnockback);
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

}
