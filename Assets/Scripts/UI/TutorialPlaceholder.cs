using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class TutorialPlaceholder : MonoBehaviour
{


    private InputActions inputActions;
    private float numberOfClicks = 0;

    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.Player.Shoot.performed +=
        context =>
        {
            StartGame();
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


    void StartGame()
    {
        numberOfClicks++;

        if (numberOfClicks == 2)
        {
            SceneManager.LoadScene("Title Screen");
        }
    }
}
