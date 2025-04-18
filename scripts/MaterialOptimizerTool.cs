using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class MaterialOptimizerTool
{
    [MenuItem("Tools/Optimize Materials 🚀")]
    public static void OptimizeMaterials()
    {
        var counter = 0;
        var materials = FindAssetsByType<Material>("t:Material");

        foreach (var material in materials.Where(material => !material.enableInstancing))
        {
            if (!material.enableInstancing)
            {
                material.enableInstancing = true;
                ++counter;
            }
        }

        Debug.Log($"{counter} materials has been optimized");
    }

    private static List<T> FindAssetsByType<T>(string filter) where T : Object
    {
        List<T> assets = new List<T>();
        string[] guids = AssetDatabase.FindAssets(filter);

        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

            if (asset != null)
                assets.Add(asset);
        }

        return assets;
    }
}