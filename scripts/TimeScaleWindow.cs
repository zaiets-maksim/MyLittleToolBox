using UnityEditor;
using UnityEngine;

public class TimeScaleWindow : EditorWindow
{
    private static TimeScaleWindow _window;
    private float _timeScale = 1f;
    private float _miтScale = 0f;
    private float _maxScale = 1f;

    [MenuItem("Tools/TimeScale Window 🕓")]
    public static void OpenWindow()
    {
        if (_window == null)
        {
            _window = GetWindow<TimeScaleWindow>("Time Scale");
            _window.minSize = new Vector2(250f, 125f);
            _window.maxSize = new Vector2(500f, 125f);
        }
        _window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Adjust Time Scale", EditorStyles.boldLabel);

        _miтScale = EditorGUILayout.FloatField("Min Scale", _miтScale);
        _maxScale = EditorGUILayout.FloatField("Max Scale", _maxScale);

        if (_miтScale > _maxScale) 
            _miтScale = _maxScale;

        _timeScale = EditorGUILayout.Slider("Time Scale", _timeScale, _miтScale, _maxScale);
        Time.timeScale = _timeScale;
    }
}
