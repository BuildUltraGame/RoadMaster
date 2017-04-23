using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;
public class ScoreBar : UIWidgetContainer,IListener<ScoreEvent> {

    public GameObject barPrefab;
    public int barNum;

    private Color[] cs= {Color.black,Color.white,Color.yellow };

    private List<GameObject> bars=new List<GameObject>();

    // Use this for initialization
    void Start () {
        EventAggregator.Register<ScoreEvent>(this);
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
        bars[message.getPlayer()].GetComponent<UIScrollBar>().value= message.getScore()/300f;
    }
}
