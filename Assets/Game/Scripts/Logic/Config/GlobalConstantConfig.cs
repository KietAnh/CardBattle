using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalConstantConfig", menuName = "Config/GlobalConstantConfig", order = 1)]
public class GlobalConstantConfig : BaseConfig
{
    public List<GlobalConstantRecord> recordList;

    public Dictionary<int, GlobalConstantRecord> recordMap;
    public Dictionary<string, GlobalConstantRecord> recordMapByName;

    public override void CreateRecordMap()
    {
        recordMap = new Dictionary<int, GlobalConstantRecord>();
        recordMapByName = new Dictionary<string, GlobalConstantRecord>();
        foreach (var record in recordList)
        {
            if (!recordMap.ContainsKey(record.id))
            {
                recordMap.Add(record.id, record);
            }
            if (!recordMapByName.ContainsKey(record.name))
            {
                recordMapByName.Add(record.name, record);
            }
        }
    }
    public override BaseRecord GetRecordById(int id)
    {
        if (recordMap.ContainsKey(id))
            return recordMap[id];
        else
        {
            DevLog.Log("id is invalid: " + id);
            return null;
        }
    }

    public override BaseRecord GetRecordByName(string name)
    {
        if (recordMapByName.ContainsKey(name))
            return recordMapByName[name];
        else
        {
            DevLog.Log("name is invalid: " + name);
            return null;
        }
    }
}

[System.Serializable]
public class GlobalConstantRecord : BaseRecord
{
    public int intParam;
    public string stringParam;
    public Sprite sprite;
}
