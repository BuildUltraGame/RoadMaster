using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectManager : MonoBehaviour {

    public GameObject cardPrefab;
    public UIGrid allGrid;
    public UIGrid selectGrid;


	// Use this for initialization
	void Start () {
        List<Spawner> sps = UnitCenter.instance.getAllSpawner();

        foreach (Spawner s in sps)
        {
            GameObject g = NGUITools.AddChild(allGrid.gameObject, cardPrefab);
            g.GetComponent<UICard>().setSpawner(s);

        }
	}


    public void SelectFinish()
    {
        List<Spawner> uses = new List<Spawner>();

       UICard[] cards= selectGrid.GetComponentsInChildren<UICard>();

        for (int i =0;i<cards.Length;i++)
        {
            uses.Add(cards[i].getSpawner());
        }

        UnitCenter.instance.setUseSpawnerList(uses);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
