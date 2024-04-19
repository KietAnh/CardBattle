using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 状态机
/// </summary>
/// <typeparam name="T">状态机拥有者类型</typeparam>

public class StateMachine
{

    public const string InvalidState = "Invalid";

    protected Dictionary<string, State> _stateCache;
    protected State _currentState = null;
    protected State _lastState = null;
    protected State _globalState = null;
    protected object _owner;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="owner"></param>
    public StateMachine(object owner)
    {
        _stateCache = new Dictionary<string, State>();
        _owner = owner;
    }

    public bool IsExist(string stateKey)
    {
        return _stateCache.ContainsKey(stateKey);
    }

    public State GetStateByKey(string stateKey)
    {
        State state = null;
        _stateCache.TryGetValue(stateKey, out state);
        return state;
    }


    /// <summary>
    /// 设置拥有者
    /// </summary>
    /// <param name="owner"></param>
    public void setOwner(object owner)
    {
        _owner = owner;
    }

    /// <summary>
    /// 注册状态
    /// </summary>
    /// <param name="type">状态类型</param>
    /// <param name="state">状态</param>
    public void RegisterState(string key, State state)
    {
        State exists = null;
        _stateCache.TryGetValue(key, out exists);
        if (exists == null)
        {
            _stateCache.Add(key, state);
        }
        else
        {
            _stateCache[key] = state;
        }
    }


    public void SetGlobalState(State state, object obj = null)
    {
        _globalState = state;
        _globalState.SetOwner(_owner);
        _globalState.OnEnter(obj);
    }


    /// <summary>
    /// 移除状态
    /// </summary>
    /// <param name="type"></param>
    public void RemoveState(int id)
    {
        if (_stateCache.ContainsKey(id.ToString()))
            _stateCache.Remove(id.ToString());
    }

    /// <summary>
    /// 改变状态
    /// </summary>
    /// <param name="type"></param>
    public virtual void ChangeState(string key, object obj = null)
    {
        State newState = null;
        _stateCache.TryGetValue(key, out newState);
        if (newState == null)
        {
            Debug.LogError("unregister state type: " + key);
            return;
        }

        if (_currentState != null)
        {
            _currentState.OnLeave(newState.GetStateKey());
        }

        _lastState = _currentState;
        _currentState = newState;
        _currentState.SetOwner(_owner);
        _currentState.OnEnter(obj);
    }

    /// <summary>
    /// 更新
    /// </summary>
    public virtual void Update()
    {
        if (_globalState != null)
            _globalState.OnUpdate();
        if (_currentState != null)
            _currentState.OnUpdate();
    }

    /// <summary>
    /// 当前状态类型 
    /// </summary>
    /// <returns></returns>
    public string GetCurrentState()
    {
        if (_currentState != null)
        {
            return _currentState.GetStateKey();
        }
        return StateMachine.InvalidState;
    }

    /// <summary>
    /// 释放
    /// </summary>
    public void Clear()
    {
        if (_currentState != null)
            _currentState.OnLeave(StateMachine.InvalidState);
        if (_stateCache != null)
            _stateCache.Clear();
        _currentState = null;
        _lastState = null;
    }

}