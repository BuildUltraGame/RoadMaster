﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 超载矿车
/// 
/// 目前没啥特殊功能代码,毕竟只是运矿
/// </summary>
public class OverweightTramcarGuard : GuardAbs
{
    public const int GUARDLEVEL = 1;
    public override bool TryDestroy(AttackAbs attackObj)
    {

        if(attackObj.getAttackLevel()>getGuardLevel()){
            Destroy(gameObject);
            return true;
        }else{
            return false;
        }

    }

    public override int getGuardLevel()
    {
        return GUARDLEVEL;
    }
}