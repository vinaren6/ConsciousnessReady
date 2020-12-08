#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(CellRules))]
public class CellRulesEditorGUI : Editor
{
    Vector2[] scrollPosition = null;
    int scrollIndex = 0;


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (scrollPosition == null || scrollPosition.Length < 6) {
            scrollPosition = new Vector2[6];
            for (int i = 0; i < scrollPosition.Length; i++) {
                scrollPosition[i] = new Vector2();
            }
        }
        scrollIndex = 0;

        CellRules controller = target as CellRules;
        SerializedProperty cellsUpLeft = serializedObject.FindProperty("cellsUpLeft");
        SerializedProperty cellsUpRight = serializedObject.FindProperty("cellsUpRight");
        SerializedProperty cellsLeft = serializedObject.FindProperty("cellsLeft");
        SerializedProperty cellsRight = serializedObject.FindProperty("cellsRight");
        SerializedProperty cellsDownLeft = serializedObject.FindProperty("cellsDownLeft");
        SerializedProperty cellsDownRight = serializedObject.FindProperty("cellsDownRight");


        GUIStyle containerStyle = new GUIStyle("HelpBox") {
            fixedWidth = 128 * 3 + 8
        };

        GUIStyle boxStyle = new GUIStyle("GroupBox") {
            fixedWidth = 128,
            margin = new RectOffset(),
            padding = new RectOffset(16, 0, 0, 0)
        };

