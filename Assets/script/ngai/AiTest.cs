using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AiTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StreamReader sr = new StreamReader("F://a.txt");
        CDAction c = new CDAction();
        JsonUtility.FromJsonOverwrite(sr.ReadToEnd(),c);
        print(c.num);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
