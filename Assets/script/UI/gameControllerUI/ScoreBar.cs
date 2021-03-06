﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;
public class ScoreBar : UIWidgetContainer,IListener<ScoreEvent> {

    public GameObject barPrefab;
    public int barNum;
    public MapDescription mapDes;

    private Color[] cs= {Color.white,Color.yellow, Color.red };

    private List<GameObject> bars=new List<GameObject>();

    // Use this for initialization
    void Start () {
        UnityEventCenter.Register<ScoreEvent>(this);
        barNum = ScoreBoard.getInstance().playerNum;

        for (int i =0;i<barNum;i++)
        {
            
            bars.Add(NGUITools.AddChild(gameObject, barPrefab));
            bars[i].GetComponentInChildren<UISprite>().color= cs[i];
            bars[i].GetComponentInChildren<UISprite>().alpha=0.5f;
            bars[i].GetComponent<UIScrollBar>().value=0;
        }
      
       




    }

    // Update is called once per frame
    void Update () {
		
	}


    public void Handle(ScoreEvent message)
    {
        bars[message.getPlayer()].GetComponent<UIScrollBar>().value= message.getScore()/(float)mapDes.missionScore;
    }
}
