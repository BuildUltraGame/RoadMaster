using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 层
/// 
/// 直接通过层来判断碰撞和物体的大类
/// 
/// 每个物体的层决定了他的大类,目前有4个大类:路,人,车,建筑
/// </summary>
public class Layers 
{

    public const int ROAD = 8;//人行道层
    public const int RAILWAY = 12;//铁路层
    public const int CHARACTER =9;//工人层
    public const int VEHICLE = 10;//矿车层
    public const int BUILDING = 11;//建筑层
    

}
