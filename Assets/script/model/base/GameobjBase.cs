using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;

/// <summary>
/// 所有游戏对象最基础脚本
/// 
/// 
/// </summary>
public class GameobjBase : MonoBehaviour {
    public static string TryDestroyFUNC = "TryDestroyGameObject";
    public static string TryDestroyDelayFUNC = "TryDestroyDelay";
    public static string CanDestroyFUNC = "setCanDestory";

    private bool readyDestory = false;
    private bool canDestroy = true;

    public const int WORLD = -1;//世界单位

    public const int PLAYER = 0;//己方
    public const int OTHER_PLAYER1 = 1;//其他玩家1
    public const int OTHER_PLAYER2 = 2;//2
    public const int OTHER_PLAYER3 = 3;//3
    public const int OTHER_PLAYER4 = 4;//4
    public const int OTHER_PLAYER5 = 5;//5

    public int owner=PLAYER;//对象所属方,用于对战和任务模式区分敌我单位

    public int game_ID=0;//游戏物体ID

    public string game_name = null;//游戏物体名字
    public string game_name_en = null;
    public void setOwner(int newOwner)
    {
        if(newOwner>5||newOwner<-1){
            return;
        }
        owner = newOwner;
    }

    void Update()
    {
        if (readyDestory&&canDestroy)
        {
            Destroy(gameObject);
        }

    }

    public int getOwner()
    {
        return owner;
    }

    public virtual void OnMouseDown()
    {

        UnityEventCenter.SendMessage<GameObjSeletEvent>(new GameObjSeletEvent(gameObject));//游戏物体被选择
        
    }


    public void TryDestroyGameObject()
    {     
        _TryDestory();
    }

    public void TryDestroyDelay(float time)
    {
       Invoke("_TryDestory", time);
    }

    private void _TryDestory()
    {
        readyDestory = true;
        
    }


    public void setCanDestory(bool b)
    {
        canDestroy = b;
    }

}
