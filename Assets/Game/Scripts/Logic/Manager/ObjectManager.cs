using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectManager : SingletonTemplate<ObjectManager>   // refactor, caching
{
    private const string OBJECT_NAME_FORMAT = "obj_{0}";
    public GameObject GetObject(string objectName, Transform parent = null)   // update later
    {
        return LoadObject(objectName, parent);        
    }
    private GameObject LoadObject(string objectName, Transform parent = null)
    {
        string abName = string.Format(OBJECT_NAME_FORMAT, objectName);
        var objectPrefab = AssetLoadManager.LoadAsset<GameObject>(abName, objectName);
        if (objectPrefab != null)
        {
            GameObject obj = GameObject.Instantiate(objectPrefab, parent);
            obj.name = objectName;
            return obj;
        }
        return null;
    }

    public GameObject LoadUIObject(string assetBundleName, string assetName, Transform parent = null)
    {
        var objectPrefab = AssetLoadManager.LoadAsset<GameObject>(assetBundleName, assetName);
        if (objectPrefab != null)
        {
            GameObject obj = GameObject.Instantiate(objectPrefab, parent, false);
            obj.name = assetName;
            return obj;
        }
        return null;
    }

    private Dictionary<string, IPoolObject> _objCache = new Dictionary<string, IPoolObject>();

    public T GetPoolObject<T>(string objectName, Transform parent) where T : IPoolObject 
    {
        //if (_objCache.ContainsKey(objectName))
        //{
        //    IPoolObject poolObject = _objCache[objectName];
        //    poolObject.Reset();
        //    poolObject.SetParent(parent, false);
        //    return poolObject.gameObj;
        //}
        return null;
    }
}

public abstract class IPoolObject        // update later
{
    public GameObject gameObj;
    public void SetParent(Transform parent, bool worldPositionStays)
    {
        gameObj.transform.SetParent(parent, worldPositionStays);
    }
    public virtual void Reset() { }
    
}
