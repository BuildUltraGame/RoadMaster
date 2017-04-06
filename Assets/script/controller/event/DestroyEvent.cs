using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 破坏事件,暂时并没有添加什么额外的信息,该类主要是为了区分出事件
/// </summary>
public class DestroyEvent:BaseEvent
{

    public DestroyEvent(GameObject _subject, GameObject _object)
        : base(_subject, "Destory", _object)
    {
        
    }


}

