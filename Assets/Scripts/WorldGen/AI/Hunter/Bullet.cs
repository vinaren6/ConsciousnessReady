using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    private void FixedUpdate()
    {
        transform.Translate(transform.up * Time.fixedDeltaTime * speed);
    }
}
