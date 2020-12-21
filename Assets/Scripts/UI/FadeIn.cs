using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class FadeIn : MonoBehaviour
{
    private RawImage rend;

    [SerializeField]
    private float speed = 0.2f;

    [SerializeField]
    private float value = 1.4f;


    private void Start()
    {
        rend = GetComponent<RawImage>();
    }

    private void FixedUpdate()
    {
        value -= Time.fixedDeltaTime * speed;

        rend.color = new Color(0, 0, 0, value);
        if (rend.color.a <= 0) {
            gameObject.SetActive(false);
        }
    }

}
