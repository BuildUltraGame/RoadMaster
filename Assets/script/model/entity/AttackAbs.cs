using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 攻击模块
/// </summary>
public abstract class AttackAbs : CollisionBaseHandler {


    public override void OnEnemyCollisionStart(Collider enemy)
    {
        base.OnEnemyCollisionStart(enemy);
        GuardAbs enemyG=enemy.GetComponent<GuardAbs>();
        GameobjBase enemyB=enemy.GetComponent<GameobjBase>();

        if(enemyG==null||enemyB==null){
            return;
        }

        Attack(enemyG);   
    }

    /// <summary>
    /// 攻击某防御对象时候
    /// </summary>
    /// <param name="guardObj">该防御对象</param>
    public abstract void Attack(GuardAbs guardObj);

    /// <summary>
    /// 获取攻击等级
    /// </summary>
    /// <returns>攻击等级</returns>
    public abstract int getAttackLevel();

    /// <summary>
    /// 销毁本攻击对象
    /// </summary>
    /// <param name="attackObj">发出销毁请求的对方</param>
    /// <returns>是否销毁成功</returns>
    public abstract bool TryDestroy(AttackAbs attackObj);

}
