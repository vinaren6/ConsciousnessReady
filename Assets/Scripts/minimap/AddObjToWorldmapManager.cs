using UnityEngine;

public class AddObjToWorldmapManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] targets;

    [SerializeField]
    private bool defaultState;

    void Start()
    {
        if (defaultState)
            foreach (GameObject target in targets)
                WorldmapManager.instace.desableObjs = GlobalFunctions.AddElementToArray(WorldmapManager.instace.desableObjs, target);
        else
            foreach (GameObject target in targets)
                WorldmapManager.instace.enableObjs = GlobalFunctions.AddElementToArray(WorldmapManager.instace.enableObjs, target);
    }

}
