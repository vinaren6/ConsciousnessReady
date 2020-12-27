using UnityEngine;

public class Hook : MonoBehaviour
{

    [SerializeField]
    private float speed = 30f;

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
