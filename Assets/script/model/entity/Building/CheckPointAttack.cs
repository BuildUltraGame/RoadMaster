﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 检查站
/// 
/// 对经过了车辆进行扣钱或者破坏
/// 防御等级一样的单位会破坏,如果高于检查站的攻击等级,则被扣钱
/// </summary>
public class CheckPointAttack : AttackAbs
{
    public float tax = 0.5f;//税率
    public float desTime = 8f;

    public const int ATTACKLEVEL = 1;


    public override void Attack(GuardAbs guardObj)
    {
        GoldCarrier gc = guardObj.gameObject.GetComponent<GoldCarrier>();
        if (gc==null)
        {
            return;
        }
        
            if (guardObj.getGuardLevel() <= getAttackLevel())
            {

                //等级相同,摧毁对方(只有超载矿车)
                if (guardObj.DestrotyGameObj(this))
                {//成功摧毁对方
                    TryDestroy(this);
                }

                
            }
            else
            {
                //等级高于检查站,则扣钱
                int gold=gc.popGold();
                gc.setGoldAmounts(Mathf.FloorToInt(gold*tax));

            }
        

    }

    public override bool TryDestroy(AttackAbs attackObj)
    {
        if (attackObj == this)
        {
            gameObject.SendMessage(GameobjBase.TryDestroyFUNC);
            //Destroy(gameObject);
        }

        return true;
    }

    public override int getAttackLevel()
    {
        return ATTACKLEVEL;
    }

    void Start()
    {
        gameObject.SendMessage(GameobjBase.TryDestroyDelayFUNC,desTime);
        //Destroy(gameObject, desTime);
    }

}
