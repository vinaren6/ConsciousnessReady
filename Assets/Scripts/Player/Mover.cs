using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Vector2 velocity;
    private bool isCoolided;
    //[SerializeField] private ScriptableObject
    // Start is called before the first frame update
    private float timer = 3;
    private string childName;
    private float childMass;
    private float childDrag;
    private float childAngularDrag;
    private float childGravityScale;
    void Start()
    {
        velocity = GetComponent<Rigidbody2D>().velocity;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = GetComponent<Rigidbody2D>().velocity;
        if (isCoolided)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Transform child = FindChildTransform(gameObject, childName); //Replace "ChildName" with the child objects name.
                GameObject ChildGameObject1 = transform.GetChild(0).gameObject;
                ChildGameObject1.AddComponent<Rigidbody2D>();
                ChildGameObject1.GetComponent<Rigidbody2D>().mass = childMass;
                ChildGameObject1.GetComponent<Rigidbody2D>().drag = childDrag;
                ChildGameObject1.GetComponent<Rigidbody2D>().angularDrag = childAngularDrag;
                ChildGameObject1.GetComponent<Rigidbody2D>().gravityScale = childGravityScale;
                ChildGameObject1.GetComponent<Rigidbody2D>().velocity = velocity;
                Debug.Log(ChildGameObject1.GetComponent<Rigidbody2D>().velocity + " gello");
                child.parent = null;
                Destroy(gameObject);
            }
        }
    }
    public Transform FindChildTransform(GameObject parent, string name)
    {
        Transform child = null;

        foreach (Transform trans in parent.transform)
        {
            if (trans.name == name)
            {
                child = trans;
                if (child != null)
                    return child;
            }
            else
            {
                child = FindChildTransform(trans.gameObject, name);
                if (child != null)
                    return child;
            }
        }

        return child;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 13 && collision.gameObject.tag != "Player")
        {
          


                if (!isCoolided)
                {
                    childName = collision.collider.gameObject.name;
                    childMass = collision.gameObject.GetComponent<Rigidbody2D>().mass;
                    childDrag = collision.gameObject.GetComponent<Rigidbody2D>().drag;
                    childAngularDrag = collision.gameObject.GetComponent<Rigidbody2D>().angularDrag;
                    childGravityScale = collision.gameObject.GetComponent<Rigidbody2D>().gravityScale;
                    Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
                    Destroy(GetComponent<BoxCollider2D>());
                    collision.gameObject.transform.SetParent(transform);
                    if (collision.transform.GetComponent<Mover>() != null)
                    {
                        Destroy(collision.transform.GetComponent<Mover>());
                    }

                    isCoolided = true;
                }
            }
        
       // Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        GetComponent<Rigidbody2D>().velocity = velocity;
        //col.gameObject
        // transform.SetParent(col.transform);

        //Destroy(GetComponent<Rigidbody2D>());
        //col.transform.parent = transform;

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }


    }
