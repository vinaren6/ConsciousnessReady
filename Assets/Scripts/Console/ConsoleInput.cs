using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class ConsoleInput : MonoBehaviour
{
    [SerializeField] private InputAction enter;
    [SerializeField] private Text log;


    private InputField text;
    public static ConsoleController console = null;

    private void OnEnable()
    {
        enter.Enable();
    }

    private void OnDisable()
    {
        enter.Disable();
    }

    private void Awake()
    {
        if (console is null)
        console = new ConsoleController();
    }

    void Start()
    {
        enter.performed += context => PerformCommand();
        console.LogChanged += UpdateLogStr;
        text = GetComponent<InputField>();
    }

    void UpdateLogStr(string[] newLog)
    {
        if (newLog == null) {
            log.text = "";
        } else {
            log.text = string.Join("\n", newLog);
        }
    }

    void PerformCommand()
    {
        string s = text.text.Trim();
        if (s != "" && isActiveAndEnabled) {
            console.RunCommandString(s);
            text.text = "";
        }
    }

    float t = 1;
    public void ZeroTime()
    {
        t = Time.timeScale;
        Time.timeScale = 0;
    }
    public void RestoreTime()
    {
        Time.timeScale = t;
    }
}
