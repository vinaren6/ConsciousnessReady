using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DraggerWeapon : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 0.001f;
    [SerializeField] private InputAction shoot;
    [SerializeField]
    private Transform gun;
    [SerializeField]
    private float coolDown = 1.1f;
    [SerializeField]
    private GameObject projectile;
    private float elapsedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        shoot.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (shoot.ReadValue<float>() == 1 && elapsedTime > coolDown)
        {

            Debug.Log("test");
            GameObject bullet = Instantiate(projectile, gun.position, transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody2D>().AddForce((Vector2)gun.up * projectileSpeed * bullet.GetComponent<Rigidbody2D>().mass, ForceMode2D.Impulse);
            elapsedTime = 0;
        }
    }
}
