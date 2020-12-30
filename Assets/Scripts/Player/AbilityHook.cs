using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AbilityHook : MonoBehaviour
{
    [SerializeField]
    private GameObject hookChain;

    [SerializeField]
    private GameObject pointOfFire;

    [SerializeField]
    private Hook hookProjectile;

    [SerializeField]
    private float hookSpeed = 10000f;


    private InputActions inputActions;
    GameObject[] allChildren;
    private bool hookMoving = false;

    RaycastHit2D raycastHit;
    string nameOfTarget;
    float distanceToTarget;


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
        MeasureDistanceToObject();

        Debug.Log(nameOfTarget + " " + distanceToTarget);

        FindObjectOfType<AudioManager>().Play("Hook");
        ResetPositionChain();

        hookChain.SetActive(true);
        hookProjectile.GetComponent<Rigidbody2D>().AddForce(transform.up * hookSpeed * (Time.fixedDeltaTime * 50), ForceMode2D.Impulse);
    }

    public void MeasureDistanceToObject()
    {
        raycastHit = Physics2D.Raycast(pointOfFire.transform.position, transform.up, 5, LayerMask.GetMask("Debris", "Enemies"));

        if (raycastHit.collider != null)
        {
            distanceToTarget = Mathf.Abs(raycastHit.point.y - transform.position.y);
            nameOfTarget = raycastHit.collider.gameObject.name;
        }
        else
        {
            distanceToTarget = 0;
            nameOfTarget = "Object not hookable";
        }


    }

    private void ResetPositionChain()
    {
        foreach (Transform child in hookChain.transform)
        {
            child.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    public void ResetVelocityChain()
    {
        foreach (Transform child in hookChain.transform)
        {
            child.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }


    private void RetractHook()
    {
        hookChain.SetActive(false);
        hookProjectile.attachedToObject = false;
        hookProjectile.DeattachFromPreviousObject();
    }

}
