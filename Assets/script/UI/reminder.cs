using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reminder : MonoBehaviour {
    public UILabel reminder;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void sendHint(string hint)
    {
        reminder = this.gameObject.GetComponent<UILabel>();
        reminder.text = hint;
    }
}
