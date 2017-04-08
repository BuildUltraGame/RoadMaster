using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;
public class BaseAI : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		EventAggregator.Register<DestroyEvent>(this);
        EventAggregator.Register<SpawnEvent>(this);
        EventAggregator.Register<DestroyEvent>(this);
        EventAggregator.Register<DestroyEvent>(this);
        EventAggregator.Register<DestroyEvent>(this);
        EventAggregator.Register<DestroyEvent>(this);
        EventAggregator.Register<DestroyEvent>(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
