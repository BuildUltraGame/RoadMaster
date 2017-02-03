using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct ACHIEVEMENT_WORD//保存成就描述的结构体
{
    public string name;
    public string description;
}
//管理成就系统的类
[System.Serializable]
public class Achievement : MonoBehaviour {
    int amount;

    public void enable(int num)
    {
        achievement[num] = 1;   
    }

    int[] achievement;

    Achievement()
    {
        amount = 1;///////成就数量
        achievement = new int[amount];
        for (int i = 0; i < amount; i++)
        {
            achievement[i] = 0;
        }
    }
    // Use this for initialization
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
