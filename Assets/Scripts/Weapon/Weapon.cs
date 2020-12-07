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
    private float rate;

    [SerializeField]
    private float cooldown;


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

    void Start()
    {

    }

    void Update()
    {
        if (fireButton.ReadValue<float>() == 1)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(defaultProjectile, firePoint.position, character.rotation);
    }


}
