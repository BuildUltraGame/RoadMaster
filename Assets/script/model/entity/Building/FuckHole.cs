using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  悬崖
///  目前是摧毁接触到的所有物体
/// </summary>

public class FuckHole : CollisionBaseHandler{


    public override void OnEnemyCollisionStart(Collider enemy)
    {
        GameObject fuckedBoyOb = enemy.gameObject;
        Debug.Log("name:" + fuckedBoyOb.name);
        //应当调用物体的销毁脚本的销毁方法，但是好像还没有
        Destroy(fuckedBoyOb);
    }

    public override void OnSelfUnitCollisionStart(Collider selfUnit)
    {
        GameObject fuckedBoyOb = selfUnit.gameObject;
        Debug.Log("name:" + fuckedBoyOb.name);
        //应当调用物体的销毁脚本的销毁方法，但是好像还没有
        Destroy(fuckedBoyOb);
    }
}
