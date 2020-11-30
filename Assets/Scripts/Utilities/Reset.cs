using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R)) {
            SceneManager.LoadScene(0);
        }
    }
}
