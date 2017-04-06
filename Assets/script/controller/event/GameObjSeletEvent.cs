using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏物体被选择事件
/// </summary>
public class GameObjSeletEvent:BaseEvent
{
    public GameObjSeletEvent(GameObject _object)
        : base(null,"Select",_object)
    {

    }

    

}

