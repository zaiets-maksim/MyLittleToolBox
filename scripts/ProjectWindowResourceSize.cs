using UnityEditor;
using UnityEngine;
using System.IO;

[InitializeOnLoad]
public class ProjectWindowResourceSize
{
    private static FileInfo _fileInfo;
    private static Rect _sizeRect;
    private static string _sizeText;
    private static float _sizeInKB;

    static ProjectWindowResourceSize()
    {
        EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
    }

    private static void OnProjectWindowItemGUI(string guid, Rect rect)
    {
        string path = AssetDatabase.GUIDToAssetPath(guid);

        if (IsValidAsset(path) && !Directory.Exists(path))
        {
            _sizeInKB = GetAssetSizeInKB(path);
            _sizeText = $"{_sizeInKB:F0} KB";

            _sizeRect = new Rect(rect.x + rect.width - 100, rect.y, 100, rect.height);
            GUI.Label(_sizeRect, _sizeText, EditorStyles.label);
        }
    }

    private static bool IsValidAsset(string path) => path.StartsWith("Assets") && !path.EndsWith(".cs");

    private static float GetAssetSizeInKB(string path)
    {
        _fileInfo = new FileInfo(path);
        return _fileInfo.Exists ? _fileInfo.Length / 1024f : 0f;
    }
}
