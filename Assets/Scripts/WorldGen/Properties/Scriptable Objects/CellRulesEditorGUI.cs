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


        GUIStyle containerStyle = new GUIStyle("HelpBox");
        containerStyle.fixedWidth = 128 * 3 + 8;

        GUIStyle boxStyle = new GUIStyle("GroupBox");
        boxStyle.fixedWidth = 128;
        boxStyle.margin = new RectOffset(0, 0, 0, 0);
        boxStyle.padding = new RectOffset(16, 0, 0, 0);

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
                GUILayout.BeginVertical(boxStyle, GUILayout.Height(128));
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
        scrollIndex = scrollIndex % 6;
        scrollPosition[scrollIndex] = GUILayout.BeginScrollView(scrollPosition[scrollIndex], boxStyle, GUILayout.Height(128));
        scrollIndex++;

        if (EditorGUILayout.PropertyField(cell, false))
            foreach (CellRules item in cellArray) {
                if (item != null)
                    GUILayout.Label(item.ToString());
                else
                    GUILayout.Label("None");
            }

        GUILayout.EndScrollView();
    }
}
#endif