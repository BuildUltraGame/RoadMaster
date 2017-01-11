using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 全局标记类,每个物体的tag表示了代表的单位
/// </summary>
public class Tags {

    public const string RAILWAY = "railway";
    public  const string RAILWAY_MOVABLE = "railway_movable";
    public const string ROAD = "road";
    public const string ROAD = "road";


    public static class Vehicle {
        public const string BASETRAMCAR = "base_tramcar";//基础矿车
        public const string OVERWEIGHTTRAMCAR = "overweight_ramcar";//超载矿车
        public const string SKILLEDTRAMCAR = "skilled_ramcar";//老练矿车
        public const string TRAIN = "train";//火车
        
    }

}
