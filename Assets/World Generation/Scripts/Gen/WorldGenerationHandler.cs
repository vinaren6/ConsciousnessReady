using System.Collections.Generic;
using UnityEngine;

public class WorldGenerationHandler : MonoBehaviour
{

    public static WorldGenerationHandler instance;

    public WorldGenSettings settings;
    [SerializeField] private GameObject cell;

    [SerializeField] private CellRules[] cellRules;

    [HideInInspector]
    public CellRules[][] cellsRulles = new CellRules[4][];

    private int[,] world;
    private float worldSize;

    [HideInInspector]
    public string debugData = "";


    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogWarning("Detected multible instance");
            Destroy(this);
            return;
        }

        SeperateRules();
        GenerateGrid();
        GenerateCells();
    }

    private void SeperateRules()
    {
        List<CellRules> cellRulesDefaltL = new List<CellRules>(), cellRulesOutpostL = new List<CellRules>(), cellRulesStartL = new List<CellRules>(), cellRulesEndL = new List<CellRules>();

        //add baseRules
        foreach (CellRules rule in cellRules) {
            switch (rule.type) {
                case Enum.CellType.Default:
                    cellRulesDefaltL.Add(rule);
                    break;

                case Enum.CellType.Outpost:
                    cellRulesOutpostL.Add(rule);
                    break;

                case Enum.CellType.Start:
                    cellRulesStartL.Add(rule);
                    break;

                case Enum.CellType.End:
                    cellRulesEndL.Add(rule);
                    break;
            }
        }

        //add filped rules
        if (settings.allowFilps)
            foreach (CellRules rule in cellRules) {
                switch (rule.type) {
                    case Enum.CellType.Default:
                        if (!rule.Allowflip)
                            break;

                        //flip X
                        CellRules flipx = FlipX(rule);
                        cellRulesDefaltL.Add(flipx);

                        //flip Y
                        cellRulesDefaltL.Add(FlipY(rule));

                        //flip X & Y
                        cellRulesDefaltL.Add(FlipY(flipx));

                        break;
                }
            }

        cellsRulles[0] = cellRulesDefaltL.ToArray();
        cellsRulles[1] = cellRulesOutpostL.ToArray();
        cellsRulles[2] = cellRulesStartL.ToArray();
        cellsRulles[3] = cellRulesEndL.ToArray();

    }

    private CellRules FlipX(CellRules value)
    {
        CellRules rule = value.Copy();
        rule.flipX = !rule.flipX;

        {
            CellRules[] temp;

            temp = rule.cellsDownLeft;
            rule.cellsDownLeft = rule.cellsDownRight;
            rule.cellsDownRight = temp;

            temp = rule.cellsLeft;
            rule.cellsLeft = rule.cellsRight;
            rule.cellsRight = temp;

            temp = rule.cellsUpLeft;
            rule.cellsUpLeft = rule.cellsUpRight;
            rule.cellsUpRight = temp;
        }

        return rule;
    }

    private CellRules FlipY(CellRules value)
    {
        CellRules rule = value.Copy();
        rule.flipY = !rule.flipY;

        {
            CellRules[] temp;

            temp = rule.cellsDownLeft;
            rule.cellsDownLeft = rule.cellsUpLeft;
            rule.cellsUpLeft = temp;

            temp = rule.cellsDownRight;
            rule.cellsDownRight = rule.cellsUpRight;
            rule.cellsUpRight = temp;
        }

        return rule;
    }

    private void GenerateGrid()
    {
        float degree = Random.Range(0f, Mathf.PI * 2f);
        float dist = Random.Range(settings.minMaxDistance.x, settings.minMaxDistance.y);
        worldSize = dist + settings.sizeOffset;
        int wSize = (int)(worldSize + 0.5f);
        world = new int[wSize, wSize];

        Vector2 center = new Vector2(Mathf.Sin(degree) * dist, Mathf.Cos(degree) * dist) / 2f;



        //make the map round
#if UNITY_EDITOR
        int cellCount = 0;
#endif

        for (int x = 0; x < wSize; x++) {
            for (int y = 0; y < wSize; y++) {
                if (Vector2.Distance(new Vector2(worldSize / 2f, worldSize / 2f), new Vector2(x + 0.5f + ((y & 1) * 0.5f), y + 0.5f)) < worldSize / 2f) {
#if UNITY_EDITOR
                    DrawCros(x, y, Color.green);
                    cellCount++;
#endif
                } else {
                    //set position to invalid
                    world[x, y] = -1;
#if UNITY_EDITOR
                    DrawCros(x, y, Color.red);
#endif
                }
            }
        }

        //define start and end positions
        world[(int)(worldSize / 2f - center.x + 0.5f), (int)(worldSize / 2f - center.y + 0.5f)] = 2;
        world[(int)(worldSize / 2f + center.x + 0.5f), (int)(worldSize / 2f + center.y + 0.5f)] = 3;


        //define outpost positions
        int outpostCount = 0;
        while (true) {
            int x = Random.Range(1, wSize - 1);
            int y = Random.Range(1, wSize - 1);

            if (world[x, y] == 0) {
                world[x, y] = 1;
                outpostCount++;
                if (outpostCount >= settings.outpostAmount) {
                    break;
                }
            }
        }

        Transform obj = Instantiate(settings.BackgroundWorldMapObj, transform).transform;
        obj.localScale *= worldSize * settings.gridSize;


#if UNITY_EDITOR
        debugData = $"Grid: {wSize}, {wSize}\nCell count: {cellCount} / {wSize * wSize}\nWorld size: {wSize * settings.gridSize}x{wSize * settings.gridSize}";
#endif

    }

    private void GenerateCells()
    {

        int wSize = (int)(worldSize + 0.5f);

        Cell[,] worldObjs = new Cell[wSize, wSize];

        for (int x = 0; x < wSize; x++) {
            for (int y = 0; y < wSize; y++) {
                //only valid positions
                if (world[x, y] != -1) {
                    //create cell
                    GameObject obj = Instantiate(cell, transform);
                    obj.transform.position = new Vector3(
                        (x + 0.5f) * settings.gridSize - worldSize * settings.gridSize / 2 + ((y & 1) * settings.gridSize / 2),
                        (y + 0.5f) * settings.gridSize - worldSize * settings.gridSize / 2);

                    //apply type to cell
                    worldObjs[x, y] = obj.GetComponent<Cell>();
                    worldObjs[x, y].type = (Enum.CellType)world[x, y];

                    switch (world[x, y]) {
                        case 0:
                            obj.name = $"{x},{y} Default";
                            break;
                        case 1:
                            obj.name = $"{x},{y} Outpost";
                            break;
                        case 2:
                            obj.name = $"{x},{y} Start";
                            break;
                        case 3:
                            obj.name = $"{x},{y} End";
                            break;
                    }

                } else {
                    worldObjs[x, y] = null;
                }
            }
        }
        for (int x = 0; x < wSize; x++) {
            for (int y = 0; y < wSize; y++) {
                if (worldObjs[x, y] is null)
                    continue;
                try {
                    if (x != 0) // left
                        worldObjs[x, y].nabors[2] = worldObjs[x - 1, y];
                    if (x < wSize - 1) //right
                        worldObjs[x, y].nabors[3] = worldObjs[x + 1, y];

                    if (y < wSize - 1) {
                        if (x - (y + 1 & 1) != -1)
                            worldObjs[x, y].nabors[0] = worldObjs[x - (y + 1 & 1), y + 1];
                        if (x + 1 - (y + 1 & 1) < wSize) {
                            worldObjs[x, y].nabors[1] = worldObjs[x + 1 - (y + 1 & 1), y + 1];
                        }
                    }

                    if (y != 0) {
                        if (x - (y + 1 & 1) != -1)
                            worldObjs[x, y].nabors[4] = worldObjs[x - (y + 1 & 1), y - 1];
                        if (x + 1 - (y + 1 & 1) < wSize)
                            worldObjs[x, y].nabors[5] = worldObjs[x + 1 - (y + 1 & 1), y - 1];
                    }
                } catch (System.Exception e) {
                    Debug.LogError(e);
                }
            }
        }
    }

    public void ScaleCemra(Camera cam)
    {
        float size = ((int)(worldSize + 0.5f)) * settings.gridSize / 2f + settings.gridSize;
        cam.orthographicSize = size;
    }

    public void Regenerate()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        for (int i = 0; i < children.Length; i++) {
            if (children[i].gameObject != gameObject)
                Destroy(children[i].gameObject);
        }
        //transform.position = new Vector3();
