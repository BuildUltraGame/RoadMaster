using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可修复的路脚本
/// </summary>
public class BreakableRoad : MonoBehaviour {

    public RoadPoint p1;
    public RoadPoint p2;

    private float repairTime = 1f;
    private float breakTime = 1f;


    public Animator animator;


    private bool breakFlag = true;//路是否破坏标志位

    /// <summary>
    /// 修路接口,修理需要时间 repairTime
    /// </summary>
    public void repairRoad()
    {
      
        if (breakFlag)
        {
            playRepairRoad();
            Invoke("repairRoadNow", repairTime);
        }
    }

    private void repairRoadNow()
    {
        p1.reachList.Add(p2);
        p2.reachList.Add(p1);
    }


    /// <summary>
    /// 破坏路接口
    /// </summary>
    public void breakRoad()
    {
       
        if (!breakFlag)
        {
            playBreakRoad();
            Invoke("breakRoadNow", breakTime);
        }

    }
    
    private void breakRoadNow()
    {
        p2.reachList.Remove(p1);
        p1.reachList.Remove(p2);
    }

    /// <summary>
    /// 播放修路动画
    /// </summary>
    private void playRepairRoad()
    {
        animator.Play("build");
    }

    /// <summary>
    /// 播放 破坏路动画
    /// </summary>
    private void playBreakRoad()
    {

    }

}
