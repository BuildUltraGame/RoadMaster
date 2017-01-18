using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  破坏模块
///  和防御模块相对GuardLevel.cs
/// 
/// 用于判断和防御模块等级的大小关系,并且执行相应函数
/// 默认是销毁
/// 
/// 推荐继承后重写方法
/// 
/// </summary>
public class DestroyLevel : CollisionBaseHandler {


    private const int DEFAULT_LEVEL = 1;

    public int level = DEFAULT_LEVEL;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int getLevel()
    {
        return level;
    }

    public override void OnEnemyCollisionStart(Collider enemy)
    {
        GuardLevel guard = enemy.gameObject.GetComponent<GuardLevel>();
        if (null == guard)
        {
            return;
        }

        if (guard.getLevel() > level)
        {
            //对方防御等级大于自己的攻击等级
            OnOppositeHighLevel(guard.gameObject);
        }
        else if (guard.getLevel() < level)
        {//对方防御等级低于自己攻击等级
            OnOppositeLowLevel(guard.gameObject);
        }
        else
        {//等级相当
            OnEqualLevel(guard.gameObject);
        }
    }



    

  

    public virtual void OnOppositeHighLevel(GameObject obj)
    {
        //这里应该破坏自己,但因为规定碰撞时候只写对对方的影响,代码应该在防御模块
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
