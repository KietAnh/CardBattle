using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class AssetBundleSimulator
{
    private const string MENU_PATH = "Assets/Simulate Asset Bundle";
    public static bool isEnable = false;
    static AssetBundleSimulator()
    {
        isEnable = EditorPrefs.GetBool("Pref_SimulateAssetBundle", false);
        EditorApplication.delayCall += () =>
        {
            ShowOrHideCheckmark();
        };
    }
    static void ShowOrHideCheckmark()
    {
        Menu.SetChecked(MENU_PATH, isEnable);
    }
    [MenuItem(MENU_PATH)]
    static void SimulateAssetBundle()
    {
        isEnable = !isEnable;
        EditorPrefs.SetBool("Pref_SimulateAssetBundle", isEnable);
        ShowOrHideCheckmark();
    }
}
