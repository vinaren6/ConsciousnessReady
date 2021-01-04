using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAfterDelay : MonoBehaviour
{
    [SerializeField] string scene = "WorldGeneration";
    [SerializeField] float delay = 10f;

    void OnEnable()
    {
        StartCoroutine(WaitAndPrint());
    }


    private IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }

}
