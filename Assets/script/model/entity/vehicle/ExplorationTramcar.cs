using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 探勘车
/// 功能:占领矿山
/// </summary>
public class ExplorationTramcar : MonoBehaviour {
    public float reqTime = 3f;//占领所需要时间


    void OnTriggerEnter(Collider other)
    {
        GameobjBase gBase = other.GetComponent<GameobjBase>();

        if(other.gameObject.layer==Layers.BUILDING&&other.gameObject.tag==Tags.Building.MINE){
            //是建筑且是矿山的时候
            HoldMineDelay(gBase);
        }

    }


    /// <summary>
    /// 延迟占领
    /// </summary>
    /// <param name="b"></param>
    private void HoldMineDelay(GameobjBase b)
    {
      TimerController.Timer timer=TimerController.getInstance().NewTimer(reqTime, false, 
          delegate(float time) { }, 
          delegate() {
              HoldMine(b);
          });

      timer.setAutoDestory(true);//自动销毁计时器
      timer.Start();
        
    }

    /// <summary>
    /// 占领
    /// </summary>
    /// <param name="b"></param>
    private void HoldMine(GameobjBase b)
    {
        GameobjBase myBase = GetComponent<GameobjBase>();
        b.setOwner(myBase.getOwner());//设置游戏单位为自己方
    }



}
