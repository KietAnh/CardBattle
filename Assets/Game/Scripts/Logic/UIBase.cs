using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIBase
{
    public Transform trans { get; private set; }
    public UIComponent view { get; private set; }   
    public WinInfo info { private set; get; }
    public string winName { get; protected set; }
    public UILayer layer { get; private set; }
    protected T GetUIWindow<T>() where T : UIComponent
    {
        return view as T;
    }
    /// <summary>
    /// Call when window is created
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="winName"></param>
    /// <param name="view"></param>
    /// <param name="winInfo"></param>
    /// <param name="winLayer"></param>
    public void Create(Transform trans, string winName, UIComponent view, WinInfo winInfo, UILayer winLayer)
    {
        this.trans = trans;
        this.winName = winName;
        this.view = view;
        this.info = winInfo;
        this.layer = winLayer;
    }
    /// <summary>
    /// call when window is created
    /// </summary>
    public virtual void OnOpen()
    {
        AddEventListener();
    }
    protected virtual void AddEventListener()
    {
        AddButtonOnClickListener();
    }
    protected virtual void AddButtonOnClickListener()
    {

    }

    protected virtual void RemoveEventListener()
    {
        RemoveButtonOnClickListener();
    }
    protected virtual void RemoveButtonOnClickListener()
    {

    }
    
    /// <summary>
    /// Call when func OnOpen() was called
    /// </summary>
    protected virtual void InitView()
    {

    }
    /// <summary>
    /// Call to refresh view
    /// </summary>
    public virtual void RefreshView()
    {

    }
    public virtual void OnUpdate()
    {

    }
    /// <summary>
    /// Call to close window
    /// </summary>
    //public void Close()
    //{
    //    GCManager.Singleton.Destroy(trans.gameObject);

    //    OnClose();
    //}
    /// <summary>
    /// Call when window was closed
    /// </summary>
    public virtual void OnClose()
    {
        GameObject.Destroy(trans.gameObject);
        RemoveEventListener();
    }

    /// <summary>
    /// call when window change from inactive to active
    /// </summary>
    public virtual void OnShow()
    {

    }
    /// <summary>
    /// call when window change from active to inactive
    /// </summary>
    public virtual void OnHide(Action callback)
    {
        if (callback != null)
        {
            callback();
        }
    }
    
    //
    public virtual void BlockInput(bool active = true)
    {
        var blockInput = trans.Find("block_input_front");
        blockInput.gameObject.SetActive(active);
    }
}
