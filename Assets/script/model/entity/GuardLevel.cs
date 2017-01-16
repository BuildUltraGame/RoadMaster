using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 防御模块
/// 用来实现防御相关的功能 和攻击模块配合使用
/// 具体实现功能如:流氓可以破坏普通矿车一样
/// </summary>
public class GuardLevel : CollisionBaseHandler {

    private const int DEFAULT_LEVEL=1;

    public int level=DEFAULT_LEVEL;


    public int getLevel()
    {
        return level;
    }

    public override void OnEnemyCollisionStart(Collider enemy)
    {
        DestroyLevel destoryModule = enemy.gameObject.GetComponent<DestroyLevel>();
        if (null == destoryModule)
        {
            return;
        }

        if (destoryModule.getLevel() > level)
        {   //对方攻击等级大于自己的防御等级
            OnOppositeHighLevel(destoryModule.gameObject);
        }
        else if (destoryModule.getLevel() < level)
        {   //对方攻击等级低于自己防御等级
            OnOppositeLowLevel(destoryModule.gameObject);
        }
        else
        {  //等级相当
            OnEqualLevel(destoryModule.gameObject);
        }


    }




    public virtual void OnOppositeHighLevel(GameObject obj)
    {
        //这里应该破坏自己,但因为规定碰撞时候只写对对方的影响,代码应该在攻击模块
      
    }

    public virtual void OnOppositeLowLevel(GameObject obj)
    {
        DestroyObject(obj);//这里是直接摧毁,如果要在销毁前执行该对象的一些操作(如动画等,请写在对象的OnDestroy方法)
    }

    public virtual void OnEqualLevel(GameObject obj)
    {
        DestroyObject(obj);//这里是直接摧毁,如果要在销毁前执行该对象的一些操作(如动画等,请写在对象的OnDestroy方法)
    }


    
}
