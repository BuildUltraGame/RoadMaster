﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;

public class mineGold : MonoBehaviour, IListener<MineSelectEvent>, IListener<cancelMountainEvent>
{
    public UILabel GoldAll;//当前矿山金钱总量
    public UILabel Train;//当前矿山训练速度
    public UILabel GoldGet;//当前矿山单次金钱获得数
    public UILabel GoldTime;//当前矿山金钱获得间隔
    MineMountain mineSelected;
	// Use this for initialization
	void Start () {
        UnityEventCenter.Register<MineSelectEvent>(this);
        UnityEventCenter.Register<cancelMountainEvent> (this);
	}
	
	// Update is called once per frame
	void Update () {
        if(mineSelected!=null)
        {
            GoldAll.text = "" + mineSelected.totalMine;
            //Train.text = ""+mineSelected.;训练速度数据，未获得
            GoldGet.text = "+" + mineSelected.increaseRate;
            //GoldTime.text = "" + mineSelected.increaseFlashTime;
        }
        
	}
    /// <summary>
    /// 获取当前矿山后改变底部面板信息
    /// </summary>
    /// <param name="message"></param>
    public void Handle(MineSelectEvent message)
    {
        MineMountain temp = message.getMine();
        if (temp.gameObject.GetComponent<GameobjBase>().getOwner() == GameobjBase.PLAYER)
        {
            mineSelected = temp;

        }
        else
        {
            mineSelected = null;
            GoldAll.text = "";
            GoldGet.text = "";
        }

        
    }

    /// <summary>
    /// 清空底部面板信息
    /// </summary>
    /// <param name="message"></param>
    public void Handle(cancelMountainEvent message)
    {
        mineSelected = null;
        GoldAll.text ="";
      //  Train.text ="";
        GoldGet.text ="";
      //  GoldTime.text ="";
    }
    void OnDisable()
    {
        UnityEventCenter.UnRegister<MineSelectEvent>(this);
        UnityEventCenter.UnRegister<cancelMountainEvent>(this);
    }
}
