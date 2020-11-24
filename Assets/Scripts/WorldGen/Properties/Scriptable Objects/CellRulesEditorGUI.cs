#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CellRules))]
public class CellRulesEditorGUI : Editor
{
    Vector2[] scrollPosition = null;
    int scrollIndex = 0;


    public override void OnInspectorGUI() {
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
            padding = new RectOffset(16,0,0,0)
        };

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

                Rect myRect = GUILayoutUtility.GetRect(128, 128);
                GUI.Box(myRect, "Drag and Drop to this Box!");
                if (myRect.Contains(Event.current.mousePosition)) {
                    if (Event.current.type == EventType.DragUpdated) {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                        Event.current.Use();
                    } else if (Event.current.type == EventType.DragPerform) {
                        Debug.Log(DragAndDrop.objectReferences.Length);

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

                /*
                if (!(controller.preview is null)) {
                    GUIStyle imgStyle = new GUIStyle {
                        margin = new RectOffset()
                    };
                    GUILayout.Box(controller.preview, imgStyle);
                } else
                */
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
        DrawDefaultInspector();
    }
    private void DrawList(GUIStyle boxStyle, SerializedProperty cell, CellRules[] cellArray) {

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

    
}
#endif