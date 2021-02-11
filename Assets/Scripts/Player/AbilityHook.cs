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
    private ProjectileHook hookProjectile;

    [SerializeField]
    private HingeJoint2D hookAnchor;

    [SerializeField]
    private GameObject playerPhantom;

    [SerializeField]
    private float hookSpeed = 10000f;


    InputActions inputActions;
    GameObject[] chainLinks;
    bool hookMoving = false;
    Rigidbody2D playerRigidBody;
    GameObject phantomPlayer;
    PlayerMovement playerMovement;
    float savedPlayerMaxSpeed;
    float savedPlayerAcceleration;
    

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
        chainLinks = new GameObject[transform.childCount];
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();

        //savedPlayerMaxSpeed = playerMovement.maxSpeed;
        //savedPlayerAcceleration = playerMovement.acceleration;
    }

    private void ExtendHook()
    {
        RaycastForward();
        //SetChainLength();

        Debug.Log(nameOfTarget + " " + distanceToTarget);

        FindObjectOfType<AudioManager>().Play("Hook");
        ResetPositionChain();

        hookChain.SetActive(true);
        hookProjectile.GetComponent<Rigidbody2D>().AddForce(transform.up * hookSpeed * (Time.fixedDeltaTime * 50), ForceMode2D.Impulse);

        if (raycastHit.collider != null)
        {
            hookProjectile.AttachToObject(raycastHit);
            AttachAnchorToPlayer();
        }
    }

    public void RaycastForward()
    {
        raycastHit = Physics2D.Raycast(pointOfFire.transform.position, transform.up, 25, LayerMask.GetMask("Debris", "Enemies"));

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
            if (!child.CompareTag("PlayerPhantom"))
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

    private void SetChainLength()
    {
        foreach (Transform child in hookChain.transform)
        {
            if (!child.CompareTag("PlayerPhantom"))
            {
                Vector2 hingeDistance = new Vector2(0, 0.02f / (distanceToTarget * 0.75f + 1f)); // Lista ut hinge distance
                child.GetComponent<HingeJoint2D>().anchor = hingeDistance;
            }
        }
    }

    private void AttachAnchorToPlayer()
    {
        hookAnchor.connectedBody = this.playerRigidBody;
        //playerMovement.acceleration = 20000;
        //playerMovement.maxSpeed = 15000;

    }

    private void AttachAnchorToPhantom()
    {
        hookAnchor.connectedBody = playerPhantom.GetComponent<Rigidbody2D>();
        //playerMovement.maxSpeed = savedPlayerMaxSpeed;
        //playerMovement.acceleration = savedPlayerAcceleration;
    }

    private void RetractHook()
    {
        AttachAnchorToPhantom();
        hookProjectile.DeattachFromPreviousObject();
        hookChain.SetActive(false);
        hookProjectile.attachedToObject = false;
    }

}
