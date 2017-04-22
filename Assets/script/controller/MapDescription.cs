using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡信息脚本
/// </summary>
public class MapDescription : MonoBehaviour {
    public int missionScore;
    public int missionType;//关卡大分类
    public int missionNum;//当前分类下的哪一小关
    public int gameType;//游戏类型，指定了胜利条件

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
                            //todo:胜利后/失败后
                            if (player.playerNum == GameobjBase.PLAYER)
                            {
                                Debug.Log("那就是赢了");
                                return 1;
                            }
                            else
                            {
                                Debug.Log("你输了");
                                return -1;
                            }
                        }
                    return 0;

                }
            default: return -1000;
        }
    }
}
