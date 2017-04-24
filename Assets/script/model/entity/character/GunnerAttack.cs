using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerAttack : AttackAbs {

    public const int ATTACKLEVEL = 6;

    public override void Attack(GuardAbs guardObj)
    {
        if (guardObj.getGuardLevel() <= getAttackLevel())
        {
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
        if (attackObj == this)
        {
            Destroy(gameObject);
        }
        return true;
    }
}
