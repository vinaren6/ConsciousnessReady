using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cell : MonoBehaviour
{
    public Enum.CellType type;

    public bool loadOnStart = false;

    [HideInInspector]
    public Cell[] nabors = new Cell[6];

    private void Start() {
        if (loadOnStart) {
            LoadLevel();
        }
    }
    void LoadLevel() {
        List<CellRules> indexList = new List<CellRules>(WorldGenerationHandler.instance.cellsRulles[(int)type]);

        

    }

}
