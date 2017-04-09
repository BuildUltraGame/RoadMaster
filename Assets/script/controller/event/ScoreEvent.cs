using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 分数改变事件
/// </summary>
public class ScoreEvent :  BaseEvent {
    private int score = 0;
    private int owner = GameobjBase.PLAYER;

    /// <summary>
    /// 分数改变
    /// </summary>
    /// <param name="_subject">计分板</param>
    /// <param name="owner">玩家</param>
    /// <param name="score">玩家对应的分数</param>
    public ScoreEvent(ScoreBoard board,int owner,int score)
        : base(null, "Score", null)
    {
        this.score = score;
        this.owner = owner;
    }


    public int getPlayer()
    {
        return owner;
    }


    public int getScore()
    {
        return score;
    }
}
