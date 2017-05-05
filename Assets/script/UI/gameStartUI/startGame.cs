using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void click()
    {
        Globe.LOADINGSCENCE = "wf";
        SceneManager.LoadScene("LoadingGame");
    }
}
