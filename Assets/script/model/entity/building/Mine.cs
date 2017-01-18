using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
