using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToScreen : MonoBehaviour
{

    [SerializeField]
    private string ScreenToLoad;

    public void LoadSceneUnloadRest()
    {
        LoadScene();

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if (scene.name != ScreenToLoad)
                SceneManager.UnloadSceneAsync(scene);
        }
    }


    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(ScreenToLoad);
    }
}
