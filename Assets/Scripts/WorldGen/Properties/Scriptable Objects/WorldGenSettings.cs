using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/World Gen Settings")]
public class WorldGenSettings : ScriptableObject
{
    public Vector2 minMaxDistance = new Vector2(5, 8);
    public float sizeOffset = 2f;
    public float gridSize = 16f;
    public int outpostAmount = 6;
}
