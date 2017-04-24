using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopIcon : MonoBehaviour {
    public UISprite icon;
	// Use this for initialization
	void Start () {
        icon = this.gameObject.GetComponent<UISprite>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            icon.spriteName = "right";
        }
        else
        {
            icon.spriteName = "ring";
        }
    }  
}
