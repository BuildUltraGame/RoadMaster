using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;
//基础的游戏进程控制器
public class baseController : MonoBehaviour,IListener<createUnit.unitEvent> ,IListener<MineSelectEvent>,IListener<UICreateUnitEvent>{

    //int[] mineList;//矿山列表
    MineMountain mineSelected;//当前选中的矿山，未选中则为null
    
    ////touchController部分
    private bool DEBUG = true;//调试用,因为调试的时候无法触屏,直接采用了鼠标点击的方式去检测

    //private Queue<RequestSelectEvent> reqQueue = new Queue<RequestSelectEvent>();//使用队列储存消息

    bool unitToBuild;//正在等待用户选择目标位置的标志
    int idToBuild;//正在等待用户选择目标位置的单位
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

    /// <summary>
    /// 训练无接口的单位的接口函数
    /// </summary>
    /// <param name="name">单位的名称</param>
    public void createUnitNoDirection(int id)
    {
        Vector3 v = new Vector3(0,0,0);
        mineSelected.buildUnitByID(id,v);
    }
    
    
    /// <summary>
    /// 训练有目的地的单位的接口函数
    /// </summary>
    /// <param name="name">建造单位名称</param>
    /// <param name="buildPos">单位指定的位置</param>

    public void createUnitWithDirection(int id)
    {
        unitToBuild = true;
        EventAggregator.SendMessage<waitClickEvent>(new waitClickEvent(null,null));
        idToBuild = id;       
    }

    void useAbility()
    {

    }

    public virtual void isWin()
    {


    }
    //

    // Use this for initialization
	void Start () {
        EventAggregator.Register<MineSelectEvent>(this);
        EventAggregator.Register<createUnit.unitEvent>(this);
        EventAggregator.Register<UICreateUnitEvent>(this);
        unitToBuild = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (unitToBuild == true) destinationConfirm();
        else if (mineSelected != null) tryCancelMine();
        
	}

    

    void OnDisable()
    {
        EventAggregator.UnRegister<createUnit.unitEvent>(this);
        EventAggregator.UnRegister<MineSelectEvent>(this);
        EventAggregator.UnRegister<UICreateUnitEvent>(this);
    }
    /// <summary>
    /// 矿山选择接口
    /// </summary>
    /// <param name="message"></param>
    public void Handle(MineSelectEvent message)
    {
        if (unitToBuild == true) return;
        MineMountain temp =message.getMine();
        if (temp != null)
            mineSelected = temp;
    }
    /// <summary>
    /// todo :单位点击接口
    /// </summary>
    /// <param name="message"></param>
    public void Handle(createUnit.unitEvent message)
    {
        if (unitToBuild == true) return;//若等待中则直接屏蔽该次建造
        if (mineSelected == null) return;//若未选中则直接屏蔽该次建造
        int id = message.unitID;
        int type = IDs.getLayerByID(id);
        if (type == Layers.CHARACTER)
        {
            createUnitWithDirection(id);//有目标（人）
            return;
        }
        createUnitNoDirection(id);///无目标（车辆）
        
    }
    /// <summary>
    /// 带目标单位点击待确认消息
    /// </summary>
    /// <param name="message"></param>

