using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousMovement : MonoBehaviour
{

    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(new Vector2(Random.Range(500, 1500), Random.Range(500, 1500)));
    }


}
