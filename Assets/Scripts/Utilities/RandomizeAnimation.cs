using UnityEditor.Animations;
using UnityEngine;

public class RandomizeAnimation : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<Animator>().SetFloat("AnimationOffset", Random.Range(0.0f, 1.0f));
    }

}
