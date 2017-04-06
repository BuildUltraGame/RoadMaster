using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 矿山被选择后会产生该事件
/// </summary>
public class MineSelectEvent : GameObjSeletEvent {

    public MineSelectEvent(GameObject _object)
        : base(_object)
    {
        if(_object.GetComponent<MineMountain>()==null){
            throw new Exception("传入的根本不是矿山对象");
        }
    }
    /// <summary>
    /// 直接获取矿山对象
    /// </summary>
    /// <returns>矿山对象</returns>
    public MineMountain getMine()
    {
        return getObject().GetComponent<MineMountain>();
    }

}
