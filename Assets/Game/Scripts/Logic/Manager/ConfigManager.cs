using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class ConfigManager : SingletonTemplate<ConfigManager>
{
    //public byte[] GetData(string beanName)
    //{
    //    return LoadBinData(beanName);
    //}
    //public byte[] LoadBinData(string beanName)
    //{
    //    string abName = string.Format("conf_{0}", beanName.ToLower());
    //    TextAsset configFile = AssetLoadManager.LoadAsset<TextAsset>(abName, beanName);
    //    if (configFile != null)
    //    {
    //        return configFile.bytes;
    //    }
    //    return null;
    //}
    //public string GetTextData(string name)
    //{
    //    return LoadTextData(name);
    //}
    //public string LoadTextData(string name)
    //{
    //    string abName = string.Format("conf_{0}", name.ToLower());
    //    TextAsset configFile = AssetLoadManager.LoadAsset<TextAsset>(abName, name);
    //    if (configFile != null)
    //    {
    //        return configFile.text;
    //    }
    //    return null;
    //}
    private Dictionary<Type, BaseConfig> _configMap = new Dictionary<Type, BaseConfig>();
    public void Init()
    {
        List<string> configNames = new List<string>() { "PetConfig", "FoodConfig", "ProbConfig", "GlobalConstantConfig" }; // hardcode
        foreach (var configName in configNames)
        {
            var config = AssetLoadManager.LoadAsset<BaseConfig>(("conf_" + configName).ToLower(), configName);
            string configTypeStr = config.GetType().ToString();
            string recordTypeStr = configTypeStr.Remove(configTypeStr.IndexOf("Config")) + "Record";
            Type recordType = Assembly.GetExecutingAssembly().GetType(recordTypeStr);
            config.CreateRecordMap();
            _configMap.Add(recordType, config);
        }
    }

    public BaseConfig GetConfigAsset<T>() where T : BaseRecord
    {
        if (_configMap.ContainsKey(typeof(T)))
        {
            return _configMap[typeof(T)];
        }
        else
        {
            DevLog.Err("asset have not loaded yet + " + typeof(T));
            return null;
        }
    }

    public T GetConfigRecord<T>(int id) where T : BaseRecord
    {
        if (_configMap.ContainsKey(typeof(T)))
        {
            return _configMap[typeof(T)].GetRecordById(id) as T;
        }
        else
        {
            return null;
        }
    }

    public T GetConfigRecord<T>(string name) where T : BaseRecord
    {
        if (_configMap.ContainsKey(typeof(T)))
        {
            return _configMap[typeof(T)].GetRecordByName(name) as T;
        }
        else
        {
            DevLog.Err("config is invalid >> " + typeof(T));
            return null;
        }
    }
}

public static class ConfigLoader
{
    public static T GetRecord<T>(int id) where T : BaseRecord
    {
        return ConfigManager.Singleton.GetConfigRecord<T>(id);
    }

    public static T GetRecord<T>(string name) where T : BaseRecord
    {
        return ConfigManager.Singleton.GetConfigRecord<T>(name);
    }

    public static BaseConfig GetConfig<T>() where T : BaseRecord
    {
        return ConfigManager.Singleton.GetConfigAsset<T>();
    }
}
