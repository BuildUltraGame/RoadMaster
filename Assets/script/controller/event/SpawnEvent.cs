
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
        Roadmovable roadABble;
        if ((roadABble = getObject().GetComponent<Roadmovable>())!=null)
        {
           return roadABble.getDistance();
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
        Roadmovable roadABble;
        if ((roadABble = getObject().GetComponent<Roadmovable>()) != null)
        {
            return roadABble.speed;
        }
        else
        {
            return -1;
        }
    }


    /// <summary>
    /// 获得生成单位的脚步(必须先判断是否是人)
    /// </summary>
    ///  <seealso cref="hasNavData"/>
    /// <returns>速度</returns>
    public float getStep()
    {
        Roadmovable roadABble;
        if ((roadABble = getObject().GetComponent<Roadmovable>()) != null)
        {
            return roadABble.step;
        }
        else
        {
            return -1;
        }
    }


}


