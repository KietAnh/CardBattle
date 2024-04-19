using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponent
{
    public static string UI_COMPONENT_PREFIX = "UI_";

    public Transform trans;

    public virtual void Init()
    {
        //Debug.Log("Init");
    }
}
