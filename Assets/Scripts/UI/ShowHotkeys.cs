using UnityEngine;
using UnityEngine.UI;

public class ShowHotkeys : MonoBehaviour
{

    private InputActions inputActions;
    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.Player.ShowHotkeys.performed +=
        context =>
        {
            Toggle();
        };
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Toggle()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        GetComponent<Image>().enabled = !GetComponent<Image>().enabled;
    }


}
