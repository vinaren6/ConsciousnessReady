using UnityEngine;

public class BackgroundElementScroller : MonoBehaviour
{

    [SerializeField]
    private GameObject virtualCamera;

    [SerializeField]
    private float amountOfParallax = 1f;

    [SerializeField]
    private float twinkleAmount = 25f;

    [SerializeField]
    private float twinkleEveryNframe = 1f;


    private float speed = 10f;

    private float textureOffsetX;
    private float textureOffsetY;


    private Material mat;
    private BackgroundController backgroundController;



    void Start()
    {
        mat = GetComponent<Renderer>().material;
        GameObject background = GameObject.FindGameObjectWithTag("BackgroundController");
        backgroundController = background.GetComponent<BackgroundController>();

        twinkleAmount *= amountOfParallax;
    }


    void Update()
    {

        if (Time.frameCount % twinkleEveryNframe == 0 && backgroundController.playerRb2d.velocity.magnitude == 0)
            twinkleAmount *= -1;

        textureOffsetX = ( (virtualCamera.transform.position.x * amountOfParallax) / backgroundController.speedOfParallax / speed) + twinkleAmount;
        textureOffsetY = ((virtualCamera.transform.position.y * amountOfParallax) / backgroundController.speedOfParallax / speed) + twinkleAmount;

        mat.SetTextureOffset("_MainTex", new Vector2(textureOffsetX, textureOffsetY));
    }
}