    /// <summary>
    /// 当处于等待点击状态时，执行的过程
    /// </summary>
    private void destinationConfirm()
    {

        if (unitToBuild == true)
        {
            //有请求,需要监听用户点击的事件情况

            if (DEBUG)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit rh;
                    Physics.Raycast(ray, out rh);
                    if (rh.collider == null)
                    {
                        return;
                    }
                    if (idToBuild == 1002)//扳道闸工人
                    {
                        if (rh.collider.tag.Equals("railway_point"))//点到扳道闸
                        {
                            mineSelected.buildUnitByID(idToBuild, rh.point);
                            unitToBuild = false;
                            idToBuild = 0;
                            EventAggregator.SendMessage<cancelClickEvent>(new cancelClickEvent(null, null));
                            return;
                        }
                        else
                        {
                            unitToBuild = false;
                            idToBuild = 0;
                            EventAggregator.SendMessage<cancelClickEvent>(new cancelClickEvent(null, null));
                            return;
                        }

                    }
                    int layout = rh.collider.gameObject.layer;
                    if (layout == Layers.ROAD || layout == Layers.RAILWAY)
                    {//如果点击到了路面(包括铁路和人行道)
                        mineSelected.buildUnitByID(idToBuild, rh.point);
                        unitToBuild = false;
                        idToBuild = 0;
                        EventAggregator.SendMessage<cancelClickEvent>(new cancelClickEvent(null, null));
                        return;
                    }
                    else
                    {
                        unitToBuild = false;
                        idToBuild = 0;
                        EventAggregator.SendMessage<cancelClickEvent>(new cancelClickEvent(null, null));
                        return;
                    }
                }
                /*else if (rh.collider.gameObject.layer == Layers.VEHICLE || rh.collider.gameObject.layer == Layers.CHARACTER)
                { //如果点击到了车辆,或者人
                    sendResult(reqQueue.Dequeue(), rh.collider.gameObject);

                }*/
            }
        }
        else
        {
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
                if (idToBuild == 1002)//扳道闸工人
                {
                    if (rh.collider.tag.Equals("railway_point"))//点到扳道闸
                    {
                        mineSelected.buildUnitByID(idToBuild, rh.point);
                        unitToBuild = false;
                        idToBuild = 0;
                        EventAggregator.SendMessage<cancelClickEvent>(new cancelClickEvent(null, null));
                        return;
                    }
                    else
                    {
                        unitToBuild = false;
                        idToBuild = 0;
                        EventAggregator.SendMessage<cancelClickEvent>(new cancelClickEvent(null, null));
                        return;
                    }

                }
                int layout = rh.collider.gameObject.layer;
                if (layout == Layers.ROAD || layout == Layers.RAILWAY)
                {//如果点击到了路面(包括铁路和人行道)
                    mineSelected.buildUnitByID(idToBuild, touch.position);
                    unitToBuild = false;
                    idToBuild = 0;
                    EventAggregator.SendMessage<cancelClickEvent>(new cancelClickEvent(null, null));
                    return;
                }
                else
                {
                    unitToBuild = false;
                    idToBuild = 0;
                    EventAggregator.SendMessage<cancelClickEvent>(new cancelClickEvent(null, null));
                    return;
                }
                /*else if (rh.collider.gameObject.layer == Layers.VEHICLE || rh.collider.gameObject.layer == Layers.CHARACTER)
                { //如果点击到了车辆,或者人
                    sendResult(reqQueue.Dequeue(), rh.collider.gameObject);

                }*/

            }
        }


    }
    private void tryCancelMine()
    {
        if (DEBUG)
        {
            if (Input.GetMouseButtonDown(0))
            {
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
                    EventAggregator.SendMessage<cancelMountainEvent>(new cancelMountainEvent(null, null));
                }
                
            }
        }
        else
        {
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
                    EventAggregator.SendMessage<cancelMountainEvent>(new cancelMountainEvent(null, null));
                }
            }
        }

    }

    public void Handle(UICreateUnitEvent message)
    {
        message.mine.buildUnitByID(message.ID, message.destination);
    }



    /*
        private void sendResult(RequestSelectEvent e, Vector3 p)
        {
            SelectEvent se = e.createSelectEvent();
            se.addSelect(p);
            EventAggregator.SendMessage<SelectEvent>(se);//已经选择完毕,发送选择完毕事件
        }

        private void sendResult(RequestSelectEvent e, GameObject obj)
        {
            SelectEvent se = e.createSelectEvent();
            se.addSelect(obj);
            EventAggregator.SendMessage<SelectEvent>(se);//已经选择完毕,发送选择完毕事件
        }*/

}
