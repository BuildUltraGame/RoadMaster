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
    /// </summary>
    /// <param name="v">搬道闸工人的起始点</param>
    /// <param name="linkNum">需要连接的铁路的序号,从起始点顺时针算第几个铁路</param>
   public abstract void GateChange(Vector3 v,int linkNum);


    public static int FromWhichPoint(Vector3[] vs,Vector3 v){
     

        float minDistance = 99;
        int minNum = 0;
        for (int i = 0; i < vs.Length; i++)
        {
            float tempDist = Vector3.Distance(v, vs[i]);
            if (tempDist < minDistance)
            {
                minDistance = tempDist;
                minNum = i;
            }
        }

        return minNum;



    }
}
