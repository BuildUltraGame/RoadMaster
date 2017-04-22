using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroButton : MonoBehaviour {

    private UISprite TouchToUse;
	// Use this for initialization
	void Start () {
        TouchToUse = this.gameObject.GetComponent<UISprite>();
        //TouchToUse.spriteName=;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClick()
    {

    }
}
