using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerAttack : AttackAbs {
    private GunnerAttack gunnerEnemy;
    private GuardAbs enemyAbs;
    private AttackAbs myAbs;
    public int bulletCount = 1;
    public const int ATTACKLEVEL = 6;

    void Awake()
    {
        myAbs = this.gameObject.GetComponent<AttackAbs>();
    }

    public override void Attack(GuardAbs guardObj)
    {
        enemyAbs = guardObj.gameObject.GetComponent<GuardAbs>();
        if (guardObj.gameObject.layer == Layers.CHARACTER && enemyAbs!=null)
        {
            if (enemyAbs.getGuardLevel() <= ATTACKLEVEL)
            {
                gunnerEnemy = guardObj.gameObject.GetComponent<GunnerAttack>();
                if (gunnerEnemy!=null)
                {
                    try
                    {
                        enemyAbs.TryDestroy(myAbs);
                        myAbs.TryDestroy(myAbs);
                    }
                    catch
                    {
                        guardObj.SendMessage(GameobjBase.TryDestroyFUNC);
                        gameObject.SendMessage(GameobjBase.TryDestroyFUNC);
                        //Destroy(guardObj.gameObject);
                        //Destroy(this.gameObject);
                    }

                }
                else
                {
                    try
                    {
                        enemyAbs.TryDestroy(myAbs);
                    }
                    catch
                    {
                        guardObj.gameObject.SendMessage(GameobjBase.TryDestroyFUNC);
                        //Destroy(guardObj.gameObject);
                    }
                    finally
                    {
                        bulletCount--;
                        if (bulletCount == 0)
                        {
                            myAbs.TryDestroy(myAbs);
                        }
                    }
                }
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
           gameObject.SendMessage(GameobjBase.TryDestroyFUNC);
            //Destroy(gameObject);
        }
        return true;
    }
}
