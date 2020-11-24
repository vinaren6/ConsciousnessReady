#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

/// <summary>
/// Scene preview window.
/// http://diegogiacomelli.com.br/unitytips-scene-preview-window
/// </summary>
public class ScenePreviewWindow : EditorWindow
{
    const float EditorMargin = 1;
    const float PreviewMargin = 1;
    const int CaptureScreenshotButtonHeight = 30;
    const float SecondsToAutoCaptureScreenshot = 10f;

#pragma warning disable IDE0044 // Add readonly modifier
    static GUIStyle _buttonStyle = new GUIStyle();
#pragma warning restore IDE0044 // Add readonly modifier
    public static ScenePreviewWindow _instance;

    [MenuItem("Window/Scene Preview")]
    public static void ShowWindow() {
        _instance = EditorWindow.GetWindow<ScenePreviewWindow>("Scene Preview");
    }

    [InitializeOnLoadMethod]
    static void Initialize() {
        Selection.selectionChanged += HandleSelectionChange;
        EditorApplication.playModeStateChanged += HandlePlayModeStateChanged;
        EditorSceneManager.sceneOpened += HandleEditorSceneManagerSceneOpened;
    }

    void OnEnable() {
        _instance = this;
        _buttonStyle.normal.background = null;
    }

    void OnDestroy() {
        Selection.selectionChanged -= HandleSelectionChange;
        EditorApplication.playModeStateChanged -= HandlePlayModeStateChanged;
        EditorSceneManager.sceneOpened -= HandleEditorSceneManagerSceneOpened;
        _instance = null;
    }

    static void HandleSelectionChange() {
        if (_instance != null) {
            ScenePreviewUtility.RefreshTextures(_instance);
        }
    }

    static void HandlePlayModeStateChanged(PlayModeStateChange playMode) {
        if (playMode == PlayModeStateChange.EnteredPlayMode && _instance != null) {
            EditorApplication.update += HandleUpdate;
            HandleSelectionChange();
        }
    }

    static void HandleEditorSceneManagerSceneOpened(Scene scene, OpenSceneMode mode) {
        HandleSelectionChange();
    }

    static void HandleUpdate() {
        if (Time.timeSinceLevelLoad > SecondsToAutoCaptureScreenshot) {
            EditorApplication.update -= HandleUpdate;

            if (_instance != null && ScenePreviewScreenshot.Capture(false))
                ScenePreviewUtility.RefreshTextures(_instance);
        }
    }

    void OnGUI() {

        if (ScenePreviewUtility.CellRule != null) {
            GUIStyle containerStyle = new GUIStyle("HelpBox") {
                fixedWidth = 128 * 3 + 8
            };

            GUIStyle boxStyle = new GUIStyle("GroupBox") {
                fixedWidth = 128,
                fixedHeight = 128,
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
                    DrawList(boxStyle, ScenePreviewUtility.CellRule.cellsUpLeft);
                    DrawList(boxStyle, ScenePreviewUtility.CellRule.cellsUpRight);
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                {
                    DrawList(boxStyle, ScenePreviewUtility.CellRule.cellsLeft);
                    GUILayout.BeginVertical(boxStyle);
                    if (!(ScenePreviewUtility.CellRule.preview is null)) {
                        GUIStyle imgStyle = new GUIStyle {
                            margin = new RectOffset()
                        };
                        GUILayout.Box(ScenePreviewUtility.CellRule.preview, imgStyle);
                    } else
                        GUILayout.Label("");
                    GUILayout.EndVertical();
                    DrawList(boxStyle, ScenePreviewUtility.CellRule.cellsRight);
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space(64);
                    DrawList(boxStyle, ScenePreviewUtility.CellRule.cellsDownLeft);
                    DrawList(boxStyle, ScenePreviewUtility.CellRule.cellsDownRight);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        } else {

            var rect = new Rect(EditorMargin, EditorMargin, Screen.width, position.height - EditorMargin * 2 - PreviewMargin - CaptureScreenshotButtonHeight - EditorMargin);
            var data = ScenePreviewUtility.Data;

            if (data.Length > 0) {
                var previewsCount = data.Length;
                var height = (position.height - EditorMargin * 2 - (PreviewMargin * previewsCount) - CaptureScreenshotButtonHeight) / previewsCount;
                var index = 0;

                foreach (var item in data) {
                    rect = new Rect(EditorMargin, index * (height + PreviewMargin), position.width, height);

                    if (GUI.Button(rect, string.Empty, _buttonStyle)) {
                        EditorSceneManager.OpenScene(item.ScenePath);
                    }

                    GUI.DrawTexture(rect, item.Texture, ScaleMode.ScaleToFit);

                    index++;
                }
            }

            if (ScenePreviewUtility.ShowCaptureScreenshotButton &&
             GUI.Button(new Rect(EditorMargin, rect.yMax + PreviewMargin, position.width - (EditorMargin * 2), CaptureScreenshotButtonHeight), "Capture screenshot")) {
                ScenePreviewScreenshot.Capture();
                ScenePreviewUtility.RefreshTextures(this);
            }
        }

    }
    private void DrawList(GUIStyle boxStyle, CellRules[] cellArray) {

        GUILayout.BeginVertical(boxStyle, GUILayout.Height(128));

        if (cellArray.Length != 0 && !(cellArray[cellArray.Length - 1] is null || cellArray[cellArray.Length - 1].preview is null)) {
            GUILayout.Box(cellArray[cellArray.Length - 1].preview, boxStyle);
        } else
            GUILayout.Label("");

        GUILayout.EndVertical();
    }
}
#endif