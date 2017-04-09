using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 通知UI，目前矿山的选择被取消
/// </summary>
public class cancelMountainEvent : BaseEvent
{
    //不需要额外信息
    public cancelMountainEvent(GameObject _subject, GameObject _object)
        : base(_subject, "CancelMountain", _object)
    {

    }


}

