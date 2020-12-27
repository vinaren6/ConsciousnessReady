using UnityEngine.Video;
using UnityEngine;

public class IsVideoFinished : MonoBehaviour
{

    [SerializeField]
    private VideoPlayer videoPlayer;

    private bool videoIsFinished;

    private void Update()
    {
        CheckIfVideoIsFinished();

        if (videoIsFinished)
            gameObject.GetComponent<LoadThisScene>().LoadScene();
    }


    void CheckIfVideoIsFinished()
    {
        if ((ulong)videoPlayer.frame == videoPlayer.frameCount - 1)
        {
            videoIsFinished = true;
        }
    }

}
