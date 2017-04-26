using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroButton : MonoBehaviour {
    public MineMountain mine;
    private UISprite TouchToUse;
    public GameObject ai;
	// Use this for initialization
	void Start () {
        TouchToUse = this.gameObject.GetComponent<UISprite>();
        //TouchToUse.spriteName=;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Money()
    {
        mine.totalMine += 1000;
    }

    public void EnableAI()
    {
        ai.SetActive(!ai.activeSelf);
    }
}
