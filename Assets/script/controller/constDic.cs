using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//管理游戏状态等常量的类
//////////////////成就的索引似乎不是很合理，待修改

public class constDic : MonoBehaviour {
    //指示游戏所处于状态的常量
    public const int INITIAL = 1000;



    //成就系统与成就编号的对应
    Dictionary<int,ACHIEVEMENT_WORD> achievementDic = new Dictionary<int,ACHIEVEMENT_WORD>();
    constDic()
    {
        //todo 添加以后的成就
        ACHIEVEMENT_WORD to_add=new ACHIEVEMENT_WORD();
        ///////
        to_add.name = "开门大吉";
        to_add.description = "获取第一场胜利";
        achievementDic.Add(0, to_add);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
