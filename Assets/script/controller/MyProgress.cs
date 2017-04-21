using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//管理游戏进度的类
[System.Serializable]
public class MyProgress : MonoBehaviour {

    int missionFinished;//当前阶段第几小关
    int missionMap;//第几阶段
    int cash;
    int miniUnlocked;//小游戏解锁数量
    int[] mineMuseum;//矿物展览

    MyProgress()
    {
        missionMap = 1;
        missionFinished = 1;
        miniUnlocked = 0;
        cash = 0;
    }
    void addCash(int num)
    {
        cash += num;
    }

    void pay (int num)
    {
        cash -= num;
    }

    void promoteMission()
    {
        missionFinished++;
    }

    void changeMap()
    {
        missionFinished = 1;
        missionMap++;
    }

    void unlockMinis(int num)
    {
        miniUnlocked += num;
    }
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
