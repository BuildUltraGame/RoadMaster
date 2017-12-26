using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setVehicleEvent : BaseEvent
{
    //id为车辆单位id
    public int id;
    public GameObject obj;//待设置的格子
    public string name;//图集名字
    public setVehicleEvent(GameObject _subject, GameObject _object,int id,string name)
        : base(_subject, "setVehicle", _object)
    {
        this.id = id;
        obj = _object;
        this.name = name;
    }


}
