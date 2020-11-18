using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WorldGenSettings")]
public class WorldGenSettings : ScriptableObject
{
    public Vector2 minMaxDistance = new Vector2(5,8);
    public float sizeOffset = 2f;
    public float gridSize = 16f;

}
