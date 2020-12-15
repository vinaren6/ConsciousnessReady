using UnityEngine;
public class ScaleCamera : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    void Start()
    {
        WorldGenerationHandler.instance.ScaleCemra(cam);
    }
}
