using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// UI创建单位的消息
/// </summary>
public class CreateUnitEvent : BaseEvent
{
    public int player=GameobjBase.PLAYER;
    public int ID;//单位ID
    public Vector3 destination;//目的地
    public MineMountain mine;//创建单位的矿山
    /// <summary>
    /// UI创建单位的消息
    /// </summary>
    /// <param name="id">单位ID</param>
    /// <param name="dest">单位目标三维坐标</param>
    /// <param name="Mine">生产单位的矿山</param>
    /// <param name="_subject"></param>
    /// <param name="_object"></param>
    public CreateUnitEvent(int id,Vector3 dest,int player=GameobjBase.PLAYER, MineMountain Mine = null)
        : base(null, "Create", null)
    {
        ID = id;
        destination = dest;
        mine = Mine;
        this.player = player;
    }



}

