using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConfigManager : SingletonTemplate<ConfigManager>
{
    public byte[] GetData(string beanName)
    {
        return LoadBinData(beanName);
    }
    public byte[] LoadBinData(string beanName)
    {
        string abName = string.Format("conf_{0}", beanName.ToLower());
        TextAsset configFile = AssetLoadManager.LoadAsset<TextAsset>(abName, beanName);
        if (configFile != null)
        {
            return configFile.bytes;
        }
        return null;
    }
    public string GetTextData(string name)
    {
        return LoadTextData(name);
    }
    public string LoadTextData(string name)
    {
        string abName = string.Format("conf_{0}", name.ToLower());
        TextAsset configFile = AssetLoadManager.LoadAsset<TextAsset>(abName, name);
        if (configFile != null)
        {
            return configFile.text;
        }
        return null;
    }
}
