using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldGenerationHandler))]
public class WorldGenerationHandlerEditorGUI : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        WorldGenerationHandler myScript = (WorldGenerationHandler)target;
        if (Application.isPlaying) {
            if (GUILayout.Button("Regenerate")) {
                myScript.Regenerate();
            }
        } else {
            var style = new GUIStyle(GUI.skin.button);
            style.normal.textColor = Color.grey;
            GUILayout.Button("Regenerate", style);
        }

        GUILayout.Label(myScript.debugData);

    }

}