        bool draged = false;

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical(containerStyle);
        {
            EditorGUI.BeginChangeCheck();
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(64);
                DrawList(boxStyle, cellsUpLeft, controller.cellsUpLeft);
                DrawList(boxStyle, cellsUpRight, controller.cellsUpRight);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            {
                DrawList(boxStyle, cellsLeft, controller.cellsLeft);
                GUIStyle centerStyle = new GUIStyle(boxStyle) {
                    fixedHeight = 128,
                    padding = new RectOffset()
                };

                GUILayout.BeginVertical(centerStyle);
                {
                    GUIStyle boxStyle2 = new GUIStyle(boxStyle) {
                        fixedHeight = 64,
                        padding = new RectOffset()
                    };
                    Rect myRect = GUILayoutUtility.GetRect(128, 64);
                    GUI.Box(myRect, "\nCopy to\nall Cells", boxStyle2);
                    if (myRect.Contains(Event.current.mousePosition)) {
                        draged = true;
                        if (DragAndDrop.objectReferences.Length > 0 && DragAndDrop.objectReferences[0].GetType() == typeof(CellRules))
                            if (Event.current.type == EventType.DragUpdated) {
                                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                                Event.current.Use();
                            } else if (Event.current.type == EventType.DragPerform) {
                                List<CellRules> downLeft = new List<CellRules>(controller.cellsDownLeft);
                                List<CellRules> downRight = new List<CellRules>(controller.cellsDownRight);
                                List<CellRules> left = new List<CellRules>(controller.cellsLeft);
                                List<CellRules> right = new List<CellRules>(controller.cellsRight);
                                List<CellRules> upLeft = new List<CellRules>(controller.cellsUpLeft);
                                List<CellRules> upRight = new List<CellRules>(controller.cellsUpRight);
                                for (int i = 0; i < DragAndDrop.objectReferences.Length; i++) {
                                    downLeft.Add(DragAndDrop.objectReferences[i] as CellRules);
                                    downRight.Add(DragAndDrop.objectReferences[i] as CellRules);
                                    left.Add(DragAndDrop.objectReferences[i] as CellRules);
                                    right.Add(DragAndDrop.objectReferences[i] as CellRules);
                                    upLeft.Add(DragAndDrop.objectReferences[i] as CellRules);
                                    upRight.Add(DragAndDrop.objectReferences[i] as CellRules);
                                }
                                controller.cellsDownLeft = downLeft.ToArray();
                                controller.cellsDownRight = downRight.ToArray();
                                controller.cellsLeft = left.ToArray();
                                controller.cellsRight = right.ToArray();
                                controller.cellsUpLeft = upLeft.ToArray();
                                controller.cellsUpRight = upRight.ToArray();

                                Event.current.Use();
                                ScenePreviewWindow._instance.Repaint();
                            }
                    }
                }
                {
                    Rect myRect = GUILayoutUtility.GetRect(128, 64);
                    GUI.Box(myRect, "\nRemove from\nall Cells");
                    if (myRect.Contains(Event.current.mousePosition)) {
                        draged = true;
                        if (DragAndDrop.objectReferences.Length > 0 && DragAndDrop.objectReferences[0].GetType() == typeof(CellRules))
                            if (Event.current.type == EventType.DragUpdated) {
                                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                                Event.current.Use();
                            } else if (Event.current.type == EventType.DragPerform) {
                                List<CellRules> downLeft = new List<CellRules>(controller.cellsDownLeft);
                                List<CellRules> downRight = new List<CellRules>(controller.cellsDownRight);
                                List<CellRules> left = new List<CellRules>(controller.cellsLeft);
                                List<CellRules> right = new List<CellRules>(controller.cellsRight);
                                List<CellRules> upLeft = new List<CellRules>(controller.cellsUpLeft);
                                List<CellRules> upRight = new List<CellRules>(controller.cellsUpRight);
                                for (int i = 0; i < DragAndDrop.objectReferences.Length; i++) {
                                    downLeft.Remove(DragAndDrop.objectReferences[i] as CellRules);
                                    downRight.Remove(DragAndDrop.objectReferences[i] as CellRules);
                                    left.Remove(DragAndDrop.objectReferences[i] as CellRules);
                                    right.Remove(DragAndDrop.objectReferences[i] as CellRules);
                                    upLeft.Remove(DragAndDrop.objectReferences[i] as CellRules);
                                    upRight.Remove(DragAndDrop.objectReferences[i] as CellRules);
                                }
                                controller.cellsDownLeft = downLeft.ToArray();
                                controller.cellsDownRight = downRight.ToArray();
                                controller.cellsLeft = left.ToArray();
                                controller.cellsRight = right.ToArray();
                                controller.cellsUpLeft = upLeft.ToArray();
                                controller.cellsUpRight = upRight.ToArray();

                                Event.current.Use();
                                ScenePreviewWindow._instance.Repaint();
                            }
                    }
                }
                GUILayout.Label("");
                GUILayout.EndVertical();
                DrawList(boxStyle, cellsRight, controller.cellsRight);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(64);
                DrawList(boxStyle, cellsDownLeft, controller.cellsDownLeft);
                DrawList(boxStyle, cellsDownRight, controller.cellsDownRight);
            }
            GUILayout.EndHorizontal();
            if (EditorGUI.EndChangeCheck()) {
                serializedObject.ApplyModifiedProperties();
                ScenePreviewWindow._instance.Repaint();
            }
        }
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Normalize")) {
            Normalize(controller);
        }

        
        if (!draged) {
            if (DragAndDrop.objectReferences.Length > 0 && DragAndDrop.objectReferences[0].GetType() == typeof(SceneAsset))
                if (Event.current.type == EventType.DragUpdated) {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    Event.current.Use();
                } else if (Event.current.type == EventType.DragPerform) {

                    controller.title = (DragAndDrop.objectReferences[0] as SceneAsset).name;
                    controller.id = SceneUtility.GetBuildIndexByScenePath(DragAndDrop.paths[0]);
                    controller.preview = AssetDatabase.LoadAssetAtPath($"Assets/Scripts/ScenePreview/Previews/{controller.title}.png", typeof(Texture2D)) as Texture2D;

                    Event.current.Use();
                    ScenePreviewWindow._instance.Repaint();
                }
        }
        

        DrawDefaultInspector();
    }
    private void DrawList(GUIStyle boxStyle, SerializedProperty cell, CellRules[] cellArray)
    {

        scrollIndex %= 6;
        scrollPosition[scrollIndex] = GUILayout.BeginScrollView(scrollPosition[scrollIndex], boxStyle, GUILayout.Height(128));
        scrollIndex++;

        GUIStyle style = new GUIStyle {
            padding = new RectOffset(16, 0, 0, 0)
        };

        if (EditorGUILayout.PropertyField(cell, false))
            foreach (CellRules item in cellArray) {
                if (item != null)
                    GUILayout.Label(item.ToString(), style);
                else
                    GUILayout.Label("None", style);
            }

        GUILayout.EndScrollView();
    }

    public void Normalize(CellRules controller)
    {
        foreach (CellRules item in controller.cellsDownLeft)
            if (!ExistInArray(item.cellsUpRight, controller))
                item.cellsUpRight = AddElementToArray(item.cellsUpRight, controller);
        foreach (CellRules item in controller.cellsDownRight)
            if (!ExistInArray(item.cellsUpLeft, controller))
                item.cellsUpLeft = AddElementToArray(item.cellsUpLeft, controller);
        foreach (CellRules item in controller.cellsLeft)
            if (!ExistInArray(item.cellsRight, controller))
                item.cellsRight = AddElementToArray(item.cellsRight, controller);
        foreach (CellRules item in controller.cellsRight)
            if (!ExistInArray(item.cellsLeft, controller))
                item.cellsLeft = AddElementToArray(item.cellsLeft, controller);
        foreach (CellRules item in controller.cellsUpLeft)
            if (!ExistInArray(item.cellsDownRight, controller))
                item.cellsDownRight = AddElementToArray(item.cellsDownRight, controller);
        foreach (CellRules item in controller.cellsUpRight)
            if (!ExistInArray(item.cellsDownLeft, controller))
                item.cellsDownLeft = AddElementToArray(item.cellsDownLeft, controller);
    }

    private bool ExistInArray<T>(T[] array, T item)
    {
        if (item != null)
            foreach (T arrayItem in array)
                if (arrayItem.Equals(item))
                    return true; //mach found
        return false; //no mach
    }

    T[] AddElementToArray<T>(T[] array, T element)
    {
        T[] newArray = new T[array.Length + 1];
        int i;
        for (i = 0; i < array.Length; i++)
            newArray[i] = array[i];
        newArray[i] = element;
        return newArray;
    }
}
#endif