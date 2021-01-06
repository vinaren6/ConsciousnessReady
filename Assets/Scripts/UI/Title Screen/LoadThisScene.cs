using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadThisScene : MonoBehaviour
{

    [SerializeField]
    private string SceneToLoad;

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

            if (scene.name != SceneToLoad)
                SceneManager.UnloadSceneAsync(scene);
        }
    }


    public void LoadScene()
    {
        if (LoadAsync)
            SceneManager.LoadSceneAsync(SceneToLoad);
        else
            SceneManager.LoadScene(SceneToLoad);
    }

    public void LoadSceneWithDelay()
    {
        Invoke("LoadScene", LoadDelay);
    }

}
