using UnityEngine;

public class Hook : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    public bool attachedToObject;


    private void Start()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        attachedToObject = true;

        HingeJoint2D hingeFromCollidedObject = other.gameObject.AddComponent(typeof(HingeJoint2D)) as HingeJoint2D;

        hingeFromCollidedObject.connectedBody = this.rigidBody2D;
        hingeFromCollidedObject.connectedAnchor = new Vector2(0, 0.02f);
    }


}
