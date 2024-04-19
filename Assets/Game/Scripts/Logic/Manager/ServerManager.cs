using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ServerManager : SingletonTemplate<ServerManager>
{
    //private Dictionary<Type, FakeServer> _serverMap = new Dictionary<Type, FakeServer>();

    //public void Init()
    //{
    //    _serverMap.Add(typeof(SettingServer), new SettingServer().Bind());
    //    _serverMap.Add(typeof(ShopServer), new ShopServer().Bind());
    //    // add server here...
    //}
    //public FakeServer GetServer<T>() where T : FakeServer
    //{
    //    Type type = typeof(T);
    //    if (_serverMap.ContainsKey(type))
    //    {
    //        return _serverMap[type];
    //    }
    //    return null;
    //}

    //public void UnbindAll()
    //{
    //    foreach (var pair in _serverMap)
    //    {
    //        pair.Value.Unbind();
    //    }
    //}
}
