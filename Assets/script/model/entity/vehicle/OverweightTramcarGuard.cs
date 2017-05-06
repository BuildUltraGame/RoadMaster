using System.Collections;
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
        gameObject.SendMessage(GameobjBase.TryDestroyFUNC);
       // Destroy(gameObject);
            return true;


    }

    public override int getGuardLevel()
    {
        return GUARDLEVEL;
    }
}
