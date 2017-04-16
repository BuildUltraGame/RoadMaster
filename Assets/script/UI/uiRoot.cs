using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class uiRoot : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AdaptiveUI();
        Camera.main.fieldOfView = getCameraFOV(60);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    static public void AdaptiveUI()
    {
        int ManualWidth = 1280;
        int ManualHeight = 720;
        UIRoot uiRoot = GameObject.FindObjectOfType<UIRoot>();
        if (uiRoot != null)
        {
            if (System.Convert.ToSingle(Screen.height) / Screen.width > System.Convert.ToSingle(ManualHeight) / ManualWidth)
                uiRoot.manualHeight = Mathf.RoundToInt(System.Convert.ToSingle(ManualWidth) / Screen.width * Screen.height);
            else
                uiRoot.manualHeight = ManualHeight;
        }
    }
    static public float getCameraFOV(float currentFOV)
    {
        UIRoot root = GameObject.FindObjectOfType<UIRoot>();
        float scale = Convert.ToSingle(root.manualHeight / 640f);
        return currentFOV * scale;
    }
}
