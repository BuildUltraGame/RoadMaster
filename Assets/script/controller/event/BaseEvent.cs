using System;
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
        checkIsGameObj();
    }


    private void checkIsGameObj()
    {
        if(_object!=null&&_object.GetComponent<GameobjBase>()==null){
             throw new Exception("传入事件的并不是游戏对象");
        }
        if(_subject!=null&&_subject.GetComponent<GameobjBase>()==null){
             throw new Exception("传入事件的并不是游戏对象");
        }
    }

    public int getObjectID()
    {
        GameobjBase b;
        if (_object!=null&&(b = _object.GetComponent<GameobjBase>()) != null)
        {
            return b.game_ID;
        }
        else {
            return 0;
        }
    }

    public int getSubjectID()
    {
        GameobjBase b;
        if (_subject!=null&&(b = _subject.GetComponent<GameobjBase>()) != null)
        {
            return b.game_ID;
        }
        else
        {
            return 0;
        }
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

    public int getObjectOwner()
    {
        if (getObject() == null) { return GameobjBase.WORLD; }
        return getObject().GetComponent<GameobjBase>().getOwner();
    }

    public int getObjectOwner()
    {
        if (getSubject() == null) { return GameobjBase.WORLD; }
        return getSubject().GetComponent<GameobjBase>().getOwner();
    }
}
