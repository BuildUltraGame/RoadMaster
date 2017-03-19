using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getInput : MonoBehaviour {

    public string input;
    public UIInput inputBar;

	// Use this for initialization
	void Start () {
        inputBar = this.GetComponent<UIInput>();
	}
	
	// Update is called once per frame
	void Update () {
        input = inputBar.value;
        Debug.Log(input);
	}
}
