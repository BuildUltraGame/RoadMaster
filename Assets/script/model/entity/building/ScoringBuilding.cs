using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;
/// <summary>
/// 终点类
/// 
/// 
/// </summary>
public class ScoringBuilding : MonoBehaviour {

    


	// Use this for initialization
	void Start () {
        ScoreBoard.getInstance().Init(2);
        PathDataCenter.registerPathPoint(transform.position);
        EventAggregator.SendMessage<ScoreBuildingSpawnEvent>(new ScoreBuildingSpawnEvent(gameObject));
	}

    void OnDisable()
    {
        PathDataCenter.unRegisterPathPoint(transform.position);
    }


    void OnTriggerEnter(Collider other)
    {
        //这里要处理所有玩家的单位,所以不去继承之前写的那个基类
        GameobjBase gBase= other.GetComponent<GameobjBase>();
        GoldCarrier carrier=other.GetComponent<GoldCarrier>();

        if(gBase==null||carrier==null){
            return;
        }
        EventAggregator.SendMessage<ScoreAddEvent>(new ScoreAddEvent(gameObject, gBase.getOwner(), carrier.popGold()));//发送分数增加事件

        Destroy(other.gameObject);
        
    }



    
}
