using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopBtn : MonoBehaviour {
    float formerTimeScale;
    bool isPause;
    // Use this for initialization
	void Start () {
        formerTimeScale = 1;
        isPause = false;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void stop()
    {
        if(isPause==false)
        {
            formerTimeScale= Time.timeScale;
            Time.timeScale = 0;
            isPause = true;

            return;
        }
        else
        {
            Time.timeScale = formerTimeScale;
            isPause = false;
            return;
        }
        
    }
}
