using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;

/// <summary>
/// 关卡信息脚本
/// </summary>
public class MapDescription : MonoBehaviour {
    public int missionScore;
    public int missionType;//关卡大分类
    public int missionNum;//当前分类下的哪一小关
    public int gameType;//游戏类型，指定了胜利条件
    public bool loadFinished;//载入是否完毕
   

    ///游戏类型参数
    public const int EXPLORE = 1001;//探索模式
    public const int ARRIVAL_VERSUS = 1002;//双方胜利条件为分数的对抗模式
    public const int TROUBLEMAKER = 1003;//干扰模式
    /////////

    //
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    Dictionary<string, string> items = new Dictionary<string, string>();
    void loadMapDescription()
    {
        loadFinished = true;
    }

    /// <summary>
    /// 游戏结束判定，返回1为胜利，0为未结束，-1为失败
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    public int isWin(playerInformation player)
    {
        switch (gameType)
        {
            case ARRIVAL_VERSUS:
                {
                        if (player.score >= missionScore)
                        {
                            //发送游戏结束消息
                            UnityEventCenter.SendMessage<GameOverEvent>(new GameOverEvent(player.playerNum, ScoreBoard.getInstance().getScoreData()));

                        }
                    return 0;

                }
            default: return -1000;
        }
    }
}
