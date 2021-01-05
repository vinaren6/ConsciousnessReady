using UnityEngine;
using UnityEngine.EventSystems;

public class TogglePanelVisibility : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private GameObject selectedButtonInPanel;

    [SerializeField]
    private GameObject selectedButtonWhenClosing;

    public void Display()
    {
        panel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedButtonInPanel);
    }

    public void Hide()
    {
        panel.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedButtonWhenClosing);
    }
}