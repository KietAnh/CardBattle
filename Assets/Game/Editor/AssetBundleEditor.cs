using UnityEngine;
using UnityEditor;
using System.IO;

public class AssetBundleEditor
{
    [MenuItem("Assets/Build Asset Bundles")]
    public static void BuildABs()
    {
        AssignBundleTag();

        //string outputPath = string.Format("{0}/res/GameAB/{1}", PathUtil.persistentDataPath, PathUtil.PlatformName);
        string outputPath = string.Format(OUTPUT_AB_FOLDER_PATH, Application.streamingAssetsPath, PathUtil.PlatformName);
        BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
    }

    public const string OUTPUT_AB_FOLDER_PATH = "{0}/res/GameAB/{1}";
    public const string INPUT_AB_FOLDER_PATH = "/Game/BuildAB";

    private static void AssignBundleTag()
    {
        string[] dirPaths = Directory.GetDirectories(Application.dataPath + INPUT_AB_FOLDER_PATH);
        foreach (var dirPath in dirPaths)
        {
            string[] assetPaths = Directory.GetFiles(dirPath);
            string[] subDirPaths = Directory.GetDirectories(dirPath);
            string folderName = dirPath.Substring(dirPath.LastIndexOf('\\') + 1);
            foreach (var subDirPath in subDirPaths)
            {
                string[] subAssetPaths = Directory.GetFiles(subDirPath);
                string subFolderName = subDirPath.Substring(subDirPath.LastIndexOf('\\') + 1);
                foreach (string assetPath in subAssetPaths)
                {
                    var path = GetAssetPath(assetPath);
                    if (IsMetaFile(path))
                        continue;
                    string bundleName = GetBundleName(path, folderName, subFolderName);
                    AssetImporter.GetAtPath(path).SetAssetBundleNameAndVariant(bundleName, "");
                }
            }
            foreach (string assetPath in assetPaths)
            {
                var path = GetAssetPath(assetPath);
                if (IsMetaFile(path))
                    continue;
                string bundleName = GetBundleName(path, folderName);
                AssetImporter.GetAtPath(path).SetAssetBundleNameAndVariant(bundleName, "");
            }
        }
    }
    private static string GetBundleName(string assetPath, string folderName, string subFolderName = "")
    {
        string assetName = assetPath.Substring(assetPath.LastIndexOf("/") + 1);
        assetName = assetName.Remove(assetName.IndexOf('.'));
        string bundleNameSuffix = subFolderName == "" ? assetName : subFolderName;
        string bundleName = string.Format("{0}_{1}", folderName, bundleNameSuffix).ToLower();
        return bundleName;
    }
    private static string GetAssetPath(string fullAssetPath)
    {
        return fullAssetPath.Replace("\\", "/").Substring(fullAssetPath.IndexOf("Assets"));
    }
    private static bool IsMetaFile(string path)
    {
        return path.Substring(path.LastIndexOf('.') + 1) == "meta";
    }
}
