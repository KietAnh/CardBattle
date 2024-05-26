using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProbConfig", menuName = "Config/ProbConfig", order = 1)]
public class ProbConfig : BaseConfig
{
    public List<ProbRecord> recordList;

    public Dictionary<int, ProbRecord> recordMap;
    public Dictionary<string, ProbRecord> recordMapByName;

    public override void CreateRecordMap()
    {
        recordMap = new Dictionary<int, ProbRecord>();
        recordMapByName = new Dictionary<string, ProbRecord>();
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
public class ProbRecord : BaseRecord
{
    public List<float> probs;
}
