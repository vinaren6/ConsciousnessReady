using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadThisScene : MonoBehaviour
{

    [SerializeField]
    private string ScreenToLoad;

    [SerializeField]
    private bool LoadAsync = false;

    [SerializeField]
    private float LoadDelay = 0f;


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
        if (LoadAsync)
            SceneManager.LoadSceneAsync(ScreenToLoad);
        else
            SceneManager.LoadScene(ScreenToLoad);
    }

    public void LoadSceneWithDelay()
    {
        Invoke("LoadScene", LoadDelay);
    }

}
