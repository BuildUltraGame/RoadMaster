using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 督察员
/// </summary>
public class InspectorAttack : AttackAbs
{
    public float probabilityDestroy_baseTramcar = 0.7f;//摧毁基础矿车概率

    public const int ATTACKLEVEL=3;


    public override void Attack(GuardAbs guardObj)
    {
      
        if(guardObj.getGuardLevel()<=getAttackLevel()){
            if(guardObj.getGuardLevel()==BaseTramcarGuard.GUARDLEVEL){

                if (Random.value < probabilityDestroy_baseTramcar)
                {
                    //摧毁对方
                    if (guardObj.DestrotyGameObj(this))
                    {//成功摧毁对方
                        TryDestroy(this);
                    }
                
                }
            }
            else
            {

                //摧毁对方
                if (guardObj.DestrotyGameObj(this))
                {//成功摧毁对方
                    TryDestroy(this);
                }
            }
        }

    }

    public override bool TryDestroy(AttackAbs attackObj)
    {
        if(attackObj==this){
            Destroy(gameObject);
        }

        return true;
    }

    public override int getAttackLevel()
    {
        return ATTACKLEVEL;
    }
}
