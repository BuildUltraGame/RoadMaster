using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 全局标记类,每个物体的tag表示了代表的单位
/// </summary>
public class Tags {

    public const string RAILWAY = "railway";
  
    public const string ROAD = "road";

    public const string RAILWAY_POINT = "railway_point";

    public const string GATE = "gate";

    public static class Vehicle {
        public const string BASETRAMCAR = "base_tramcar";//基础矿车
        public const string OVERWEIGHTTRAMCAR = "overweight_ramcar";//超载矿车
        public const string SKILLEDTRAMCAR = "skilled_ramcar";//老练矿车
        public const string TRAIN = "train";//火车

        public const string EXPLORATIONTRAMCAR = "exploration_tramcar";//探勘车,占领矿山用
        
    }



    public static class Character
    {
        public const string GATEWORKER = "gate_worker";//搬道闸工人
        public const string ROADMAKER = "road_maker";//修路工人
        public const string ELITEGATEWORKER = "elite_gate_worker";//精英搬道闸工人
        public const string INSPECTOR = "inspector";//督察员
        public const string ROGUE = "rogue";//流氓
        public const string RAILWAYDESTROIER = "railway_destroier";//道路破坏者
        public const string GUNNER = "gunner";//枪手
    }

    public static class Building
    {
        public const string MINE = "mine";//矿山
        public const string MAINMINE = "main_mine";//主矿山
        public const string SCORING = "scoring_building";//终点
        public const string FUCKHOLE = "fuck_hole";
        public const string BREAKABLEROAD = "breakable_road";

    }

    public const string CAR_SELECTOR = "car_selector";
}