#if UNITY_EDITOR
        SeperateRules();
#endif
        GenerateGrid();
        GenerateCells();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Application.isPlaying) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(new Vector3(), worldSize * settings.gridSize / 2);
        } else {
            debugData = $"Min Max Distance: {settings.minMaxDistance}\nSize Offset: {settings.sizeOffset}\nGrid Size: {settings.gridSize}";
        }
    }

    private void DrawCros(int x, int y, Color color)
    {
        Debug.DrawLine(
            new Vector2(x * settings.gridSize - worldSize * settings.gridSize / 2 + ((y & 1) * settings.gridSize / 2),
                        y * settings.gridSize - worldSize * settings.gridSize / 2),
            new Vector2(x * settings.gridSize + settings.gridSize - worldSize * settings.gridSize / 2 + ((y & 1) * settings.gridSize / 2),
                        y * settings.gridSize + settings.gridSize - worldSize * settings.gridSize / 2),
            color, 2f);

        Debug.DrawLine(
            new Vector2(x * settings.gridSize + settings.gridSize - worldSize * settings.gridSize / 2 + ((y & 1) * settings.gridSize / 2),
                        y * settings.gridSize - worldSize * settings.gridSize / 2),
            new Vector2(x * settings.gridSize - worldSize * settings.gridSize / 2 + ((y & 1) * settings.gridSize / 2),
                        y * settings.gridSize + settings.gridSize - worldSize * settings.gridSize / 2),
            color, 2f);
    }

#endif

}
