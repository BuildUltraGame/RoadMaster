using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 流氓脚本
/// </summary>
public class Rogue : CollisionBaseHandler {
    public float probabilityDestroy_baseTramcar = 1.0f;//摧毁基础矿车概率
    public float probabilityDestroy_overweightTramcar = 1.0f;//摧毁超载矿车的概率



    public override void OnEnemyCollisionStart(Collider enemy)
    {


        switch (enemy.gameObject.tag)
        {
            case Tags.Vehicle.BASETRAMCAR:
                //碰到基础矿车,几率双方消失

                if (Random.value <= probabilityDestroy_baseTramcar)
                {
                    //摧毁双方
                    Destroy(enemy.gameObject);
                    Destroy(this);

                }

                break;

            case Tags.Vehicle.OVERWEIGHTTRAMCAR:
                //碰到超重矿车
                if (Random.value <= probabilityDestroy_overweightTramcar)
                {
                    //摧毁双方
                    Destroy(enemy.gameObject);
                    Destroy(this);

                }


                break;

            default:
                break;

        }


    }

}
