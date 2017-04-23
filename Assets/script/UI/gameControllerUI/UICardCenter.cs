using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;

public class UICardCenter : MonoBehaviour,IListener<MineSelectEvent>,IListener<cancelMountainEvent> {
   
    public GameObject cardPrefab;

    public UIGrid grid;

    private MineMountain mine;
    private List<GameObject> cards=new List<GameObject>();

    public void Handle(cancelMountainEvent message)
    {
        mine = null;
        foreach (GameObject o in cards)
        {
            Destroy(o);
        }

        cards.Clear();
    }

    public void Handle(MineSelectEvent message)
    {
        mine = message.getMine();
        initCards();
    }

    // Use this for initialization
    void Awake () {
        EventAggregator.Register<MineSelectEvent>(this);
        EventAggregator.Register<cancelMountainEvent>(this);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void initCards()
    {
        List<Spawner> sp = mine.getSpawnerList();
        for (int i=0; i<sp.Count;i++)
        {
            GameObject g = NGUITools.AddChild(grid.gameObject, cardPrefab);
            cards.Add(g);
            g.GetComponent<UICard>().setID(sp[i].spawnUnit.GetComponent<GameobjBase>().game_ID);
            g.GetComponent<UICard>().setName(sp[i].spawnUnit.GetComponent<GameobjBase>().game_name);
        }

        grid.Reposition();

    }
}
