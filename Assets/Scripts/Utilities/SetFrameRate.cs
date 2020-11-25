using UnityEngine;

public class SetFrameRate : MonoBehaviour
{

    [SerializeField]
    private bool LimitFrameRate = false;

    [SerializeField]
    private int TargetFrameRate = 60;


    void Awake()
    {
        if (LimitFrameRate)
        {
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = TargetFrameRate;
        }
    }
}
