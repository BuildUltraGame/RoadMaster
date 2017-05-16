using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCenter : MonoBehaviour {
    private List<Spawner> splist=new List<Spawner>();

    private List<Spawner> useSpList = new List<Spawner>();

    public static UnitCenter instance;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(gameObject);
        instance = this;
        init();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public List<Spawner> getAllSpawner()
    {
        return splist;
    }

    public void setUseSpawnerList(List<Spawner> sp)
    {
        useSpList = sp;
    }

    private void init()
    {
        splist.Clear();
        GameObject[] prefabs = Resources.LoadAll<GameObject>("prefab/spawner");
        foreach (GameObject obj in prefabs)
        {
            splist.Add(obj.GetComponent<Spawner>());
        }
    }
}
