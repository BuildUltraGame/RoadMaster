using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 先把抢手的碰撞处理稍微写一写 
/// </summary>
public class GunnerTrigger : CollisionBaseHandler {
    private GunnerTrigger gunnerEnemy;
    private GuardAbs enemyAbs;
    private AttackAbs myAbs;

    public override void OnEnemyCollisionStart(Collider enemy)
    {
        base.OnEnemyCollisionStart(enemy);
        if(enemy.gameObject.layer == Layers.CHARACTER)
        {

            gunnerEnemy = enemy.gameObject.GetComponent<GunnerTrigger>();
            enemyAbs = enemy.gameObject.GetComponent<GuardAbs>();
            if (gunnerEnemy)
            {
                try
                {
                    enemyAbs.TryDestroy(myAbs);
                    myAbs.TryDestroy(myAbs);
                }
                catch
                {
                    Destroy(enemy.gameObject);
                    Destroy(this.gameObject);
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
                    Destroy(enemy.gameObject);
                }
            }


        }
    }

    void Awake()
    {
        myAbs = this.gameObject.GetComponent<AttackAbs>();
    }

}
