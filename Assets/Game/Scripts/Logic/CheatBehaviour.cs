using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatBehaviour : MonoBehaviour
{
    //private Button _btnCheat;
    //private int _numClick = 0;
    //private float _lastTimeClick = 0;
    //private const int MAX_NUM_CLICK = 7;
    //private void Awake()
    //{
    //    _btnCheat = transform.Find("btn").GetComponent<Button>();
    //    _btnCheat.onClick.AddListener(OnClickCheat);
    //}

    //private void OnDestroy()
    //{
    //    _btnCheat.onClick.RemoveListener(OnClickCheat);
    //}

    //private void Update()
    //{
    //    if (_numClick > 0)
    //    {
    //        if (Time.time - _lastTimeClick > 0.5f)
    //        {
    //            _numClick = 0;
    //        }
    //    }
    //}

    //private void OnClickCheat()
    //{
    //    _numClick += 1;
    //    _lastTimeClick = Time.time;
    //    if (_numClick >= MAX_NUM_CLICK)
    //    {
    //        WindowManager.Singleton.OpenWindow<CheatWindow>();
    //        _numClick = 0;
    //    }
    //}
}
