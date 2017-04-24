using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 劫掠者矿车的防御脚本，要和trigger一起挂在车上
/// </summary>
public class MarauderTramcarGuard : GuardAbs
{

    public const int GUARDLEVEL = 4;

    public override bool TryDestroy(AttackAbs attackObj)
    {
        if (attackObj.getAttackLevel() > getGuardLevel())
        {
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
