using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [Header("Base Settings")]

    [SerializeField]
    private Transform gun;

    [SerializeField]
    private float coolDown = 0.3f;


    [Header("Projectiles")]

    [SerializeField]
    private Projectile defaultProjectile;

    [Header("Input")]
    [SerializeField]
    private InputAction fireButton;


    private float elapsedTime = 0f;


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

        if (fireButton.ReadValue<float>() == 1 && elapsedTime > coolDown)
        {
            Shoot();
            elapsedTime = 0f;
        }
    }

    void Shoot()
    {
        Projectile projectile = Instantiate(defaultProjectile, gun.position, transform.rotation);
    }


}
