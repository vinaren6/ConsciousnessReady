using UnityEngine;

public class VictoryUIRefrence : MonoBehaviour
{

    public static GameObject instance;

    void Awake()
    {
        instance = gameObject;
        gameObject.SetActive(false);
    }

}
