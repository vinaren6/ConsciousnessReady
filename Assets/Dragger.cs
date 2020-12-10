using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    Vector2 velocity;
    private bool isCoolided;
    //[SerializeField] private ScriptableObject
    // Start is called before the first frame update
    void Start()
    {
        velocity = GetComponent<Rigidbody2D>().velocity;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = GetComponent<Rigidbody2D>().velocity;

    }
     void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCoolided)
        {
            Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
            Destroy(GetComponent<BoxCollider2D>());
            collision.gameObject.transform.SetParent(transform);
            if (collision.transform.GetComponent<Dragger>() != null)
            {
                Destroy(collision.transform.GetComponent<Dragger>());
            }
          
            isCoolided = true;
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
