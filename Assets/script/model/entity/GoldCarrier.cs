using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 搬运金矿 模块
/// 提供搬运金矿的所有功能
/// 
/// </summary>
public class GoldCarrier : MonoBehaviour
{

    public int MaxGold = 1000;//最大运载量

    private int goldAmounts = 0;//运输黄金数
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 设置装的金矿总量
    /// 一般是生成矿车的时候调用
    /// </summary>
    /// <param name="amounts">要装载的金矿总量</param>
    /// <returns>剩余金矿数量(装满后剩余的量)</returns>
    public int setGoldAmounts(int amounts)
    {
        if (amounts > 0)
        {
            if(MaxGold<amounts){
                goldAmounts = MaxGold;
                return goldAmounts - MaxGold;
            }
            else {
                goldAmounts = amounts;
                return 0;            
            }
        }
        return 0;
    }

    /// <summary>
    /// 改变金矿数量
    /// 一般用于一些对装载矿车产生影响的单位去调用
    /// </summary>
    /// <param name="gold"></param>
    /// <returns>剩余金矿数量(装满后剩余的量)</returns>
    public int addGold(int gold)
    {
        if(goldAmounts+gold>MaxGold){
            int oldAmounts=goldAmounts;
            goldAmounts = MaxGold;
            return oldAmounts + gold - MaxGold;
        }else if(goldAmounts+gold<=0){
            goldAmounts = 0;
            return 0;
        }
        else
        {
            goldAmounts += gold;
            return 0;
        }
        
    }

    /// <summary>
    /// 卸金矿
    /// 重点负责调用
    /// </summary>
    /// <returns></returns>
    public int popGold()
    {
        int g = goldAmounts;
        goldAmounts = 0;
        return g;
    }



}
