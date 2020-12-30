using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField]
    private AbilityHook abilityHook;

    private Rigidbody2D rigidBody2D;
    private Rigidbody2D foreignRigidBody2D;
    private GameObject previousAttachedObject;


    [HideInInspector]
    public bool attachedToObject;


    private void Start()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        previousAttachedObject = other.gameObject;

        attachedToObject = true;
        foreignRigidBody2D = other.GetComponent<Rigidbody2D>();

        HingeJoint2D hingeFromCollidedObject = other.gameObject.AddComponent(typeof(HingeJoint2D)) as HingeJoint2D;

        foreignRigidBody2D.bodyType = RigidbodyType2D.Static;
        abilityHook.ResetVelocityChain();

        transform.position = other.transform.position;
        hingeFromCollidedObject.connectedBody = this.rigidBody2D;
        hingeFromCollidedObject.connectedAnchor = new Vector2(0, 0.02f);

    }

    public void DeattachFromPreviousObject()
    {
        if (previousAttachedObject != null)
        {
            Destroy(previousAttachedObject.GetComponent<HingeJoint2D>());
        }
    }


}
