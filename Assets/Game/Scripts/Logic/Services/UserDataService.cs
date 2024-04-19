using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserDataService          // upgrade later, optimize when data is saved to disk, optimize ram
{
    public static int CurLevel => GetData<int>(PREF_KEY.CurLevel, 1);

    private static Dictionary<string, object> _dataCache = new Dictionary<string, object>();

    public static T GetData<T>(string key, object defaultValue)
    {
        Type valueType = typeof(T);
        if (_dataCache.ContainsKey(key))
            return (T)_dataCache[key];
        else
        {
            return (T)LoadData(key, defaultValue, valueType);
        }
    }
    private static object LoadData(string key, object defaultValue, Type valueType)
    {
        object data = null;
        try
        {
            if (valueType == typeof(int))
            {
                data = PlayerPrefs.GetInt(key, (int)defaultValue);
            }
            else if (valueType == typeof(float))
            {
                data = PlayerPrefs.GetFloat(key, (float)defaultValue);
            }
            else if (valueType == typeof(string))
            {
                data = PlayerPrefs.GetString(key, (string)defaultValue);
            }
            else if (valueType == typeof(bool))
            {
                data = PlayerPrefsExtension.GetBool(key, (bool)defaultValue);
            }
            else
            {
                Debug.LogError("Value type is not defined");
            }
        }
        catch(Exception e)
        {
            Debug.LogError(e.ToString());
        }
        if (data != null)
        {
            _dataCache.Add(key, data);
        }
        return data;
    }
    public static void SetData(string key, object value)
    {
        if (_dataCache.ContainsKey(key))
        {
            _dataCache[key] = value;
        }
        SaveData(key, value);
    }
    private static void SaveData(string key, object value)
    {
        Type valueType = value.GetType();
        if (valueType == typeof(int))
        {
            PlayerPrefs.SetInt(key, (int)value);
        }
        else if (valueType == typeof(float))
        {
            PlayerPrefs.SetFloat(key, (float)value);
        }
        else if (valueType == typeof(string))
        {
            PlayerPrefs.SetString(key, (string)value);
        }
        else if (valueType == typeof(bool)) 
        { 
            PlayerPrefsExtension.SetBool(key, (bool)value);
        }
    }
}

public static class PREF_KEY
{
    public const string CurLevel = "CurLevel";
    public const string IsFirstPlay = "FirstPlay";

    public const string BackCount = "BackCount";
    public const string HelpCount = "HelpCount";
    public const string ResetCount = "ResetCount";

    public const string SoundOn = "SoundOn";
    public const string MusicOn = "MusicOn";
    public const string VibraOn = "VibraOn";

    public const string NoAds = "NoAds";

    public const string NextGuide = "NextGuide";

    public const string Version = "Version";
}