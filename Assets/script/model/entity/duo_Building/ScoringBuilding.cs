using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 终点类
/// 
/// 
/// </summary>
public class ScoringBuilding : MonoBehaviour {

    


	// Use this for initialization
	void Start () {
        ScoreBoard.getInstance().Init(2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {
        //此处违反了之前规定的写对对方造成的影响...但我觉得...这样写舒服...
        GameobjBase gBase= other.GetComponent<GameobjBase>();
        GoldCarrier carrier=other.GetComponent<GoldCarrier>();

        if(gBase==null||carrier==null){
            return;
        }

        ScoreBoard.getInstance().addScore(gBase.getOwner(),carrier.popGold());

        Destroy(other.gameObject);
        
    }



    
}
