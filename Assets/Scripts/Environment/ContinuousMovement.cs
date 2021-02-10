using UnityEngine;

public class ContinuousMovement : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(750, 1500), Random.Range(750, 1500)));
    }
}