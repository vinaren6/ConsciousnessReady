using UnityEngine;
using UnityEngine.Playables;

public class IsCutsceneFinished : MonoBehaviour
{

    [SerializeField]
    private double switchToGameAtSecond;

    [SerializeField]
    private PlayableDirector playableDirector;

    private void Update()
    {
        //CheckIfCutSceneIsFinished();

        Debug.Log(playableDirector.time);

        if (playableDirector.time > switchToGameAtSecond)
            gameObject.GetComponent<LoadThisScene>().LoadScene();
    }



}
