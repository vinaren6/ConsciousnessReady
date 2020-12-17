using UnityEngine;

public class VictoryUIRefrence : MonoBehaviour
{

    public static GameObject instance;

    void Start()
    {
        instance = gameObject;
    }

}
