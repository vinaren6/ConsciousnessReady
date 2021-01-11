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


    public void AttachToObject(RaycastHit2D raycast)
    {
        previousAttachedObject = raycast.collider.gameObject;
        attachedToObject = true;

        foreignRigidBody2D = raycast.collider.GetComponent<Rigidbody2D>();

        HingeJoint2D hingeFromCollidedObject = raycast.collider.gameObject.AddComponent(typeof(HingeJoint2D)) as HingeJoint2D;

        //foreignRigidBody2D.bodyType = RigidbodyType2D.Static;
        abilityHook.ResetVelocityChain();

        transform.position = raycast.transform.position;
        hingeFromCollidedObject.connectedBody = this.rigidBody2D;
        hingeFromCollidedObject.anchor = new Vector2(0, 0);
    }


    public void DeattachFromPreviousObject()
    {
        if (previousAttachedObject != null)
        {
            Destroy(previousAttachedObject.GetComponent<HingeJoint2D>());
            Debug.Log("Dettached");
        }
    }


}
