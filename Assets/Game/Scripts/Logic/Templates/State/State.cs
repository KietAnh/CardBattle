/*
 * file State.cs
 *
 * author: Pengmian
 * date:   2014/10/9
 * 
 * Edit by liqiang 
 * date:   2015/12/17
 */

/// <summary>
/// 状态
/// </summary>
using System;
using System.Collections;

public abstract class State
{
    protected object _owner;

    /// <summary>
    /// 状态拥有者
    /// </summary>
    /// <param name="owner"></param>
    public virtual void SetOwner(object owner)
    {
        _owner = owner;
    }

    /// <summary>
    /// 返回状态拥有者
    /// </summary>
    /// <returns></returns>
    public object GetOwner()
    {
        return _owner;
    }

    public abstract void OnReEnter(object obj = null);

    /// <summary>
    /// 进入状态
    /// </summary>
    /// <param name="owner"></param>
    public abstract void OnEnter(object obj = null);

    /// <summary>
    /// 状态更新
    /// </summary>
    /// <param name="owner"></param>
    public abstract void OnUpdate();

    /// <summary>
    /// 状态结束
    /// </summary>
    /// <param name="owner"></param>
    public abstract void OnLeave(string stateKey);

    /// <summary>
    /// 返回状态ID
    /// </summary>
    public abstract string GetStateKey();

}

