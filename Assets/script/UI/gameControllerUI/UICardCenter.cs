using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;

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
            NGUITools.Destroy(o);
        }

        cards.Clear();
    }

    public void Handle(MineSelectEvent message)
    {
        
       MineMountain temp = message.getMine();
        if (temp.gameObject.GetComponent<GameobjBase>().getOwner() == GameobjBase.PLAYER)
        {
            mine = temp;
            initCards();
        }
        else {
            mine = null;
            foreach (GameObject o in cards)
            {
                NGUITools.Destroy(o);
            }

            cards.Clear();
        }
    }

    // Use this for initialization
    void Awake () {
        UnityEventCenter.Register<MineSelectEvent>(this);
        UnityEventCenter.Register<cancelMountainEvent>(this);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void initCards()
    {
        foreach (GameObject o in cards)
        {
            grid.RemoveChild(o.transform);
            NGUITools.Destroy(o);
        }

        cards.Clear();

        List<Spawner> sp = mine.getSpawnerList();
        for (int i=0; i<sp.Count;i++)
        {
            GameObject g = NGUITools.AddChild(grid.gameObject, cardPrefab);
            cards.Add(g);
            g.GetComponent<UICard>().setSpawner(sp[i]);
        }
        grid.repositionNow = true;
        grid.Reposition();

    }
}
