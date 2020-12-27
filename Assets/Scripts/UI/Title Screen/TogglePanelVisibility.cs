using UnityEngine;

public class TogglePanelVisibility : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    public void Display()
    {
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}