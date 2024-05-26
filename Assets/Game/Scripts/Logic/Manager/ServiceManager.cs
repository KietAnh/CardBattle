using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ServiceManager : SingletonTemplate<ServiceManager>
{
    private Dictionary<Type, BaseService> _serviceMap = new Dictionary<Type, BaseService>();

    public void Init()
    {
        //_serviceMap.Add(typeof(PlayerService), new PlayerService());
        //_serviceMap.Add(typeof(MapObjectService), new MapObjectService());
        //_serviceMap.Add(typeof(StoreService), new StoreService());
        //_serviceMap.Add(typeof(QuestService), new QuestService());
        //_serviceMap.Add(typeof(BossService), new BossService());
        //_serviceMap.Add(typeof(StarChargeService), new StarChargeService());
        // add service here...

        //LoadAllData();
        PreloadData();
    }
    public void PreloadData()
    {
        //_serviceMap[typeof(PlayerService)].LoadData();
        //_serviceMap[typeof(StoreService)].LoadData();  // refactor: no preload
        //_serviceMap[typeof(QuestService)].LoadData();
        //_serviceMap[typeof(BossService)].LoadData();      // refactor: no preload
        //_serviceMap[typeof(StarChargeService)].LoadData(); // refactor: no preload
    }
    public T GetService<T>() where T : BaseService
    {
        Type type = typeof(T);
        if (_serviceMap.ContainsKey(type))
        {
            return _serviceMap[type] as T;
        }
        return null;
    }

    public void LoadAllData()
    {
        foreach (var pair in _serviceMap)
        {
            pair.Value.LoadData();
        }
    }

    public void SaveAllData()
    {
        foreach (var pair in _serviceMap)
        {
            pair.Value.SaveData();
        }
    }

    public void ReloadData()
    {
        foreach (var pair in _serviceMap)
        {
            pair.Value.ReloadData();
        }
    }
}
