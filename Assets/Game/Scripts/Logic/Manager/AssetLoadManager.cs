using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using Object = UnityEngine.Object;

public class AssetBundleCache
{
    public AssetBundle assetBundle;
    public int refCount;
    public bool isHardCache;     // Can be unloaded ? 

    public AssetBundleCache(AssetBundle assetBundle, bool isHardCache = false)
    {
        this.assetBundle = assetBundle;
        this.isHardCache = isHardCache;
        refCount = 1;
    }
}

public static class AssetLoadManager
{
    private static Dictionary<string, AssetBundleCache> _abCache = new Dictionary<string, AssetBundleCache>();
    private static Dictionary<string, List<string>> _dependenciesMap = new Dictionary<string, List<string>>();

    private static AssetBundleManifest _abManifest;

#if UNITY_EDITOR
    public static bool SimulateAssetBundleInEditor
    {
        get
        {
            return EditorPrefs.GetBool("Pref_SimulateAssetBundle", false);
        }
    }
#endif

    public static void Init()
    {
#if UNITY_EDITOR
        if (SimulateAssetBundleInEditor)
#endif
        {
            _abManifest = LoadAsset<AssetBundleManifest>(PathUtil.PlatformName, "AssetBundleManifest", true);
            if (_abManifest == null)
            {
                Debug.LogError("[AssetLoadManager]: AssetBundleManifest null >> " + PathUtil.PlatformName);
                return;
            }
        }
    }

    public static T LoadAsset<T>(string assetBundleName, string assetName, bool isHardCache = false) where T : Object
    {
        return LoadAsset(assetBundleName, assetName, typeof(T), isHardCache) as T;
    }
    public static Object LoadAsset(string assetBundleName, string assetName, Type type, bool isHardCache = false)
    {
#if UNITY_EDITOR
        if (!SimulateAssetBundleInEditor)
        {
            string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(assetBundleName, assetName);
            if (assetPaths.Length == 0)
            {
                Debug.Log("[AssetLoadManager]:There is no asset with name \"" + assetName + "\" in " + assetBundleName);
                return null;
            }
            Object target = AssetDatabase.LoadAssetAtPath(assetPaths[0], type);
            return target;
        }
        else
#endif
        {
            LoadABAndDependencies(assetBundleName, isHardCache);
            AssetBundleCache loadedBundle;
            if (_abCache.TryGetValue(assetBundleName, out loadedBundle))
            {
                var obj = loadedBundle.assetBundle.LoadAsset(assetName, type);
                return obj;
            }
            else
            {
                return null;
            }
        }
    }

    private static void LoadABAndDependencies(string assetBundleName, bool isHardCache)
    {
        if (string.IsNullOrEmpty(assetBundleName))
            return;
        string[] deps = null;
        if (_abManifest != null)
        {
            deps = _abManifest.GetDirectDependencies(assetBundleName);
        }
        if (deps != null)
        {
            if (!_dependenciesMap.ContainsKey(assetBundleName))
            {
                _dependenciesMap.Add(assetBundleName, new List<string>(deps));
            }
            for (int i = 0; i < deps.Length; i++)
            {
                LoadABAndDependencies(deps[i], isHardCache);
            }
        }
        if (_abCache.ContainsKey(assetBundleName))
        {
            _abCache[assetBundleName].refCount += 1;
            return;
        }

        var path = PathUtil.GetAssetBundleDiskPath(assetBundleName);
        if (path != null)
        {
            Debug.Log("kiet log >> Load ab at path >> " + path);
            try
            {
                AssetBundle loadedBundle = AssetBundle.LoadFromFile(path);
                if (loadedBundle != null)
                {
                    Debug.Log("kiet log >> load ab success! " + assetBundleName);
                    _abCache.Add(assetBundleName, new AssetBundleCache(loadedBundle, isHardCache));
                }
                else
                {
                    Debug.LogError("[AssetloadManager]: bundle load fail >> " + path);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }

    public static void UnloadAssetBundle(string assetBundleName)
    {
#if UNITY_EDITOR
        if (!SimulateAssetBundleInEditor)
            return;
#endif

        AssetBundleCache bundle = _abCache[assetBundleName];
        if (bundle.isHardCache)
            return;

        List<string> dependencies = null;

        _dependenciesMap.TryGetValue(assetBundleName, out dependencies);

        if (dependencies != null)
        {
            int length = dependencies.Count;
            string depend = "";
            for (int i = 0; i < length; i++)
            {
                depend = dependencies[i];
                UnloadAssetBundle(depend);
            }
        }

        if (--bundle.refCount <= 0)
        {
            bundle.assetBundle.Unload(true);
            _abCache.Remove(assetBundleName);
            _dependenciesMap.Remove(assetBundleName);
        }
    }

    public static void PreloadAssetBundles(List<string> abNameList)
    {
        foreach (var abName in abNameList)
        {
            LoadAB(abName, true);
        }
    }
    public static void PreloadAssetBundle(string assetBundleName)
    {
        LoadAB(assetBundleName, true);
    }

    public static void LoadAB(string assetBundleName, bool isHardCache = false)
    {
#if UNITY_EDITOR
        if (SimulateAssetBundleInEditor)
#endif
        {
            LoadABAndDependencies(assetBundleName, isHardCache);
            AssetBundleCache bundle;
            if (!_abCache.TryGetValue(assetBundleName, out bundle))
            {
                Debug.LogError("[AssetLoadManager]: Load assetbundle fail >> " + assetBundleName);
            }
        }
    }
}

/* Update notes:
 * - Viết thêm hàm xử lý load async
*/