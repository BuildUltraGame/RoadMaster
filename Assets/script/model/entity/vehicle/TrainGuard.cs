using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainGuard : GuardAbs {
    public const int GUARDLEVEL = 5;


    public override bool TryDestroy(AttackAbs attackObj)
    {
        if(attackObj.getAttackLevel()<=getGuardLevel()){
            Destroy(attackObj);
            return false;
        }
        else
        {
            Destroy(gameObject);
            return true;

        }


    }

    public override int getGuardLevel()
    {
        return GUARDLEVEL;
    }
}
