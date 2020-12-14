#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class NameRand : MonoBehaviour
{
    [TextArea(15, 20)]
    public string text;
    void Start()
    {
        TextMesh textComp = GetComponent<TextMesh>();
        string[] s = text.Split('\n');
        textComp.text = s[Random.Range(0, s.Length)];
    }
}

/*
#if UNITY_EDITOR
[CustomEditor(typeof(NameRand))]
public class TestUI : Editor
{
    public override void OnInspectorGUI()
    {
        NameRand test = target as NameRand;
        EditorGUILayout.LabelField("Names");
        test.text = EditorGUILayout.TextArea(test.text.Replace(",",",\n")).Replace("\n", "").Trim();
    }
}
#endif
*/