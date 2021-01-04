using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private bool hasBeenMoved;


    void Update()
    {
        if (!hasBeenMoved)
        {
            transform.Rotate(new Vector3(0f, 0f, speed) * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasBeenMoved = true;
    }
}