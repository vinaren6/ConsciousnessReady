using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooAI : MonoBehaviour
{

    [SerializeField] float acceleration = 0.1f;
    [SerializeField] string targetTag = "Player";

    bool haveParent = false;
    Transform target;
    float t;


    // Start is called before the first frame update
    void Awake()
    {
        enabled = false;
    }

    private void Update()
    {
        t += Time.deltaTime * acceleration;
        transform.position = Vector3.MoveTowards(transform.position, target.position, t);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!haveParent && collision.transform.CompareTag(targetTag)) {
            target = collision.transform;
            enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!haveParent && collision.transform.CompareTag(targetTag)) {
            transform.parent = target;
            haveParent = true;
            enabled = false;
        }
    }

}
