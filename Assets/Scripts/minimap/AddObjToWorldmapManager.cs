using UnityEngine;

public class AddObjToWorldmapManager : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private bool defaultState;

    void Start()
    {
        if (defaultState)
            WorldmapManager.instace.desableObjs = GlobalFunctions.AddElementToArray(WorldmapManager.instace.desableObjs, target);
        else
            WorldmapManager.instace.enableObjs = GlobalFunctions.AddElementToArray(WorldmapManager.instace.enableObjs, target);
    }

}
