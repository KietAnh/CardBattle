using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseContainer
{
    public bool Loaded { get; protected set; }
    public virtual IList getList()
    {
        return null;
    }

    public virtual IDictionary getMap()
    {
        return null;
    }

    public virtual void loadDataFromBin()
    {

    }
}

public class BaseBin
{

}

public class ConfigBean
{
    public static bool IsServer;
    /// <summary>
    ///T -----> Bean
    ///K -----> id type (int/string)
    /// </summary>
    public static T GetBean<T, K>(K id) where T : BaseBin
    {
        return GameDataManager.Instance.GetBean<T, K>(id);
    }

    /// <summary>
    /// Get Table Data By List
    /// </summary>
    public static List<T> GetBeanList<T>() where T : BaseBin
    {
        return GameDataManager.Instance.GetBeanList<T>();
    }

    /// <summary>
    /// Get Table Data By Map 
    /// </summary>
    public static Dictionary<K, T> GetBeanMap<T, K>() where T : BaseBin
    {
        return GameDataManager.Instance.GetBeanMap<T, K>();
    }
}
