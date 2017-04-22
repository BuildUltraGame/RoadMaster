using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用户的信息，用以判定胜利等
/// </summary>
public class playerInformation {
   
    public int playerNum;
    public int score;
    public playerInformation(int Num,int Score)
    {
        playerNum = Num;
        score = Score;
    }
    
}
