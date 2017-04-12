using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;

public class scoreBoard : MonoBehaviour, IListener<ScoreEvent>, IListener<cancelMountainEvent>
{


    public UILabel scoreNeed;//距胜利所需分数
    public UILabel scoreNow;//当前分数
	// Use this for initialization
	void Start () {
        EventAggregator.Register<ScoreEvent>(this);
        EventAggregator.Register<cancelMountainEvent>(this);
        scoreNow.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Handle(ScoreEvent message)
    {
        if(message.getPlayer()==GameobjBase.PLAYER){
            scoreNow.text = "" + message.getScore();
        }
       
    }

    
    void OnDisable()
    {
        EventAggregator.UnRegister<ScoreEvent>(this);
        EventAggregator.UnRegister<cancelMountainEvent>(this);
    }

    public void Handle(cancelMountainEvent message)
    {

    }
}
