using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Reset : MonoBehaviour
{
    [SerializeField] private InputAction reset;


    private void OnEnable()
    {
        reset.Enable();
    }

    private void OnDisable()
    {
        reset.Disable();
    }

    void Start()
    {
        reset.performed += context => Res();
    }
    private void Res()
    {
        SceneManager.LoadScene(0);
    }
}
