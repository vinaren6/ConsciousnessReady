using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerationHandler : MonoBehaviour
{

    public static WorldGenerationHandler instance;

    public WorldGenSettings settings;
    public GameObject cell;

    public int[,] world;

    private float worldSize;
    private Vector2 center;

    [HideInInspector]
    public string debugData = "";

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogWarning("Detected multible instance");
            Destroy(this);
            return;
        }

        Generate();
        GenerateCells();
    }

    private void Generate() {
        float degree = Random.Range(0f, Mathf.PI * 2f);
        float dist = Random.Range(settings.minMaxDistance.x, settings.minMaxDistance.y);
        worldSize = dist + settings.sizeOffset;
        int wSize = (int)(worldSize + 0.5f);
        world = new int[wSize, wSize];

        center = new Vector2(Mathf.Sin(degree) * dist, Mathf.Cos(degree) * dist);



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
                } else {
                    world[x, y] = -1;
                    DrawCros(x, y, Color.red);
#endif
                }
            }
        }

        //place world in center
        transform.position = new Vector3(
            center.x * settings.gridSize / 2,
            center.y * settings.gridSize / 2);

#if UNITY_EDITOR
        debugData = $"Grid: {wSize}, {wSize}\nCell count: {cellCount} / {wSize * wSize}\nWorld size: {wSize * settings.gridSize}x{wSize * settings.gridSize}";
#endif

    }

    private void GenerateCells() {
        int wSize = (int)(worldSize + 0.5f);
        for (int x = 0; x < wSize; x++) {
            for (int y = 0; y < wSize; y++) {
                if (world[x, y] >= 0) {
                    GameObject obj = Instantiate(cell, transform);
                    obj.transform.position = new Vector3(
                        (x + 0.5f) * settings.gridSize - worldSize * settings.gridSize / 2 + ((y & 1) * settings.gridSize / 2),
                        (y + 0.5f) * settings.gridSize - worldSize * settings.gridSize / 2);
                }
            }
        }
    }

    public void Regenerate() {
        Transform[] children = GetComponentsInChildren<Transform>();
        for (int i = 0; i < children.Length; i++) {
            if (children[i].gameObject != gameObject)
                Destroy(children[i].gameObject);
        }
        transform.position = new Vector3();
        Generate();
        GenerateCells();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if (Application.isPlaying) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(new Vector3(), worldSize * settings.gridSize / 2);
        } else {
            debugData = $"Min Max Distance: {settings.minMaxDistance}\nSize Offset: {settings.sizeOffset}\nGrid Size: {settings.gridSize}";
        }
    }

    private void DrawCros(int x, int y, Color color) {
        Debug.DrawLine(
            new Vector2(x * settings.gridSize                     - worldSize * settings.gridSize / 2 + ((y & 1) * settings.gridSize / 2),
                        y * settings.gridSize                     - worldSize * settings.gridSize / 2),
            new Vector2(x * settings.gridSize + settings.gridSize - worldSize * settings.gridSize / 2 + ((y & 1) * settings.gridSize / 2),
                        y * settings.gridSize + settings.gridSize - worldSize * settings.gridSize / 2),
            color,    2f);

        Debug.DrawLine(
            new Vector2(x * settings.gridSize + settings.gridSize - worldSize * settings.gridSize / 2 + ((y & 1) * settings.gridSize / 2),
                        y * settings.gridSize                     - worldSize * settings.gridSize / 2),
            new Vector2(x * settings.gridSize                     - worldSize * settings.gridSize / 2 + ((y & 1) * settings.gridSize / 2),
                        y * settings.gridSize + settings.gridSize - worldSize * settings.gridSize / 2),
            color,    2f);
    }

#endif


}
