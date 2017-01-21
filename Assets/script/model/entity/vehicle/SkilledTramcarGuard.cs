using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 老练矿车
/// 
/// 
/// </summary>
public class SkilledTramcarGuard : GuardAbs
{
    public const int GUARDLEVEL = 4;


    public override bool TryDestroy(AttackAbs attackObj)
    {
        if(attackObj.getAttackLevel()>=getGuardLevel()){
            Destroy(gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int getGuardLevel()
    {
        return GUARDLEVEL;
    }
}
