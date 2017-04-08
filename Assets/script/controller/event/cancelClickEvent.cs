using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 通知UI，结束处于位置信息点击确认阶段
/// </summary>
public class cancelClickEvent : BaseEvent
{
    //不需要额外信息
    public cancelClickEvent(GameObject _subject, GameObject _object)
        : base(_subject, "CancelClick", _object)
    {

    }


}

