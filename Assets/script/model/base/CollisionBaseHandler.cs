using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 碰撞处理基类
/// 全部含有碰撞的脚本直接继承本类
/// 已经将碰撞敌人和碰撞己方分开
/// </summary>
public class CollisionBaseHandler : MonoBehaviour
{

	


    void OnTriggerEnter(Collider other)
    {
        GameobjBase gBase=other.GetComponent<GameobjBase>();
        if (gBase == null)
        {
            return;
        }
        if (gBase.getOwner() == GameobjBase.PLAYER)
        {
            OnSelfUnitCollisionStart(other);
        }
        else
        {
            OnEnemyCollisionStart(other);

        }
     
    }

    void OnTriggerExit(Collider other)
    {
        GameobjBase gBase = other.GetComponent<GameobjBase>();
        if (gBase == null)
        {
            return;
        }
        if (gBase.getOwner() == GameobjBase.PLAYER)
        {
            OnSelfUnitCollisionEnd(other);
        }
        else
        {
            OnEnemyCollisionEnd(other);

        }
    }

    void OnTriggerStay(Collider other)
    {
        GameobjBase gBase = other.GetComponent<GameobjBase>();
        if(gBase==null){
            return;
        }
        if (gBase.getOwner() == GameobjBase.PLAYER)
        {
            OnSelfUnitCollisionStay(other);
        }
        else
        {
            OnEnemyCollisionStay(other);

        }
    }


    /// <summary>
    /// 当开始碰撞到敌方单位的时候
    /// </summary>
    /// <param name="enemy">敌方单位碰撞体</param>
    public virtual void OnEnemyCollisionStart(Collider enemy)
    {

    }
    /// <summary>
    /// 当碰撞敌方单位结束的时候
    /// </summary>
    /// <param name="enemy">敌方单位碰撞体</param>
    public virtual void OnEnemyCollisionEnd(Collider enemy)
    {

    }
    /// <summary>
    /// 当碰撞敌方单位的时候(持续)
    /// </summary>
    /// <param name="enemy">敌方单位碰撞体</param>
    public virtual void OnEnemyCollisionStay(Collider enemy)
    {

    }

    /// <summary>
    /// 当开始碰撞到己方单位的时候
    /// </summary>
    /// <param name="selfUnit">己方单位碰撞体</param>
    public virtual void OnSelfUnitCollisionStart(Collider selfUnit)
    {

    }
    /// <summary>
    /// 当碰撞己方单位结束的时候
    /// </summary>
    /// <param name="selfUnit">己方单位碰撞体</param>
    public virtual void OnSelfUnitCollisionEnd(Collider selfUnit)
    {

    }
    /// <summary>
    /// 当碰撞己方单位的时候(持续)
    /// </summary>
    /// <param name="selfUnit">己方单位碰撞体</param>
    public virtual void OnSelfUnitCollisionStay(Collider selfUnit)
    {

    }
    

}
