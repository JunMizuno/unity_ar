using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetBundleEditor
{
    static string ROOT_PATH = "AssetBundles";
    static string variant = "assetbundle";

    [MenuItem("AssetBundle/Build")]
    static private void BuildAssetBundles()
    {
        UnityEditor.BuildTarget targetFlatform = UnityEditor.BuildTarget.StandaloneOSX;

        var outputPath = System.IO.Path.Combine(ROOT_PATH, targetFlatform.ToString());

        if (System.IO.Directory.Exists(outputPath) == false)
        {
            System.IO.Directory.CreateDirectory(outputPath);
        }

        var assetBundleBuildList = new List<UnityEditor.AssetBundleBuild>();

        foreach (string assetBundleName in UnityEditor.AssetDatabase.GetAllAssetBundleNames())
        {
            var builder = new AssetBundleBuild();

            builder.assetBundleName = assetBundleName;
            builder.assetNames = UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundle(builder.assetBundleName);
            builder.assetBundleVariant = variant;
            assetBundleBuildList.Add(builder);
        }

        if (assetBundleBuildList.Count > 0)
        {
            UnityEditor.BuildPipeline.BuildAssetBundles(outputPath, assetBundleBuildList.ToArray(), UnityEditor.BuildAssetBundleOptions.ChunkBasedCompression, targetFlatform);
        }
    }
}
