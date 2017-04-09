using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// UI创建单位的消息
/// </summary>
public class UICreateUnitEvent : BaseEvent
{
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
    public UICreateUnitEvent(int id,Vector3 dest,MineMountain Mine, GameObject _subject=null, GameObject _object=null)
        : base(_subject, "UICreate", _object)
    {
        ID = id;
        destination = dest;
        mine = Mine;
    }


}

