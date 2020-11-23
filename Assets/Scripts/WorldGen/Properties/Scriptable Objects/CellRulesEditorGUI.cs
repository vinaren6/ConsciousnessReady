#if UNITY_EDITOR
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
            padding = new RectOffset()
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
                    fixedHeight = 128
                };
                GUILayout.BeginVertical(centerStyle);
                if (!(controller.preview is null)) {
                    GUIStyle imgStyle = new GUIStyle {
                        margin = new RectOffset()
                    };
                    GUILayout.Box(controller.preview, imgStyle);
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
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
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

        if (cellArray.Length != 0 && !(cellArray[cellArray.Length - 1] is null || cellArray[cellArray.Length - 1].preview is null)) {
            GUIStyle imgStyle = new GUIStyle {
                margin = new RectOffset()
            };

            GUILayout.Box(cellArray[cellArray.Length - 1].preview, imgStyle);
        }

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