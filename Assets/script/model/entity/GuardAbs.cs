using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;
/// <summary>
/// 防御模块
/// </summary>
public abstract class GuardAbs : CollisionBaseHandler {


    /// <summary>
    /// 销毁本防御对象
    /// </summary>
    /// <param name="attackObj">发出销毁请求的对方</param>
    /// <returns>是否销毁成功</returns>
    public bool TryDestroy(AttackAbs attackObj) {
        EventAggregator.SendMessage<BaseEvent>(new BaseEvent(gameObject, "Destroy", attackObj.gameObject, null));
        return false;
    }

    /// <summary>
    /// 获取防御等级
    /// </summary>
    /// <returns>防御等级</returns>
    public abstract int getGuardLevel();

}
