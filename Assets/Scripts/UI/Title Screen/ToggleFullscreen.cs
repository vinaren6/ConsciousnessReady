using UnityEngine;
using UnityEngine.UI;

public class ToggleFullscreen : MonoBehaviour
{
    Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
    }


    public void SwitchToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}