using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cell : MonoBehaviour
{
    public Enum.CellType type;

    [HideInInspector]
    public Cell[] nabors = new Cell[6];

    [HideInInspector]
    public int ruleId = -1;
    [HideInInspector]
    public int savedRuleId = -1;

    bool isLoaded = false;
    Scene scene = new Scene();

    static bool LoadingLevel = false;

    static bool GameHaveStarted = false;

    private void Start()
    {
        enabled = false;

        switch (type) {
            case Enum.CellType.Default:
                break;
            case Enum.CellType.Outpost:
                Instantiate(WorldGenerationHandler.instance.settings.outpostWorldMapObj, transform);
                break;
            case Enum.CellType.Start:
                Instantiate(WorldGenerationHandler.instance.settings.startWorldMapObj, transform);
                break;
            case Enum.CellType.End:
                Instantiate(WorldGenerationHandler.instance.settings.endWorldMapObj, transform);
                break;
        }

        if (WorldGenerationHandler.instance.settings.loadOnStart || type == Enum.CellType.Start) {
            LoadLevel();
        }
    }

    private void Update()
    {
        if (isLoaded) {
            enabled = false;
            return;
        }
        if (!LoadingLevel) {
            LoadingLevel = true;
            enabled = false;
            if (scene.name == transform.name + " Scene") {
                AsyncOperation load = SceneManager.LoadSceneAsync(scene.buildIndex, LoadSceneMode.Additive);
                load.completed += EndLoad;
            } else
                StartCoroutine(LoadSceneAsync());
        }
    }

    void LoadLevel()
    {
        //List<CellRules> indexList = new List<CellRules>(WorldGenerationHandler.instance.cellsRulles[(int)type]);

        List<int> posebleCells = new List<int>()//, posebleCellsFlipHorizontal = new List<int>(), posebleCellsFlipVertical = new List<int>()
            ;

        int type = (int)this.type;

        for (int i = 0; i < WorldGenerationHandler.instance.cellsRulles[type].Length; i++) {
            //target cellsRule set does not exist
            if (WorldGenerationHandler.instance.cellsRulles[type][i] == null)
                continue;

            //up left / nabors[0]
            //up right / nabors[1]
            //left / nabors[2]
            //right / nabors[3]
            //down left / nabors[4]
            //down right / nabors[5]
            if (WorldGenerationHandler.instance.settings.fastRules) {
                if ((nabors[0] == null || nabors[0].ruleId.Equals(-1) || WorldGenerationHandler.instance.cellsRulles[type][i].cellsUpLeft.Length.Equals(0) || //no rules detected                                                                                                              
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsUpLeft, WorldGenerationHandler.instance.cellsRulles[(int)nabors[0].type][nabors[0].ruleId])) //check if target accepts nabor cellsRule
                && (nabors[1] == null || nabors[1].ruleId.Equals(-1) || WorldGenerationHandler.instance.cellsRulles[type][i].cellsUpRight.Length.Equals(0) ||
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsUpRight, WorldGenerationHandler.instance.cellsRulles[(int)nabors[1].type][nabors[1].ruleId]))
                && (nabors[2] == null || nabors[2].ruleId.Equals(-1) || WorldGenerationHandler.instance.cellsRulles[type][i].cellsLeft.Length.Equals(0) ||
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsLeft, WorldGenerationHandler.instance.cellsRulles[(int)nabors[2].type][nabors[2].ruleId]))
                && (nabors[3] == null || nabors[3].ruleId.Equals(-1) || WorldGenerationHandler.instance.cellsRulles[type][i].cellsRight.Length.Equals(0) ||
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsRight, WorldGenerationHandler.instance.cellsRulles[(int)nabors[3].type][nabors[3].ruleId]))
                && (nabors[4] == null || nabors[4].ruleId.Equals(-1) || WorldGenerationHandler.instance.cellsRulles[type][i].cellsDownLeft.Length.Equals(0) ||
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsDownLeft, WorldGenerationHandler.instance.cellsRulles[(int)nabors[4].type][nabors[4].ruleId]))
                && (nabors[5] == null || nabors[5].ruleId.Equals(-1) || WorldGenerationHandler.instance.cellsRulles[type][i].cellsDownRight.Length.Equals(0) ||
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsDownRight, WorldGenerationHandler.instance.cellsRulles[(int)nabors[5].type][nabors[5].ruleId])))
                    posebleCells.Add(i);
            } else {
                if ((nabors[0] == null || nabors[0].ruleId.Equals(-1) || WorldGenerationHandler.instance.cellsRulles[type][i].cellsUpLeft.Length.Equals(0) || //no rules detected                                                                                                              
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[(int)nabors[0].type][nabors[0].ruleId].cellsDownRight, WorldGenerationHandler.instance.cellsRulles[type][i]) && //check if nabor accepts target cellsRule
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsUpLeft, WorldGenerationHandler.instance.cellsRulles[(int)nabors[0].type][nabors[0].ruleId])) //check if target accepts nabor cellsRule
                && (nabors[1] == null || nabors[1].ruleId.Equals(-1) || WorldGenerationHandler.instance.cellsRulles[type][i].cellsUpRight.Length.Equals(0) ||
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[(int)nabors[1].type][nabors[1].ruleId].cellsDownLeft, WorldGenerationHandler.instance.cellsRulles[type][i]) &&
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsUpRight, WorldGenerationHandler.instance.cellsRulles[(int)nabors[1].type][nabors[1].ruleId]))
                && (nabors[2] == null || nabors[2].ruleId.Equals(-1) || WorldGenerationHandler.instance.cellsRulles[type][i].cellsLeft.Length.Equals(0) ||
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[(int)nabors[2].type][nabors[2].ruleId].cellsRight, WorldGenerationHandler.instance.cellsRulles[type][i]) &&
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsLeft, WorldGenerationHandler.instance.cellsRulles[(int)nabors[2].type][nabors[2].ruleId]))
                && (nabors[3] == null || nabors[3].ruleId.Equals(-1) || WorldGenerationHandler.instance.cellsRulles[type][i].cellsRight.Length.Equals(0) ||
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[(int)nabors[3].type][nabors[3].ruleId].cellsLeft, WorldGenerationHandler.instance.cellsRulles[type][i]) &&
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsRight, WorldGenerationHandler.instance.cellsRulles[(int)nabors[3].type][nabors[3].ruleId]))
                && (nabors[4] == null || nabors[4].ruleId.Equals(-1) || WorldGenerationHandler.instance.cellsRulles[type][i].cellsDownLeft.Length.Equals(0) ||
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[(int)nabors[4].type][nabors[4].ruleId].cellsDownRight, WorldGenerationHandler.instance.cellsRulles[type][i]) &&
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsDownLeft, WorldGenerationHandler.instance.cellsRulles[(int)nabors[4].type][nabors[4].ruleId]))
                && (nabors[5] == null || nabors[5].ruleId.Equals(-1) || WorldGenerationHandler.instance.cellsRulles[type][i].cellsDownRight.Length.Equals(0) ||
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[(int)nabors[5].type][nabors[5].ruleId].cellsDownLeft, WorldGenerationHandler.instance.cellsRulles[type][i]) &&
                    ExistInArray(WorldGenerationHandler.instance.cellsRulles[type][i].cellsDownRight, WorldGenerationHandler.instance.cellsRulles[(int)nabors[5].type][nabors[5].ruleId])))
                    posebleCells.Add(i);
            }



        }
        if (posebleCells.Count != 0) {
            ruleId = posebleCells[Random.Range(0, posebleCells.Count)];
            LoadScene();
        }
    }

    void LoadScene()
    {
        if (ruleId != -1 && !isLoaded) {
            //SceneManager.LoadSceneAsync(WorldGenerationHandler.instance.cellsRulles[(int)type][ruleId].id, LoadSceneMode.Additive);
            if (LoadingLevel) {
                enabled = true;
                return;
            } else {
                LoadingLevel = true;

                if (scene.name == transform.name + " Scene") {
                    AsyncOperation load = SceneManager.LoadSceneAsync(scene.buildIndex, LoadSceneMode.Additive);
                    load.completed += EndLoad;
                } else {

                    //scene = SceneManager.CreateScene(transform.name + " Scene");
                    //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(WorldGenerationHandler.instance.cellsRulles[(int)type][ruleId].id, LoadSceneMode.Additive);
                    //asyncLoad.completed += LoadCoompleate;

                    StartCoroutine(LoadSceneAsync());
                }
            }
        }
    }
    void UnloadSceane()
    {
        if (ruleId != -1 && isLoaded) {

            //dont unload objs close to player
            GameObject[] objs = scene.GetRootGameObjects();
            for (int i = 0; i < objs.Length; i++) {
                if (((1 << objs[i].layer) & WorldGenerationHandler.instance.settings.movebleObjs.value) == 1) {
                    if (Vector3.Distance(objs[i].transform.position, PlayerMovement.playerObj.transform.position) < WorldGenerationHandler.instance.settings.dontUnloadObjInDistance) {
                        int sc = SceneManager.sceneCount - 1;
                        if (SceneManager.GetSceneAt(sc) == scene)
                            sc--;
                        
                        SceneManager.MoveGameObjectToScene(objs[i], SceneManager.GetSceneAt(sc));
                        /*
                        Debug.Log("move " + objs[i].name + " to scene: " + SceneManager.GetSceneAt(sc).name);
                    }
                    else {
                        Debug.Log("obj " + objs[i].name + " is to far away to move sceane: " + Vector3.Distance(objs[i].transform.position, PlayerMovement.playerObj.transform.position));
                    }
                } else {
                    if (Vector3.Distance(objs[i].transform.position, PlayerMovement.playerObj.transform.position) < WorldGenerationHandler.instance.settings.dontUnloadObjInDistance) {
                        Debug.Log("obj " + objs[i].name + " did not move sceane because it missing layer");
                    } else {
                        if (Vector3.Distance(objs[i].transform.position, PlayerMovement.playerObj.transform.position) < WorldGenerationHandler.instance.settings.dontUnloadObjInDistance) {
                            Debug.Log("obj " + objs[i].name + " did not move sceane because it missing layer, and was to far away: " + Vector3.Distance(objs[i].transform.position, PlayerMovement.playerObj.transform.position));
                        }
                        */
                    }
                }
            }

            SceneManager.UnloadSceneAsync(scene);
            isLoaded = false;
        }
    }

    IEnumerator LoadSceneAsync()
    {
        scene = SceneManager.CreateScene(transform.name + " Scene");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(WorldGenerationHandler.instance.cellsRulles[(int)type][ruleId].id, LoadSceneMode.Additive);
        while (!asyncLoad.isDone) {
            yield return null;
        }
        Scene s;
        try {
            s = SceneManager.GetSceneByBuildIndex(WorldGenerationHandler.instance.cellsRulles[(int)type][ruleId].id);
            GameObject[] objs = s.GetRootGameObjects();
            //flip and move objects
            for (int i = 0; i < objs.Length; i++) {
                if (!objs[i].CompareTag("StayAtZero")) {
                    //flip pos
                    objs[i].transform.position = new Vector3(
                        WorldGenerationHandler.instance.cellsRulles[(int)type][ruleId].flipX ? -objs[i].transform.position.x : objs[i].transform.position.x,
                        WorldGenerationHandler.instance.cellsRulles[(int)type][ruleId].flipY ? -objs[i].transform.position.y : objs[i].transform.position.y,
                        objs[i].transform.position.z);
                    //flip scale
                    if (objs[i].layer == 12)
                        objs[i].transform.localScale = new Vector3(
                            WorldGenerationHandler.instance.cellsRulles[(int)type][ruleId].flipX ? -objs[i].transform.localScale.x : objs[i].transform.localScale.x,
                            WorldGenerationHandler.instance.cellsRulles[(int)type][ruleId].flipY ? -objs[i].transform.localScale.y : objs[i].transform.localScale.y,
                            objs[i].transform.localScale.z);

                    objs[i].transform.position += transform.position;
                }
                SceneManager.MoveGameObjectToScene(objs[i], scene);
            }
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(s);
            if (type == Enum.CellType.Start)
                asyncUnload.completed += EndLoadStart;
            else
                asyncUnload.completed += EndLoad;
        } catch (System.Exception ex) {
            Debug.LogError(ex);
            LoadingLevel = false;
        }
    }

    void EndLoad(AsyncOperation obj)
    {
        LoadingLevel = false;
        isLoaded = true;
    }
    void EndLoadStart(AsyncOperation obj)
    {
        LoadingLevel = false;
        isLoaded = true;
        GameHaveStarted = true;
    }


    public void Load()
    {
        ruleId = savedRuleId;
    }
    public void Save()
    {
        savedRuleId = ruleId;
    }

    private bool ExistInArray<T>(T[] array, T item)
    {
        if (array.Length.Equals(0) || item == null)
            return true; //there is no item or no array
        foreach (T arrayItem in array)
            if (arrayItem.Equals(item))
                return true; //mach found
        return false; //no mach
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!WorldGenerationHandler.instance.settings.loadOnStart && collision.CompareTag("Player") && !isLoaded && !enabled && GameHaveStarted) {
            if (ruleId != -1) {
                //if (scene.name == transform.name + " Scene") {
                //LoadingLevel = true;
                //AsyncOperation load = SceneManager.LoadSceneAsync(scene.buildIndex, LoadSceneMode.Additive);
                //load.completed += EndLoad;
                LoadScene();
            } else {
                LoadLevel();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!WorldGenerationHandler.instance.settings.loadOnStart && collision.CompareTag("Player") && isLoaded && type != Enum.CellType.Start)
            UnloadSceane();
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(WorldGenerationHandler.instance.settings.gridSize, WorldGenerationHandler.instance.settings.gridSize));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(WorldGenerationHandler.instance.settings.gridSize, WorldGenerationHandler.instance.settings.gridSize));
    }

#endif
}
