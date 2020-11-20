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

    [HideInInspector]
    public int ruleId;

    private void Start() {
        if (loadOnStart) {
            LoadLevel();
        }
    }
    void LoadLevel() {
        //List<CellRules> indexList = new List<CellRules>(WorldGenerationHandler.instance.cellsRulles[(int)type]);

        List<CellRules> posebleCells = new List<CellRules>(), posebleCellsFlipHorizontal = new List<CellRules>(), posebleCellsFlipVertical = new List<CellRules>();

        int type = (int)this.type;

        for (int i = 0; i < WorldGenerationHandler.instance.cellsRulles[(int)type].Length; i++) {
            //target cellsRule set does not exist
            if (WorldGenerationHandler.instance.cellsRulles[(int)type][i] == null)
                continue;

            int checks = 0;

            //up left / nabors[0]
            if (nabors[0] == null || WorldGenerationHandler.instance.cellsRulles[type][i].cellsUpLeft.Length.Equals(0) || //no rules detected                                                                                                              
                ExistInArray(WorldGenerationHandler.instance.cellsRulles[(int)nabors[0].type][nabors[0].ruleId].cellsDownRight, WorldGenerationHandler.instance.cellsRulles[type][i]) && //check if nabor accepts target cellsRule
                ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsUpLeft, WorldGenerationHandler.instance.cellsRulles[(int)nabors[0].type][nabors[0].ruleId])) //check if target accepts nabor cellsRule
                checks++;
            //up right / nabors[1]
            if (nabors[1] == null || WorldGenerationHandler.instance.cellsRulles[type][i].cellsUpRight.Length.Equals(0) ||                                                                                                           
                ExistInArray(WorldGenerationHandler.instance.cellsRulles[(int)nabors[1].type][nabors[1].ruleId].cellsDownLeft, WorldGenerationHandler.instance.cellsRulles[type][i]) && 
                ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsUpRight, WorldGenerationHandler.instance.cellsRulles[(int)nabors[1].type][nabors[1].ruleId]))
                checks++;
            //left / nabors[2]
            if (nabors[2] == null || WorldGenerationHandler.instance.cellsRulles[type][i].cellsLeft.Length.Equals(0) ||
                ExistInArray(WorldGenerationHandler.instance.cellsRulles[(int)nabors[2].type][nabors[2].ruleId].cellsRight, WorldGenerationHandler.instance.cellsRulles[type][i]) &&
                ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsLeft, WorldGenerationHandler.instance.cellsRulles[(int)nabors[2].type][nabors[2].ruleId]))
                checks++;
            //right / nabors[3]
            if (nabors[3] == null || WorldGenerationHandler.instance.cellsRulles[type][i].cellsRight.Length.Equals(0) ||
                ExistInArray(WorldGenerationHandler.instance.cellsRulles[(int)nabors[3].type][nabors[3].ruleId].cellsLeft, WorldGenerationHandler.instance.cellsRulles[type][i]) &&
                ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsRight, WorldGenerationHandler.instance.cellsRulles[(int)nabors[3].type][nabors[3].ruleId]))
                checks++;
            //down left / nabors[4]
            if (nabors[4] == null || WorldGenerationHandler.instance.cellsRulles[type][i].cellsDownLeft.Length.Equals(0) ||
                ExistInArray(WorldGenerationHandler.instance.cellsRulles[(int)nabors[4].type][nabors[4].ruleId].cellsDownRight, WorldGenerationHandler.instance.cellsRulles[type][i]) &&
                ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsDownLeft, WorldGenerationHandler.instance.cellsRulles[(int)nabors[4].type][nabors[4].ruleId]))
                checks++;
            //down right / nabors[5]
            if (nabors[5] == null || WorldGenerationHandler.instance.cellsRulles[type][i].cellsRight.Length.Equals(0) ||
                ExistInArray(WorldGenerationHandler.instance.cellsRulles[(int)nabors[5].type][nabors[5].ruleId].cellsDownLeft, WorldGenerationHandler.instance.cellsRulles[type][i]) &&
                ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsRight, WorldGenerationHandler.instance.cellsRulles[(int)nabors[5].type][nabors[5].ruleId]))
                checks++;
        }

    }

    private bool ExistInArray<T>(T[] array, T item) {
        if (array.Length.Equals(0) || item == null)
            return true; //there is no item or no array
        foreach (T arrayItem in array)
            if (arrayItem.Equals(item))
                return true; //mach found
        return false; //no mach
    }

}
