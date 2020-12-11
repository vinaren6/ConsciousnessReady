using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/World Gen Settings")]
public class WorldGenSettings : ScriptableObject
{
    public Vector2 minMaxDistance = new Vector2(5, 8);
    public float sizeOffset = 2f;
    public float gridSize = 16f;
    public int outpostAmount = 6;
    [Space(10)]
    public bool loadOnStart = false;
    public bool fastRules = true;
    public bool allowFilps = true;
    //public LayerMask flippebleObjects;
    [Space(10)]
    public LayerMask movebleObjs;
    public float dontUnloadObjInDistance = 8f;
}
