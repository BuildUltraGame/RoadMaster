﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 流氓脚本
/// </summary>
public class RogueAttack : AttackAbs
{
    public const int ATTACKLEVEL = 2;


    public override void Attack(GuardAbs guardObj)
    {
        if(guardObj.getGuardLevel()<=getAttackLevel()&&guardObj.gameObject.layer==Layers.VEHICLE){
            if (guardObj.DestrotyGameObj(this))
            {
                TryDestroy(this);
            }
        }

    }

    public override int getAttackLevel()
    {
        return ATTACKLEVEL;
    }

    public override bool TryDestroy(AttackAbs attackObj)
    {
       if(attackObj==this){
            gameObject.SendMessage(GameobjBase.TryDestroyFUNC);
            //Destroy(gameObject);
       }

       return true;
    }
}
