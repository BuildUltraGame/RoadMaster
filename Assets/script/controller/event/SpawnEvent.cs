
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// 单位生成的时候会发生该事件
/// </summary>
public class SpawnEvent : BaseEvent
{

    public SpawnEvent(GameObject spawner,GameObject gameObj)
        : base(spawner, "Spawn", gameObj)
    {
        
    }

    /// <summary>
    /// 判断生成单位是否具有导航功能(即是否是人)
    /// </summary>
    /// <returns></returns>
    public bool hasNavData()
    {
        return getObject().GetComponent<NavMeshAgent>() != null;
    }

    /// <summary>
    /// 判断生成单位距离目的地有多远(必须先判断是否是人)
    /// </summary>
    /// <seealso cref="hasNavData"/>
    /// <returns>距离</returns>
    public float getDistance()
    {
        NavMeshAgent nav;
        if ((nav = getObject().GetComponent<NavMeshAgent>())!=null)
        {
           return nav.remainingDistance;
        }else{
            return -1;
        }
    }


    /// <summary>
    /// 获得生成单位的速度(必须先判断是否是人)
    /// </summary>
    ///  <seealso cref="hasNavData"/>
    /// <returns>速度</returns>
    public float getSpeed()
    {
        NavMeshAgent nav;
        if ((nav = getObject().GetComponent<NavMeshAgent>()) != null)
        {
            return nav.speed;
        }
        else
        {
            return -1;
        }
    }


}


