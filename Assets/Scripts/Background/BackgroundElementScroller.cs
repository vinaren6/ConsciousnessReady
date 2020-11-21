using UnityEngine;

public class BackgroundElementScroller : MonoBehaviour
{

    [SerializeField]
    private GameObject virtualCamera;

    [SerializeField]
    private float amountOfParallax = 1f;

    [SerializeField]
    private bool flipX = false;

    [SerializeField]
    private bool flipY = false;

    private float textureOffsetX;
    private float textureOffsetY;

    private Material mat;
    private BackgroundController backgroundController;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        GameObject background = GameObject.FindGameObjectWithTag("BackgroundController");
        backgroundController = background.GetComponent<BackgroundController>();

        //if (flipX)
            //transform.localScale.x *= -1;
    }


    void Update()
    {
        textureOffsetX = (virtualCamera.transform.position.x * amountOfParallax) / backgroundController.speedOfParallax / 10;
        textureOffsetY = (virtualCamera.transform.position.y * amountOfParallax) / backgroundController.speedOfParallax / 10;
        mat.SetTextureOffset("_MainTex", new Vector2(textureOffsetX, textureOffsetY));
    }
}
