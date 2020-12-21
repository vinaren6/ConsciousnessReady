using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Cell Rules")]
public class CellRules : ScriptableObject
{
    public string title = "Untitled";
    public Enum.CellType type;
    public int id;

    public bool AllowflipX = true;
    public bool AllowflipY = true;

    [HideInInspector]
    public bool flipX = false;
    [HideInInspector]
    public bool flipY = false;

#if UNITY_EDITOR
    public Texture2D preview;
#endif

    public CellRules[] cellsUpLeft, cellsUpRight, cellsLeft, cellsRight, cellsDownLeft, cellsDownRight;

    public override string ToString() {
        return title;
    }
    public CellRules Copy()
    {
        CellRules rule = ScriptableObject.CreateInstance("CellRules") as CellRules;
        rule.title = title;
        rule.id = id;

        rule.cellsUpLeft = cellsUpLeft;
        rule.cellsUpRight = cellsUpRight;
        rule.cellsLeft = cellsLeft;
        rule.cellsRight = cellsRight;
        rule.cellsDownLeft = cellsDownLeft;
        rule.cellsDownRight = cellsDownRight;

        return rule;
    }
}
