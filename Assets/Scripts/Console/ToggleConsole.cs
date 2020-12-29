using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleConsole : MonoBehaviour
{

    [SerializeField] private InputAction trigger;

    [SerializeField] GameObject[] objs;

    private void OnEnable()
    {
        trigger.Enable();
    }

    private void OnDisable()
    {
        trigger.Disable();
    }
    void Start()
    {
        trigger.performed += context => Toggle();
    }

    void Toggle()
    {
        bool state = !objs[0].activeSelf;
        foreach (GameObject obj in objs) {
            obj.SetActive(state);
        }
    }
}
