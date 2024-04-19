/**
 * Auto generated, do not edit it
 */
using Data.Containers;
using System;
using System.Collections;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;

public class GameDataManager
{
    public static readonly GameDataManager Instance = new GameDataManager();
    //public t_languageContainer t_languageContainer = new t_languageContainer();
    //public t_auditing_languageContainer t_auditing_languageContainer = new t_auditing_languageContainer();
    public t_global_constantContainer t_global_constantContainer = new t_global_constantContainer();
    public t_textContainer t_textContainer = new t_textContainer();
    public t_levelContainer t_levelContainer = new t_levelContainer();
    public t_level_guideContainer t_level_guideContainer = new t_level_guideContainer();
    public t_shop_packContainer t_shop_packContainer = new t_shop_packContainer();
    public t_itemContainer t_itemContainer = new t_itemContainer();
    public t_guideContainer t_guideContainer = new t_guideContainer();
    // add container here...
    private GameDataManager()
    {
        t_containerMap.Add(t_global_constantContainer.BinType, t_global_constantContainer);
        t_containerMap.Add(t_textContainer.BinType, t_textContainer);
        t_containerMap.Add(t_levelContainer.BinType, t_levelContainer);
        t_containerMap.Add(t_level_guideContainer.BinType, t_level_guideContainer);
        t_containerMap.Add(t_shop_packContainer.BinType, t_shop_packContainer);
        t_containerMap.Add(t_itemContainer.BinType, t_itemContainer);
        t_containerMap.Add(t_guideContainer.BinType, t_guideContainer);
        // add container here...
    }

    public void LoadAll(bool forceReload = false)
    {
        LoadBean(t_global_constantContainer.BinType, forceReload);
        LoadBean(t_textContainer.BinType, forceReload);
        LoadBean(t_levelContainer.BinType, forceReload);
        LoadBean(t_level_guideContainer.BinType, forceReload);
        LoadBean(t_shop_packContainer.BinType, forceReload);
        LoadBean(t_itemContainer.BinType, forceReload);
        LoadBean(t_guideContainer.BinType, forceReload);
        // load bean here...
    }

    private Dictionary<Type, BaseContainer> t_containerMap = new Dictionary<Type, BaseContainer>();

    public T GetBean<T, K>(K key) where T : BaseBin
    {
        Type t = typeof(T);
        LoadBean(t);
        if (t_containerMap.ContainsKey(t))
        {
            var t_container = t_containerMap[t];
            Dictionary<K, T> map = t_container.getMap() as Dictionary<K, T>;
            if (map != null && map.ContainsKey(key))
                return map[key];
        }
        return null;
    }

    public List<T> GetBeanList<T>() where T : BaseBin
    {
        Type t = typeof(T);
        LoadBean(t);
        if (t_containerMap.ContainsKey(t))
        {
            var t_container = t_containerMap[t];
            List<T> list = t_container.getList() as List<T>;
            if (list != null)
                return list;
            Debug.LogError("can not find Bean > " + t.Name);
        }
        Debug.LogError("can not find Bean > " + t.Name);
        return null;
    }

    public Dictionary<K, T> GetBeanMap<T, K>() where T : BaseBin
    {
        Type t = typeof(T);
        LoadBean(t);
        if (t_containerMap.ContainsKey(t))
        {
            var t_container = t_containerMap[t];
            Dictionary<K, T> map = t_container.getMap() as Dictionary<K, T>;
            if (map != null)
                return map;
        }
        return null;
    }

    public void LoadBean<T>(bool forceReload = false) where T : BaseBin
    {
        Type t = typeof(T);
        //if (!ConfigBean.IsServer)
        //    setCode();
        LoadBean(t, forceReload);
    }

    //private void setCode()
    //{
    //    if (GameManager.GetMainFlag() < 14)
    //    {
    //        PathUtil.codeOffset = 13;
    //        PathUtil.codeKey = "GameDataManager.Bean";
    //    }
    //}

    public void LoadBean(Type t, bool forceReload = false)
    {
        if (t_containerMap.ContainsKey(t))
        {
            if (!t_containerMap[t].Loaded || forceReload)
            {
                t_containerMap[t].loadDataFromBin();
            }
        }
    }
}

