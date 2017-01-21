using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 矿山类,未完成
/// 
/// 因为每个矿山独自生产兵种,其实矿山是个工程量很大的部分
/// 
/// 首先需要整合所有的生成器,然后向外给控制器一个接口,并且生成矿车的时候又要给个交互,给矿车放钱
/// 
/// 
/// BY:峰少
/// </summary>
public class Mine : CollisionBaseHandler {

    private int gold = 0;

    private int addGoldPerTime = 100;
    private float addGoldInterval = 2;
    private TimerController.Timer addGoldTimer;

	// Use this for initialization
	void Start () {
        setAddGoldInfo(addGoldPerTime,addGoldInterval);
    }


    public void setAddGoldInfo(int addGoldPerTime, float interval)
    {
        this.addGoldPerTime = addGoldPerTime;
        this.addGoldInterval = interval;
        addGoldTimer = TimerController.getInstance().NewTimer(addGoldInterval, true);
        addGoldTimer.runFun = addGold;
    }
	
	// Update is called once per frame
	void Update () {
       
	}

    private void addGold()
    {
        gold += addGoldPerTime; 
    }

   


}
