using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;
/// <summary>
/// 终点类
/// 
/// 
/// </summary>
public class ScoringBuilding : MonoBehaviour {

    


	// Use this for initialization
	void Start () {

        UnityEventCenter.SendMessage<ScoreBuildingSpawnEvent>(new ScoreBuildingSpawnEvent(gameObject));
	}


    void OnTriggerEnter(Collider other)
    {
        //这里要处理所有玩家的单位,所以不去继承之前写的那个基类
        GameobjBase gBase= other.GetComponent<GameobjBase>();
        GoldCarrier carrier=other.GetComponent<GoldCarrier>();

        if(gBase==null||carrier==null){
            return;
        }
        UnityEventCenter.SendMessage<ScoreAddEvent>(new ScoreAddEvent(gameObject, gBase.getOwner(), carrier.popGold()));//发送分数增加事件

        Destroy(other.gameObject);
        
    }



    
}
