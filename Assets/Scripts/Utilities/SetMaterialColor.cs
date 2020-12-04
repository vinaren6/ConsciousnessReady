using UnityEngine;
using UnityEngine.UI;

public class SetMaterialColor : MonoBehaviour
{
    [SerializeField]
    [Range(0,1)]
    private float materialHue;

    [SerializeField]
    [Range(0, 1)]
    float materialSaturation;

    [SerializeField]
    [Range(0, 1)]
    float materialValue;

    Renderer materialRenderer;

    void Start()
    {
        materialRenderer = GetComponent<Renderer>();

        materialRenderer.material.color = Color.HSVToRGB(materialHue, materialSaturation, materialValue, true);
    }

}