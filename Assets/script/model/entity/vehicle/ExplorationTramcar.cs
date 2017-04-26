using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 探勘车
/// 功能:占领矿山
/// </summary>
public class ExplorationTramcar : MonoBehaviour {
    public float reqTime = 3f;//占领所需要时间
    private GameObject mineExp = null;

    void OnTriggerEnter(Collider other)
    {
        GameobjBase gBase = other.GetComponent<GameobjBase>();
        MineMountain mine = other.GetComponent<MineMountain>();
        if(mine==null){
            return;
        }
        if (!mineExp && mine.isSmallMine)
        {
            //是建筑且是矿山的时候
            mineExp = other.gameObject;
            HoldMineDelay();
        }

    }


    /// <summary>
    /// 延迟占领
    /// </summary>
    /// <param name="b"></param>
    private void HoldMineDelay()
    {
        Invoke("HoldMine",reqTime);
        GetComponent<RailwayMovable>().enabled=false;
        
    }

    /// <summary>
    /// 占领
    /// </summary>
    /// 
    void HoldMine()
    {
        GameobjBase myBase = GetComponent<GameobjBase>();
        mineExp.GetComponent<GameobjBase>().setOwner(myBase.getOwner());//设置游戏单位为自己方
        Destroy(gameObject);
    }



}
