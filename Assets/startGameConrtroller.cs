using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGameConrtroller : MonoBehaviour {
    public Spawner spawner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    /// <summary>
    /// 开始按钮回调
    /// </summary>
    public void startGame()
    {
        build();
        Invoke("startGameNow",2f);
    }

    void startGameNow()
    {
        SceneManager.LoadSceneAsync("selector");
    }

    private void build()
    {
        spawner.build();
    }

    public void exit()
    {
        Application.Quit();
    }
}
