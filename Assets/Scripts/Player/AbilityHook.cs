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
    GameObject[] allChildren;
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

    private void Start()
    {
        allChildren = new GameObject[transform.childCount];
    }

    private void ExtendHook()
    {
        FindObjectOfType<AudioManager>().Play("Hook");
        ResetChain();

        hookChain.SetActive(true);
        hookProjectile.GetComponent<Rigidbody2D>().AddForce(transform.up * hookSpeed * (Time.fixedDeltaTime * 50), ForceMode2D.Impulse);
    }

    private void ResetChain()
    {
        foreach (Transform child in hookChain.transform)
        {
            child.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    private void RetractHook()
    {
        hookChain.SetActive(false);
        hookProjectile.attachedToObject = false;
    }

}
