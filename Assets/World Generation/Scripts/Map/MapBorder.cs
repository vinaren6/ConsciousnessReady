using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public class MapBorder : MonoBehaviour
{


    [SerializeField]
    private RawImage image;

    [SerializeField]
    private float offset;

    [SerializeField]
    private float gradiantDist = 2f;

    private float size;

    void Start()
    {
        size = WorldGenerationHandler.instance.GetScale() + offset;
        GetComponent<CircleCollider2D>().radius = size;
        enabled = false;
        image.enabled = false;
    }

    void Update()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 
            (Vector3.Distance(Vector3.zero, PlayerMovement.playerObj.transform.position) - size) / gradiantDist);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enabled = true;
        image.enabled = true;
        //transform.position = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        enabled = false;
        image.enabled = false;
    }
}
