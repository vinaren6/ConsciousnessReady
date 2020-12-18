using UnityEngine;

public class PlaySoundClip : MonoBehaviour
{
    [SerializeField]
    private string soundClipName;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play(soundClipName);
    }

}
