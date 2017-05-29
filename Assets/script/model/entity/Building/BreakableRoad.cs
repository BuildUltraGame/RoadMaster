using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可修复的路脚本
/// </summary>
public class BreakableRoad : MonoBehaviour {

    public RoadPoint p1;
    public RoadPoint p2;

    private bool breakFlag = true;//路是否破坏标志位

    /// <summary>
    /// 修路接口
    /// </summary>
    public void repairRoad()
    {
        if (breakFlag)
        {
            p1.reachList.Add(p2);
            p2.reachList.Add(p1);
        }

    }


    /// <summary>
    /// 破坏路接口
    /// </summary>
    public void breakRoad()
    {
        if (!breakFlag)
        {
            p2.reachList.Remove(p1);
            p1.reachList.Remove(p2);
        }

    }

}
