using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CDAction : MonoBehaviour,IAINode
{

    public int num;

    public void addNode(IAINode node) { }
    public void addNodes(ICollection c) { }
    public bool condition(AIContext context) { return false; }

    public void execute(AIContext context)
    {

    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
