using UnityEngine;

public class SetTimeScale : MonoBehaviour
{
    [SerializeField]
    private float timeScale = 1;

    void Start()
    {
        Time.timeScale = timeScale;
    }

}
