using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道口基类
/// </summary>
public abstract class MetroGate : Railway {

    /// <summary>
    /// 变换道口连接情况
    /// 
    /// 经过讨论后不需要用户操作,分情况处理:
    /// 当是三岔道口的时候:
    ///                     1.如果工人所在点已经和其他接通,那就只有一种情况,直接转换另一个接口到未连接接口
    ///                     2.如果工人所在点没有联通,则随机将所在点和其他两个点联通
    /// 当四岔道口的时候:
    ///                 只有两种情况,直接转换就可以了
    ///                 
    /// 所以用户并不能显式决定所连接的铁路,于是第二个参数暂时无效
    /// 
    /// </summary>
    /// <param name="v">搬道闸工人的起始点</param>
    /// <param name="linkNum">(暂时无效)需要连接的铁路的序号,从起始点顺时针算第几个铁路</param>
   public abstract void GateChange(Vector3 v,int linkNum=1);



   /// <summary>
   /// 摧毁本岔路的车
   /// </summary>
   protected void destroyVehilesOnRoad()
   {
       foreach (GameObject obj in vehilesOnRoad)
       {
           Destroy(obj);
       }
   }

}
