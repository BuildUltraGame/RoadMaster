using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mineGold : MonoBehaviour {
    public Reminder remind;//用户提醒器
    public UILabel scoreNow;//当前分数
    public UILabel scoreNeed;//距胜利所需分数
    public UILabel GoldAll;//当前矿山金钱
    public UILabel Train;//当前矿山训练速度
    public UILabel GoldGet;//当前矿山单次金钱获得数
    public UILabel GoldTime;//当前矿山金钱获得间隔
    MineMountain mineSelected;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// 当前已获得的分数
    /// </summary>
    public void getScorenow()
    {
        scoreNow.text = "" + mineSelected.currentScore;
    }
    /// <summary>
    /// 仍需要金钱数
    /// </summary>
    public void getScoreneed()
    {
        scoreNeed.text = "";
    }
    /// <summary>
    /// 当前矿山金钱
    /// </summary>
    public void getThisMineGold()
    {
        if (mineSelected == null)
           remind.sendHint("please select a mine");
        else GoldAll.text = ""+mineSelected.totalMine;
    }
    /// <summary>
    /// 当前矿山训练速度
    /// </summary>
    public void getThisMineTrain()
    {
        Train.text = "";
    }
    /// <summary>
    /// 当前矿山单次金钱获得数
    /// </summary>
    public void getGoldAmount()
    {
        GoldGet.text = "" + mineSelected.increaseRate;
    }
    /// <summary>
    /// 当前矿山获得金钱的时间间隔
    /// </summary>
    public void getGoldTime()
    {
        GoldTime.text = "" + mineSelected.increaseFlashTime;
    }
}
