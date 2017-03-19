using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public UISprite icon;

    void Awake()
    {
        icon = this.gameObject.GetComponent<UISprite>();
    }

	// Use this for initialization
	void Start () {
		
	}
	

    public void front()
    {
        icon.spriteName = "Flag-US";
    }

    public void next()
    {
        icon.spriteName = "Flag-US";
    }

}
