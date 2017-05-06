using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 矿山的碰撞处理模块，与矿山主体分离开来
/// 已知问题:碰到刚刚生产出来的矿车会直接被判断是送钱的.
/// </summary>
public class MineMountainTrigger : CollisionBaseHandler
{

    public override void OnEnemyCollisionStart(Collider enemy)
    {
        MineMountain mineMountain = this.GetComponent<MineMountain>();
        ExplorationTramcar explorationTramcar = enemy.GetComponent<ExplorationTramcar>();
        base.OnEnemyCollisionStart(enemy);
        if (!(mineMountain.isSmallMine && explorationTramcar))
        {
            GoldCarrier goldCarrier = enemy.GetComponent<GoldCarrier>();
            if (goldCarrier != null)
            {
                mineMountain.getMineFromCar(goldCarrier.popGold());
            }
            enemy.gameObject.SendMessage(GameobjBase.TryDestroyFUNC);
            //Destroy(enemy.gameObject);
        }


    }

    public override void OnSelfUnitCollisionStart(Collider selfUnit)
    {
        base.OnSelfUnitCollisionStart(selfUnit);
        if (selfUnit.gameObject.GetComponent<DestoryMe>() != null)
        {
            if (!selfUnit.gameObject.GetComponent<DestoryMe>().couldDestory)
            {
                selfUnit.gameObject.GetComponent<DestoryMe>().couldDestory = true;
            }
            else
            {
                
                GoldCarrier goldCarrier = selfUnit.GetComponent<GoldCarrier>();
                if (goldCarrier != null)
                {
                    MineMountain mineMountain = this.GetComponent<MineMountain>();
                    mineMountain.getMineFromCar(goldCarrier.popGold());
                }
                selfUnit.gameObject.SendMessage(GameobjBase.TryDestroyFUNC,SendMessageOptions.DontRequireReceiver);
                //Destroy(selfUnit.gameObject);
            }
        }
        else
        {
            selfUnit.gameObject.AddComponent<DestoryMe>();
            selfUnit.gameObject.GetComponent<DestoryMe>().couldDestory = true;
        }
    }
}
