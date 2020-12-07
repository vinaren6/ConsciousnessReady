using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField]
    private Transform character;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float cooldown = 0.1f;

    private float elapsedTime = 0f;

    [Header("Projectiles")]
    [SerializeField]
    private Projectile defaultProjectile;

    [Header("Input")]
    [SerializeField]
    private InputAction fireButton;


    private void OnEnable()
    {
        fireButton.Enable();
    }

    private void OnDisable()
    {
        fireButton.Disable();
    }


    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (fireButton.ReadValue<float>() == 1 && elapsedTime > cooldown)
        {
            Shoot();
            elapsedTime = 0f;
        }
    }

    void Shoot()
    {
        Instantiate(defaultProjectile, firePoint.position, character.rotation);
    }


}
