using UnityEngine;
using UnityEngine.UI;

public class ToggleFullscreen : MonoBehaviour
{
    Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = Screen.fullScreen;
    }


    public void SwitchToggle()
    {
        Screen.fullScreen = toggle.isOn;
    }
}