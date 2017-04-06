using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;

public class TouchController : MonoBehaviour, IListener<RequestSelectEvent>
{
    private List<RequestSelectEvent> reqList=new List<RequestSelectEvent>();
    
	void Awake () {
        EventAggregator.Register<RequestSelectEvent>(this);
	}

    void OnDisable()
    {
        EventAggregator.UnRegister<RequestSelectEvent>(this);
    }
	void Update () {
	    	
	}

    public void Handle(RequestSelectEvent message)
    {
        reqList.Add(message);
    }
}
