using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;
public class Reminder : MonoBehaviour,IListener<waitClickEvent>,IListener<cancelClickEvent> {
    public UILabel reminder;//用户提醒器
	// Use this for initialization
	void Start () {
        EventAggregator.Register<waitClickEvent>(this);
        EventAggregator.Register<cancelClickEvent>(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void sendHint(string hint)
    {
        reminder = this.gameObject.GetComponent<UILabel>();
        reminder.text = hint;
    }
    

    public void Handle(waitClickEvent message)
    {
        sendHint("Please click your target point!!!");
    }

    public void Handle(cancelClickEvent message)
    {
        sendHint(null);
    }
    void OnDisable()
    {
        EventAggregator.UnRegister<waitClickEvent>(this);
        EventAggregator.UnRegister<cancelClickEvent>(this);
    }
}
