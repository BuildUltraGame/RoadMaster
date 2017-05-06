using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGuard : GuardAbs
{

    public const int GUARDLEVEL = 5;//gunner是6，我就先设置成5了

    public override bool TryDestroy(AttackAbs attackObj)
    {
        if (attackObj.getAttackLevel() >= getGuardLevel())
        {
            gameObject.SendMessage(GameobjBase.TryDestroyFUNC);
            //Destroy(gameObject);
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
