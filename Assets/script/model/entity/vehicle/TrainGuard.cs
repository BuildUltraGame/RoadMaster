using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainGuard : GuardAbs {
    public const int GUARDLEVEL = 5;


    public override bool TryDestroy(AttackAbs attackObj)
    {
        if(attackObj.getAttackLevel()<=getGuardLevel()){
            attackObj.gameObject.SendMessage(GameobjBase.TryDestroyFUNC);
           // Destroy(attackObj);
            return false;
        }
        else
        {
            gameObject.SendMessage(GameobjBase.TryDestroyFUNC);
            //Destroy(gameObject);
            return true;

        }


    }

    public override int getGuardLevel()
    {
        return GUARDLEVEL;
    }
}
