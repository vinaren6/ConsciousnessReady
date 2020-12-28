using UnityEngine;
using UnityEngine.UI;

public class ToggleFullscreen : MonoBehaviour
{
    bool skipFullscreen = false;

    private void Start()
    {
        if (!Screen.fullScreen) {
            GetComponent<Toggle>().isOn = false;
            skipFullscreen = true;
        }
    }


    public void SwitchToggle()
    {
        if (skipFullscreen) {
            skipFullscreen = !skipFullscreen;
        } else {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}