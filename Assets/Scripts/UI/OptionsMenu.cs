using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private GameObject startOnThisButton;


    private InputActions inputActions;
    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.Player.ShowIngameOptions.performed +=
        context =>
        {
            DisplayOptions();
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

    public void DisplayOptions()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startOnThisButton);
    }

    public void HideOptions()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }


}
