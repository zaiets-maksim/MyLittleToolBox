using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class SceneNavigator : EditorWindow
{
    private const int MaxHistory = 10;
    
    private static Stack<string> _sceneHistory = new();
    private static List<string> _tempList;
    private static string _currentScene;

    private void OnEnable()
    {
        EditorSceneManager.sceneOpened += OnSceneOpened;
        _currentScene = SceneManager.GetActiveScene().path;
    }

    private void OnDisable()
    {
        EditorSceneManager.sceneOpened -= OnSceneOpened;
    }

    public static void ShowWindow() => GetWindow<SceneNavigator>("Scene Navigator");

    private void OnSceneOpened(Scene scene, OpenSceneMode mode)
    {
        if (!string.IsNullOrEmpty(_currentScene) && _currentScene != scene.path)
        {
            _sceneHistory.Push(_currentScene);
            if (_sceneHistory.Count > MaxHistory)
            {
                _tempList = new List<string>(_sceneHistory);
                _tempList.RemoveAt(0);
                _sceneHistory = new Stack<string>(_tempList);
            }
        }
        _currentScene = scene.path;
    }

    private void OnGUI()
    {
        GUILayout.Label("Scenes in Build Settings:", EditorStyles.boldLabel);

        foreach (var scene in EditorBuildSettings.scenes)
            if (GUILayout.Button($"Open {scene.path}")) 
                OpenScene(scene.path);

        GUILayout.Space(10);
        GUILayout.Label("Recent Scenes:", EditorStyles.boldLabel);

        foreach (var scenePath in _sceneHistory)
            if (GUILayout.Button($"Open {scenePath}")) 
                OpenScene(scenePath);
    }

    private void OpenScene(string scenePath)
    {
        if (scenePath != _currentScene) 
            EditorSceneManager.OpenScene(scenePath);
    }
}


internal abstract class SceneNavigatorShortcut
{
    const string ShortcutId = "MyTools/OpenSceneNavigator";

    [Shortcut(ShortcutId, KeyCode.O, ShortcutModifiers.Control | ShortcutModifiers.Shift)]
    static void SpecialActionShortcut() => SceneNavigator.ShowWindow();
}
