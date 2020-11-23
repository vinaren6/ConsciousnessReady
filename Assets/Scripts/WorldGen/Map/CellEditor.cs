#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Cell))]
public class CellEditor : Editor
{
    string s = "";
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        //if (s == "") {
            Cell controller = target as Cell;

            s = $"\n{controller.ruleId}\n\n";
            for (int i = 0; i < 6; i++) {
                s += (controller.nabors[i] == null ? "null" : controller.nabors[i].ToString()) + '\n';
            }
        //}

        GUILayout.Label(s);

    }
}

#endif