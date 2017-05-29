using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerInit : MonoBehaviour {

    public Spawner spawner;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void initSpeed(float speed)
    {
        Roadmovable rm = spawner.gameObject.GetComponent<Roadmovable>();
        RailwayMovable rwm = spawner.gameObject.GetComponent<RailwayMovable>();
        if (rm != null)
        {
            rm.speed = speed;
        }

        if (rwm != null)
        {
            rwm.speed = speed;
        }
    }

    private void initCost(int cost)
    {
        spawner.cost = cost;
    }

    private void initCD(float cd)
    {
        spawner.CD = cd;
    }

    private void initBuildTime(float t)
    {
        spawner.buildTime = t;
    }

    private void initGoldAmount(int amount)
    {
        spawner.spawnUnit.GetComponent<GoldCarrier>().MaxGold = amount;
    }





}
