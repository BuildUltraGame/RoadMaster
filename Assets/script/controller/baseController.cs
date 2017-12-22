using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;


//基础的游戏进程控制器
public class baseController : MonoBehaviour,IListener<MineSelectEvent>,IListener<CreateUnitEvent>,IListener<ScoreEvent>{

 

    MineMountain mineSelected;//当前选中的矿山，未选中则为null
    public static int isWin = 0;
    public static int missionToLoad = 0;
    ////touchController部分

   

    ////
    /// <summary>
    /// 获取矿山的引用
    /// </summary>
    /// <returns></returns>
    MineMountain getMine()
    {
        return mineSelected;
        
    }
    
    /// <summary>
    /// 
    /// </summary>
    void loadMap()
    {

    }

   

    void createMenu()
    {

    }

    void onMenuClick()//参数未定
    {

    }

    void insertUnit()
    {

    }

    void insertAbility()
    {

    }

    void insertMenu()
    {

    }

    /// <summary>
    /// 升级对应矿山的科技
    /// </summary>
    /// <param name="name">科技的名字</param>
    public void Upgrade(string name)
    {
        //mineSelected.upGrade(name);
    }

 
  
  
    void useAbility()
    {

    }

   
    //

    // Use this for initialization
	void Awake () {
        UnityEventCenter.Register<MineSelectEvent>(this,12);
        UnityEventCenter.Register<CreateUnitEvent>(this);
        UnityEventCenter.Register<ScoreEvent>(this);

		ScoreBoard.getInstance ().Init (2);
    }
	
	// Update is called once per frame
	void Update () {
        if (mineSelected != null) tryCancelMine();

    }

    

    void OnDisable()
    {
        UnityEventCenter.UnRegister<MineSelectEvent>(this);
        UnityEventCenter.UnRegister<CreateUnitEvent>(this);
        UnityEventCenter.UnRegister<ScoreEvent>(this);
    }
    /// <summary>
    /// 矿山选择接口
    /// </summary>
    /// <param name="message"></param>
    public void Handle(MineSelectEvent message)
    {
        MineMountain temp =message.getMine();
        if (message.getObject().GetComponent<GameobjBase>().owner != GameobjBase.PLAYER)
        {
            message.setCancel(true);
            return;//不可选择非玩家的矿山
        }
          
        if (temp != null)
            mineSelected = temp;
    }
    
   /// <summary>
   /// 确定单位是否可以按规则创建
   /// </summary>
   /// <param name="des"></param>
   /// <param name="ID"></param>
   /// <returns></returns>
    private bool destinationConfirm(Vector3 des,int ID)
    {
        Ray ray=new Ray(des- new Vector3(0, 1, 0), Vector3.up);
        RaycastHit rh;
        Physics.Raycast(ray, out rh, 1 << Layers.BUILDING | 1 << Layers.RAILWAY);
        if (rh.collider == null)//未点击到建筑或路return
        {
            return false;
        }
        if (ID == IDs.getIDByName(Tags.Character.GATEWORKER))//扳道闸工人
        {
            if (!(rh.collider.tag.Equals(Tags.GATE)))//未点到扳道闸
                return false;
        }
        return true;
     }
    
   

    private void tryCancelMine()
    {
        
        if (Application.platform==RuntimePlatform.WindowsEditor||Application.platform==RuntimePlatform.WindowsPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (Physics.Raycast(UICamera.mainCamera.ScreenPointToRay(Input.mousePosition),20))//检测是否点击到NGUI
                {
                    return;
                }
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rh;
                Physics.Raycast(ray, out rh);
                if (rh.collider == null)
                {
                    return;
                }
                MineMountain mine = rh.collider.gameObject.GetComponent<MineMountain>();
                if (mine ==null)
                {//没点击到矿山
                    mineSelected=null;
                    UnityEventCenter.SendMessage<cancelMountainEvent>(new cancelMountainEvent(null, null));
                }
                
            }
        }
        else
        {
            if (Input.touchCount<=0) {
                return;
            }
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && touch.tapCount >= 2)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit rh;
                Physics.Raycast(ray, out rh);
                if (rh.collider == null)
                {
                    return;
                }
                MineMountain mine = rh.collider.gameObject.GetComponent<MineMountain>();
                if (mine == null)
                {//没点击到矿山
                    mineSelected = null;
                    UnityEventCenter.SendMessage<cancelMountainEvent>(new cancelMountainEvent(null, null));
                }
            }
        }

    }

    public void Handle(CreateUnitEvent message)
    {
        if(destinationConfirm(message.destination,message.ID)==false)
            return;
        if (message.player==GameobjBase.PLAYER)//来自UI的消息
        {
            if (mineSelected == null) return;
            mineSelected.buildUnitByID(message.ID, message.destination);
        }
        else
        {
            message.mine.buildUnitByID(message.ID, message.destination);
        }
        
    }
    /// <summary>
    /// 判断积分消息
    /// </summary>
    /// <param name="message"></param>
    public void Handle(ScoreEvent message)
    {
        int player=message.getPlayer();
        int score=message.getScore();
        GetComponent<MapDescription>().isWin(new playerInformation(player, score));
    }



}
