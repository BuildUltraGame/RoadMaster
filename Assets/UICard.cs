using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICard : MonoBehaviour {

    Vector3 orign;
	// Use this for initialization
	void Start () {
        orign = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDragEnd() {
        print(transform.position);
        print(orign);
        transform.position = orign;
    }
}
