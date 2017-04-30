using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEvent;

/// <summary>
/// 计分板,包括了所有玩家的分数情况
/// 
/// 依旧单例模式
/// 
/// 使用前必须初始化,至于为什么初始化,
/// 如果看了代码,你会认为没必要初始化,当然也可以不初始化,
/// 但我认为,这个不初始化的话,如果玩家都没有得分,你都不知道参加的玩家有谁
/// 如果有更好的方法,请告诉我
/// </summary>
public class ScoreBoard :IListener<ScoreAddEvent>
{
    private static ScoreBoard _instance=new ScoreBoard();
    private Hashtable scores=new Hashtable();

    public int playerNum=2;
    public static ScoreBoard getInstance()
    {
        return _instance;
    }

    private ScoreBoard()
    {
        UnityEventCenter.Register<ScoreAddEvent>(this);
    }
    /// <summary>
    /// 初始化玩家数
    /// </summary>
    /// <param name="playerNum">玩家的数目</param>
    public void Init(int playerNum)
    {//这里暂时不考虑联机的问题....想考虑的话你可以自己改代码先
        this.playerNum = playerNum;
        scores.Clear();
        scores.Add(-1, 0);//这个是世界,也算是一个玩家
        for (int i = 0; i < playerNum;i++ )
        {
            scores.Add(i,0);//这里是因为gameobjbase中对玩家的定义都是用整数..
        }
    }
    /// <summary>
    /// 加分
    /// </summary>
    /// <param name="player">加分的玩家</param>
    /// <param name="score">分</param>
    private void addScore(int player,int score)
    {
        if(scores.ContainsKey(player)){
            scores[player] = System.Convert.ToInt32(scores[player]) + score;
        }
        else
        {
            throw new System.Exception("- -你难道用了外挂不成,明明没有这个玩家,不不不,其实我知道的,你忘了初始化计分板了吧");
        }

        UnityEventCenter.SendMessage<ScoreEvent>(new ScoreEvent(this, player, System.Convert.ToInt32(scores[player])));//发送玩家分数更新信息
    }


    public void Handle(ScoreAddEvent message)
    {
        if(message!=null){
            addScore(message.getOwner(),message.getScore());
        }
    }

    public Hashtable getScoreData()
    {
        Hashtable table = scores.Clone() as Hashtable;
        table.Remove(-1);
        return table;
    }
}
