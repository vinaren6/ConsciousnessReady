using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AbilityHook : MonoBehaviour
{
    [SerializeField]
    private GameObject hookChain;

    [SerializeField]
    private Hook hookProjectile;

    [SerializeField]
    private float hookSpeed = 10000f;


    private InputActions inputActions;
    private bool hookMoving = false;


    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.Player.Hook.performed +=
        context =>
        {
            if (!hookProjectile.attachedToObject)
                ExtendHook();
            else
                RetractHook();
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

    private void ExtendHook()
    {
        hookChain.SetActive(true);
        hookProjectile.GetComponent<Rigidbody2D>().AddForce(transform.up * hookSpeed * (Time.fixedDeltaTime * 50), ForceMode2D.Impulse);
        //hookProjectile.GetComponent<Rigidbody2D>().velocity = new Vector3(5000, 0, 0);
    }

    private void RetractHook()
    {
        hookChain.SetActive(false);
        hookProjectile.attachedToObject = false;
    }

}
