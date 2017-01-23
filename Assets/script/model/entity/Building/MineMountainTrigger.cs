using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 矿山的碰撞处理模块，与矿山主体分离开来
/// </summary>
public class MineMountainTrigger : CollisionBaseHandler
{

    public override void OnEnemyCollisionStart(Collider enemy)
    {
        MineMountain mineMountain = this.GetComponent<MineMountain>();
        base.OnEnemyCollisionStart(enemy);
        if (mineMountain.isSmallMine)
        {
            //TODO 判断是否是占领单位
        }

        Destroy(enemy.gameObject);

    }

    public override void OnSelfUnitCollisionStart(Collider selfUnit)
    {
        base.OnSelfUnitCollisionEnd(selfUnit);
        GoldCarrier goldCarrier = selfUnit.GetComponent<GoldCarrier>();
        if (goldCarrier != null)
        {
            MineMountain mineMountain = this.GetComponent<MineMountain>();
            mineMountain.getMineFromCar(goldCarrier.popGold());
        }
        Destroy(selfUnit.gameObject);        

    }

}
