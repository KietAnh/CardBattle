using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopService : BaseService
{
    
    public override void LoadData()
    {
        
    }


    public override void SaveData()
    {
        
    }
    public override void ClearCache()
    {

    }
    public override void ReloadData()
    {
        ClearCache();
        LoadData();
    }
}
