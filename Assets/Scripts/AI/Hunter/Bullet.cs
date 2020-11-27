using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    [SerializeField]
    float life = 16;

    private void Start()
    {
        Destroy(gameObject, life);
    }

    private void FixedUpdate()
    {
        transform.position += transform.up * Time.fixedDeltaTime * speed;
    }
}
