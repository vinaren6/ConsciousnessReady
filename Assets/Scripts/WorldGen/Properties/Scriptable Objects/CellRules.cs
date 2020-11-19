using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Cell Rules")]
public class CellRules : ScriptableObject
{
    public string title = "Untitled";
    public Enum.CellType type;
    public int id;

    public CellRules[] cellsUpLeft, cellsUpRight, cellsLeft, cellsRight, cellsDownLeft, cellsDownRight;

    public override string ToString() {
        return title;
    }
}
