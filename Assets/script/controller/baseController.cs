using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;
//基础的游戏进程控制器
public class baseController : MonoBehaviour,IListener<MineStateChangeEvent> ,IListener<GameObjSeletEvent>{

    //int[] mineList;//矿山列表
    MineMountain mineSelected;//当前选中的矿山，未选中则为null

    /// <summary>
    /// 获取矿山的引用
    /// </summary>
    /// <returns></returns>
    MineMountain getMine()
    {
        return mineSelected;
    }
    
    /// <summary>
    /// 
    /// </summary>
    void loadMap()
    {

    }

    public void onMineSelected(MineMountain selected)
    {
        mineSelected = selected;
    }

    void createMenu()
    {

    }

    void onMenuClick()//参数未定
    {

    }

    void insertUnit()
    {

    }

    void insertAbility()
    {

    }

    void insertMenu()
    {

    }

    /// <summary>
    /// 升级对应矿山的科技
    /// </summary>
    /// <param name="name">科技的名字</param>
    public void Upgrade(string name)
    {
        //mineSelected.upGrade(name);
    }

    /// <summary>
    /// 训练无接口的单位的接口函数
    /// </summary>
    /// <param name="name">单位的名称</param>
    public void createUnitNoDirection(int id)
    {
        Vector3 v = new Vector3(0,0,0);
        mineSelected.buildUnitByID(id,v);
    }
    
    
    /// <summary>
    /// 训练有目的地的单位的接口函数
    /// </summary>
    /// <param name="name">建造单位名称</param>
    /// <param name="buildPos">单位指定的位置</param>

    public void createUnitWithDirection(string name,Vector3 buildPos)
    {
        mineSelected.buildUnitByName(name, buildPos);
    }

    void useAbility()
    {

    }

    public virtual void isWin()
    {


    }
    //

    // Use this for initialization
	void Start () {
        UnityEventAggregator.EventAggregator.Register<GameObjSeletEvent>(this);
        UnityEventAggregator.EventAggregator.Register<MineStateChangeEvent>(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// 矿山选择接口
    /// </summary>
    /// <param name="message"></param>
    public void Handle(MineStateChangeEvent message)
    {
        MineMountain temp =message.getMine();
        if (temp != null)
            mineSelected = temp;
    }
    /// <summary>
    /// todo :单位点击接口
    /// </summary>
    /// <param name="message"></param>
    public void Handle(GameObjSeletEvent message)
    {
        int id=message.getObject().GetComponent<>();
        createUnitNoDirection(id);
    }
}
