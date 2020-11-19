using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    [SerializeField]
    private GameObject virtualCamera;

    public float amountOfParallax = 1f;

    private float textureOffsetX;
    private float textureOffsetY;
    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }


    void Update()
    {
        textureOffsetX = (virtualCamera.transform.position.x * amountOfParallax) / 10;
        textureOffsetY = (virtualCamera.transform.position.y * amountOfParallax) / 10;
        mat.SetTextureOffset("_MainTex", new Vector2(textureOffsetX, textureOffsetY));
    }
}
