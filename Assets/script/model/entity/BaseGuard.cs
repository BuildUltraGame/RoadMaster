using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基础防御模块,最基础版本,只有该单位的防御机制很简单的时候才使用
/// </summary>
public class BaseGuard : GuardAbs
{
    public int level = 1;

    public override int getGuardLevel()
    {
        return level;
    }

    public override bool TryDestroy(AttackAbs attackObj)
    {
        if (attackObj.getAttackLevel() >= getGuardLevel())
        {
            gameObject.SendMessage(GameobjBase.TryDestroyFUNC);
            //Destroy(gameObject);
            return true;
        }
        else {
            return false;
        }
    }

 
}
