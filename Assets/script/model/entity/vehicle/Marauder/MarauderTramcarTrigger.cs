using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 组长说先暂时不用
/// 劫掠者矿车的碰撞处理脚本，遇见带钱的敌方单位就抢走钱(如果对方也是掠夺者就平分)
/// </summary>
public class MarauderTramcarTrigger : CollisionBaseHandler
{
    private GoldCarrier myGoldCarrier;
    private GoldCarrier enemyGoldCarrier;
    private MarauderTramcarGuard isMarauder;
    private int totalPirateGold = 0;//当双方都是劫掠者时使用

    public override void OnEnemyCollisionStart(Collider enemy)
    {
        base.OnEnemyCollisionStart(enemy);
        enemyGoldCarrier = enemy.gameObject.GetComponent<GoldCarrier>();
        if (enemyGoldCarrier)
        {
            isMarauder = enemy.gameObject.GetComponent<MarauderTramcarGuard>();
            if (isMarauder)//如果是劫掠者，执行这一段
            {
                divideGoldEqually(enemyGoldCarrier, myGoldCarrier);
            }
            else//如果是普通小车，执行这一段
            {
                getEnemyGold(enemyGoldCarrier);

            }
        }
    }            

    /// <summary>
    /// 平分双方金钱
    /// </summary>
    /// <param name="enemyGold"></param>
    /// <param name="myGold"></param>
    void divideGoldEqually(GoldCarrier enemyGold,GoldCarrier myGold)
    {
        totalPirateGold += enemyGold.popGold();
        totalPirateGold += myGold.popGold();
        enemyGold.addGold(totalPirateGold / 2);
        myGold.addGold(totalPirateGold / 2);
        totalPirateGold = 0;
    }

    /// <summary>
    /// 直接抢走别人的钱
    /// </summary>
    /// <param name="goldCarrier"></param>
    void getEnemyGold(GoldCarrier goldCarrier)
    {
        myGoldCarrier.addGold(goldCarrier.popGold());
    }

    void Awake()
    {
        myGoldCarrier = this.GetComponent<GoldCarrier>();
    }

}
