using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 通知UI，目前处于位置信息点击确认阶段
/// </summary>
public class waitClickEvent : BaseEvent
{
    //不需要额外信息
    public waitClickEvent(GameObject _subject, GameObject _object)
        : base(_subject, "WaitClick", _object)
    {

    }


}

