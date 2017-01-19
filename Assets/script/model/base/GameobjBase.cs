using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有游戏对象最基础脚本
/// 
/// 
/// </summary>
public class GameobjBase : MonoBehaviour {
    public const int WORLD = -1;//世界单位

    public const int PLAYER = 0;//己方
    public const int OTHER_PLAYER1 = 1;//其他玩家1
    public const int OTHER_PLAYER2 = 2;//2
    public const int OTHER_PLAYER3 = 3;//3
    public const int OTHER_PLAYER4 = 4;//4
    public const int OTHER_PLAYER5 = 5;//5

    public int owner=PLAYER;//对象所属方,用于对战和任务模式区分敌我单位


    public void setOwner(int newOwner)
    {
        if(newOwner>5||newOwner<0){
            return;
        }
        owner = newOwner;
    }

    public int getOwner()
    {
        return owner;
    }
}
