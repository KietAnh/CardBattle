using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class PrebuildEditor : EditorWindow
{
    [MenuItem("File/Pre-build Settings...")]
    static void Init()
    {
        PrebuildEditor window = (PrebuildEditor)EditorWindow.GetWindow(typeof(PrebuildEditor));
        window.Show();
    }

    AppInfo appInfo;
    string managersString;

    private void Awake()
    {
        Debug.Log("prebuild awake");
        InitAppInfo();
    }

    private void InitAppInfo()
    {
        appInfo = new AppInfo();
        string currentVersion = Application.version;
        appInfo.appVersion = int.Parse(currentVersion.Replace(".", string.Empty));
        appInfo.isUpdateStore = false;
        appInfo.assetBundleUrl = "/res/GameAB/{0}";
        appInfo.managers = new List<string>() { "SpawnManager", "GCManager", "IAPManager" };
        managersString = "";
        foreach (var manager in appInfo.managers)
        {
            managersString += manager + ",";
        }
        managersString = managersString.Remove(managersString.Length - 1);
    }

    void OnGUI()
    {
        //GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        //myString = EditorGUILayout.TextField("Text Field", myString);

        //groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        //myBool = EditorGUILayout.Toggle("Toggle", myBool);
        //myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        //EditorGUILayout.EndToggleGroup();

        GUILayout.Label("App Info", EditorStyles.boldLabel);
        appInfo.appVersion = int.Parse(EditorGUILayout.TextField("App version", appInfo.appVersion.ToString()));
        appInfo.isUpdateStore = EditorGUILayout.Toggle("Is update on store", appInfo.isUpdateStore);
        appInfo.assetBundleUrl = EditorGUILayout.TextField("AB Url", appInfo.assetBundleUrl);
        managersString = EditorGUILayout.TextField("Managers", managersString);
        if (GUILayout.Button("Build AssetBundles"))
        {
            AssetBundleEditor.BuildABs();
        }
        if (GUILayout.Button("Export app_info.json"))
        {
            ExportAppInfoFile();
        }
    }

    public const string APP_INFO_PATH = "/Game/Resources/app_info.json";
    private void ExportAppInfoFile()
    {
        appInfo.managers = new List<string>(managersString.Split(','));
        appInfo.abFiles = new List<FileEntry>();
        string outputPath = string.Format(AssetBundleEditor.OUTPUT_AB_FOLDER_PATH, Application.streamingAssetsPath, PathUtil.PlatformName);
        string[] abFilePaths = Directory.GetFiles(outputPath);
        foreach (string abFilePath in abFilePaths)
        {
            string path = abFilePath.Replace("\\", "/");
            if (path.Contains("meta") || path.Contains("manifest"))
                continue;

            FileEntry fileEntry = new FileEntry();
            fileEntry.resName = path.Substring(path.LastIndexOf("/") + 1);
            fileEntry.size = (long)Math.Ceiling(new FileInfo(path).Length / 1024f);

            appInfo.abFiles.Add(fileEntry);
        }

        string jsonStr = JsonUtility.ToJson(appInfo, true);
        string appInfoFilePath = Application.dataPath + APP_INFO_PATH;   // chuyển tất cả những gì liên quan đến path vào pathUtils cho dễ quản lý
        //if (!File.Exists(appInfoFilePath))
        //{
        //    File.Create(appInfoFilePath);
        //}
        Debug.Log(jsonStr);
        Debug.Log(appInfoFilePath);
        using (StreamWriter writer = new StreamWriter(appInfoFilePath))
        {
            writer.WriteLine(jsonStr);
        }
    }
}
