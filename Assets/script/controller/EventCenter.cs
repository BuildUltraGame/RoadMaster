
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;
/// <summary>
/// 事件中心,会监听所有事件
/// </summary>
public class EventCenter : MonoBehaviour, IListener<BaseEvent>, IListener<GameObjSeletEvent>,IListener<SpawnEvent>
{

    private static EventCenter _instance;
    

    public static EventCenter getInstance()
    {
        return _instance;
    }

    

    void Awake()
    {
        _instance = this;
        
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        print("OnEnable");
    }

    void Start()
    {
        print("Start");
        EventAggregator.Register<BaseEvent>(this);
        EventAggregator.Register<GameObjSeletEvent>(this);
        EventAggregator.Register<SpawnEvent>(this);
    }


    void OnDestroy() {
        EventAggregator.UnRegister<BaseEvent>(this);
    }

	
	// Update is called once per frame
	void Update () {
       
	}

    public void Handle(BaseEvent message)
    {
        print(message.getObject()+message.getVerb()+message.getSubject());
    }

    public void Handle(GameObjSeletEvent message)
    {
        print(message.getObject() + message.getVerb() + message.getSubject());
    }

    public void Handle(SpawnEvent message)
    {
        print("生产单位");
        print(message.getObject() + message.getVerb() + message.getSubject());
    }
}
