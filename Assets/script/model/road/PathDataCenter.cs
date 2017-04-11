using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;

/// <summary>
/// 路径点数据中心
/// </summary>
public class PathDataCenter : MonoBehaviour{

    public static List<Vector3> pathPoints =  new List<Vector3>();



    public static void registerPathPoint(Vector3 v)
    {
        v.y = 0;
        pathPoints.Add(v);
    }


    public static void unRegisterPathPoint(Vector3 v)
    {
        v.y = 0;
        pathPoints.Remove(v);
    }
}
