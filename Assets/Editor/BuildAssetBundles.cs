using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string bundleDirectory = "Assets/AssetBundles";
        if (!Directory.Exists(bundleDirectory))
        {
            Directory.CreateDirectory(bundleDirectory);
        }

        // Build Asset Bundles for the current platform
        BuildPipeline.BuildAssetBundles(bundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

        // Add extension to asset bundles after building them
        string[] files = Directory.GetFiles(bundleDirectory);
        foreach (string file in files)
        {
            // Add the .bundle extension if it's not already there
            if (Path.GetExtension(file) == "")
            {
                File.Move(file, file + ".bundle");
            }
        }

        Debug.Log("Asset Bundles built and extensions added successfully!");
    }
}
