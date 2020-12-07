using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [Header("Base Settings")]

    [SerializeField]
    private Transform gun;

    [SerializeField]
    private float coolDown = 0.1f;

    [SerializeField]
    private float projectileSpeed = 15f;


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
        Rigidbody2D rigidbody2D = projectile.GetComponent<Rigidbody2D>();

        //rigidbody2D.velocity = (Vector2)(gun.up * projectileSpeed) + gameObject.GetComponent<Rigidbody2D>().velocity;
        //rigidbody2D.velocity.magnitude += gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;


        rigidbody2D.AddForce((Vector2)gun.up * projectileSpeed, ForceMode2D.Impulse);
        //rigidbody2D.AddForce((Vector2)gun.up * projectileSpeed + gameObject.GetComponent<Rigidbody2D>().velocity.normalized, ForceMode2D.Impulse);


        //rigidbody2D.velocity = (Vector2)(gun.up * projectileSpeed) + gameObject.GetComponent<Rigidbody2D>().velocity;

    }


}
