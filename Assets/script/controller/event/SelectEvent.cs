using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 当选择请求已经完成的时候会发送这个事件,表示用户已经选择
/// </summary>
public class SelectEvent : BaseEvent {
    private Type type;
    private List<Vector3> listV;
    private List<GameObject> listObj;
    public SelectEvent(GameObject _obj)
        : base(null, "Select", _obj)
    {
        listV = new List<Vector3>();
        listObj = new List<GameObject>();
    }

    private void setType(Type t) {
        type = t;
    }

    public Type getSelectType()
    {
        return type;
    }

    public void addSelect(Vector3 v)
    {
        setType(typeof(Vector3));
        listV.Add(v);
    }

    public void addSelect(GameObject obj)
    {
        setType(typeof(GameObject));
        listObj.Add(obj);
    }


    public List<Vector3> getVectorList()
    {
        return listV;
    }


    public List<GameObject> getTargetList()
    {
        return listObj;
    }

}
