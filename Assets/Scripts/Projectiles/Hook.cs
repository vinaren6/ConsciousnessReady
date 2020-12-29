using UnityEngine;

public class Hook : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.AddComponent<HingeJoint2D>();
        var rope = collider.gameObject;
        var hinge = player.GetComponent<HingeJoint2D>();
        var rb = rope.GetComponent<Rigidbody2D>();
        hinge.connectedBody = rb;
        hinge.connectedAnchor = new Vector2(0, -2.5f);
    }


}
