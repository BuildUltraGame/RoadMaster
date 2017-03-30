using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有游戏事件的基础类
/// </summary>
public class BaseEvent
{
    private GameObject _subject;
    private string verb;
    private GameObject _object;

    /// <summary>
    /// 创建事件
    /// </summary>
    /// <param name="_subject">事件主体</param>
    /// <param name="verb">动作</param>
    /// <param name="_object">事件作用对象</param>
    public BaseEvent(GameObject _subject, string verb, GameObject _object)
    {
        this._subject = _subject;
        this._object = _object;
        this.verb = verb;

    }

    public GameObject getSubject()
    {
        return _subject;
    }

    public GameObject getObject()
    {
        return _object;
    }

    public string getVerb()
    {
        return verb;
    }


}
