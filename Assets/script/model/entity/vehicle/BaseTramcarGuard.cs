using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 基础矿车
/// 
/// 目前没啥特殊功能代码,毕竟只是运矿
/// </summary>
public class BaseTramcarGuard : GuardAbs
{
    public const int GUARDLEVEL=2;

    public override bool TryDestroy(AttackAbs attackObj)
    {
        Destroy(gameObject);
        return true;
    }

    public override int getGuardLevel()
    {
        return GUARDLEVEL;
    }
}
