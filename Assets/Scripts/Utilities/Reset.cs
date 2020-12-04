using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class Reset : MonoBehaviour
{
    [SerializeField] private InputAction reset;
    void Start()
    {
        reset.Enable();
        reset.performed += context => SceneManager.LoadScene(0) ;
    }
}
