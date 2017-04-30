using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour,IListener<GameOverEvent> {
    public GameObject gameEndWindows;

    public UILabel title;
    public UILabel score;
    public UILabel scoreList;


    // Use this for initialization
    void Awake() {
        UnityEventCenter.Register<GameOverEvent>(this,1);
        gameEndWindows.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Handle(GameOverEvent message)
    {
        if (message.getWinner()==GameobjBase.PLAYER) {
            title.text = "你赢了";
        }
        else
        {
            title.text = "你输了";
        }

        

        string listStr = "";

        Hashtable table = message.getScores();
        score.text = System.Convert.ToInt32(table[GameobjBase.PLAYER]) + "";
        for (int i =0;i<table.Count;i++)
        {
            listStr += "玩家" + i + ":" + System.Convert.ToInt32(table[i])+"\n";
        }
        scoreList.text = listStr;
        gameEndWindows.SetActive(true);
        Time.timeScale = 0;

    }


    public void reStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("wf");   
    }

}
