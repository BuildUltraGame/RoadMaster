using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 某玩家分数增加后会产生该事件
/// </summary>
public class ScoreAddEvent : BaseEvent {
    private int owner = GameobjBase.WORLD;
    private int score = 0;
    public ScoreAddEvent(GameObject scoreBuilding,int owner,int addscore)
        : base(scoreBuilding, "Score", null)
    {
        this.owner = owner;
        this.score = addscore;
    }

    /// <summary>
    /// 分数增加量
    /// </summary>
    /// <returns>分数增加量</returns>
    public int getScore()
    {
        return score;
    }

    /// <summary>
    /// 当前分数所属方
    /// </summary>
    /// <returns>所属方</returns>
    public int getOwner()
    {
        return owner;
    }



	
}
