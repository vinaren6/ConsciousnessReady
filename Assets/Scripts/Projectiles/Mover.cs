using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Vector2 velocity;
    private bool isCoolided = false;
    //[SerializeField] private ScriptableObject
    // Start is called before the first frame update
    private float timer = 1;
    private string childName;
    private float childMass;
    private float childDrag;
    private float childAngularDrag;
    private float childGravityScale;

    [SerializeField]private Explosion explosion;
    [SerializeField] private Explosion explosionCollision;

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
                if (ChildGameObject1.gameObject.GetComponent<AI>() != null && FindChildTransform(ChildGameObject1.gameObject, "Trigger").GetComponent<EnableScriptOnTrigger>() != null)
                {
                    FindChildTransform(ChildGameObject1.gameObject, "Trigger").GetComponent<CircleCollider2D>().enabled = true;
                    ChildGameObject1.gameObject.GetComponent<AI>().enabled = true;
                    ChildGameObject1.GetComponent<Health>().TakeDamage(80);
                    if (explosion != null)
                    {
                        Transform cildTransform = FindChildTransform(this.gameObject, childName);
                        Instantiate(explosion, cildTransform.position, cildTransform.rotation);
                        FindObjectOfType<AudioManager>().Play("Explosion (High)");
                        FindObjectOfType<AudioManager>().Play("Explosion (Low)");
                    }

                  
                }
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
        if (isCoolided)
        {
            if (collision.gameObject.tag != "Small Debris" && collision.gameObject.tag != "Player")
            {
                if ((collision.gameObject.layer == 13 || collision.gameObject.tag == "Asteroids" || collision.gameObject.tag == "Medium Debris") && collision.gameObject.tag != "Player")
                {
                    if (collision.collider.name.Contains("Slumber") || collision.collider.name.Contains("Rippler") || collision.collider.name.Contains("BFA"))
                    {
                        collision.gameObject.GetComponent<Health>().TakeDamage(160);
                    }
                    else
                    {
                        if (explosionCollision != null && collision.gameObject.layer != 13)
                        {
                            Instantiate(explosionCollision, collision.transform.position, collision.transform.rotation);
                        }
                        Destroy(collision.gameObject);
                    }
                }
                if (explosion != null)
                {
                    Transform cildTransform = FindChildTransform(this.gameObject, childName);
                    Instantiate(explosion, cildTransform.position, cildTransform.rotation);
                }

                FindObjectOfType<AudioManager>().Play("Explosion (High)");
                FindObjectOfType<AudioManager>().Play("Explosion (Low)");
                Destroy(gameObject);
            }
        }
        if ((collision.gameObject.layer == 13  || collision.gameObject.tag == "Asteroids" || collision.gameObject.tag == "Medium Debris") && collision.gameObject.tag != "Player")
        {

            if (collision.collider.name.Contains("Slumber") || collision.collider.name.Contains("Rippler") || collision.collider.name.Contains("BFA"))
            {
                if (explosion != null)
                {
                    Destroy(gameObject);
                    Instantiate(explosion, transform.position, transform.rotation);
                    FindObjectOfType<AudioManager>().Play("Explosion (High)");
                    FindObjectOfType<AudioManager>().Play("Explosion (Low)");

                    collision.gameObject.GetComponent<Health>().TakeDamage(80);
                   
                }

                
         
                return;
            }
            
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
                    velocity = velocity / 2;
                    colidedWithEnemy(collision);
                    
                


                if (collision.transform.GetComponent<Mover>() != null)
                    {
                        Destroy(collision.transform.GetComponent<Mover>());
                    }
                    isCoolided = true;
                }
        }
        else if (collision.gameObject.tag != "Small Debris" && collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
        
        
       // Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        GetComponent<Rigidbody2D>().velocity = velocity;
        //col.gameObject
        // transform.SetParent(col.transform);

        //Destroy(GetComponent<Rigidbody2D>());
        //col.transform.parent = transform;

    }
   
    void colidedWithEnemy(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<AI>() != null && FindChildTransform(collision.gameObject, "Trigger").GetComponent<EnableScriptOnTrigger>() != null)
        {
            // EnableScriptOnTrigger
            FindChildTransform(collision.gameObject, "Trigger").GetComponent<CircleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<AI>().enabled = false;
        }
    }
 
    void OnCollisionStay2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }


    }
