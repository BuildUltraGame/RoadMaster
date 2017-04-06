using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectEvent : BaseEvent {
    private Type type;
    private List<Vector3> listV;
    private List<GameObject> listObj;
    public SelectEvent(GameObject _obj)
        : base(null, "Select", _obj)
    {
        
    }

    private void setType(Type t) {
        type = t;
    }

    public void setSeletList(List<Vector3> listV)
    {
        setType(typeof(Vector3));
        this.listV = listV;
    }

    public void setSeletList(List<GameObject> listObj)
    {
        setType(typeof(GameObject));
        this.listObj = listObj;
    }

}
