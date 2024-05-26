using UnityEngine;

public static class GlobalConstant
{
    public static int GetInt(int id)
    {
        return ConfigLoader.GetRecord<GlobalConstantRecord>(id).intParam;
    }
    public static int GetInt(string name)
    {
        return ConfigLoader.GetRecord<GlobalConstantRecord>(name).intParam;
    }
    public static float GetFloat(int id)
    {
        return ConfigLoader.GetRecord<GlobalConstantRecord>(id).intParam / 100f;
    }
    public static float GetFloat(string name)
    {
        return ConfigLoader.GetRecord<GlobalConstantRecord>(name).intParam / 100f;
    }
    public static string GetString(int id)
    {
        return ConfigLoader.GetRecord<GlobalConstantRecord>(id).stringParam;
    }
    public static string GetString(string name)
    {
        return ConfigLoader.GetRecord<GlobalConstantRecord>(name).stringParam;
    }

    public static Sprite GetSprite(int id)
    {
        return ConfigLoader.GetRecord<GlobalConstantRecord>(id).sprite;
    }

    public static Sprite GetSprite(string name)
    {
        return ConfigLoader.GetRecord<GlobalConstantRecord>(name).sprite;
    }
}