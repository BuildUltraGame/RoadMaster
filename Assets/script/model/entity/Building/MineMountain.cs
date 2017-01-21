﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMountain : MonoBehaviour {

    /// <summary>
    ///  矿山
    ///  通过Inspector面板来设定是主矿还是分矿
    /// </summary>	

    public int totalMine = 100;
    public float increaseRate = 0.0f;//每秒增长速率
    public float increaseFlashTime = 1.0f;//
    public bool isSmallMine = false;
    public GameObject Lighthouse; //这个是我用来测试的，指定一个物体，这样比较好找到位置

    public int owner;

    public List<Spawner> SpawnerUnitList = new List<Spawner>();
    private Dictionary<string, Spawner> SpawnerUnitDict = new Dictionary<string, Spawner>();

    void Awake()
    {
        InitSpawnerDict();
    }

    void Start () {
        InvokeRepeating("increaseMine",0.0f,increaseFlashTime);
	}
	

	void Update () {
        //testBuild();
        Debug.Log("mine" + totalMine);

    }

    bool buildUnitByName(string name, Vector3 buildPos)
    {
        Spawner targetSpawner = null;
        if (SpawnerUnitDict.ContainsKey(name) == true)
        {
            targetSpawner = SpawnerUnitDict[name];
        }
        else
        {
            return false;
        }
        //Debug.Log("pos:" + buildPos);
        targetSpawner.setTarget(buildPos);
        if (totalMine >= targetSpawner.getCost())
        {
            if (targetSpawner.build())
            {
                totalMine -= targetSpawner.getCost();
            }
               
            else
                // TODOS
                return false;
        }
        return false;
    }

    void increaseMine()
    {
        totalMine += (int)(increaseRate * increaseFlashTime);
    }

    void InitSpawnerDict()
    {
        foreach(Spawner spawnerUnit in SpawnerUnitList)
        { 
            SpawnerUnitDict.Add(spawnerUnit.getName(), spawnerUnit);
        }
    }

    void testBuild()
    {
        Vector3 pos = Lighthouse.transform.position;
        buildUnitByName("基础运输矿车", pos);
    }

}
