using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;

public class scoreBoard : MonoBehaviour, IListener<MineSelectEvent>, IListener<cancelMountainEvent>
{

    MineMountain mineNow;
    public UILabel scoreNeed;//距胜利所需分数
    public UILabel scoreNow;//当前分数
	// Use this for initialization
	void Start () {
        EventAggregator.Register<MineSelectEvent>(this);
        EventAggregator.Register<cancelMountainEvent>(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Handle(MineSelectEvent message)
    {
        mineNow=message.getMine();
        scoreNow.text = ""+mineNow.currentScore;
        //scoreNeed.text=mineNow.???//计算获得
    }

    
    void OnDisable()
    {
        EventAggregator.UnRegister<MineSelectEvent>(this);
        EventAggregator.UnRegister<cancelMountainEvent>(this);
    }

    public void Handle(cancelMountainEvent message)
    {

    }
}
