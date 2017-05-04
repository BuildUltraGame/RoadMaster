using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;
using UnityEngine.AI;

/// <summary>
/// 例子里面没有展示怎么去自己调用方法去造兵啊啥的,因为没写好,写的时候你可以先留空哪部分,写剩下
/// </summary>

[RequireComponent(typeof(AIDetector))]
public class TestAI : MonoBehaviour,
    IListener<DestroyEvent>, IListener<SpawnEvent>, IListener<ScoreEvent>, IListener<MineMoutainSpawnerEvent>
{

    public int owner=GameobjBase.OTHER_PLAYER1;

	public float aiThinkingTime =10f;

	public RoadPoint start1;
	private RailwayMovable rm;

    private static float roadW = 0.1f;
    private List<RoadPoint> rps=new List<RoadPoint>();

    // Use this for initialization
    void Awake()
    {//这里不能改动,一定只能在Awake函数里
        UnityEventCenter.Register<DestroyEvent>(this);
        UnityEventCenter.Register<SpawnEvent>(this);
        UnityEventCenter.Register<ScoreEvent>(this);
        UnityEventCenter.Register<MineMoutainSpawnerEvent>(this);
		rm = GetComponent<AIDetector>();
		rm.fromPoint = start1;
		rm.nextPoint = start1;

        GameObject[] gos = GameObject.FindGameObjectsWithTag(Tags.RAILWAY_POINT);

        foreach (GameObject x in gos)
        {
            rps.Add(x.GetComponent<RoadPoint>());
        }
    }

    private RoadPoint getStartPoint(Vector3 v)
    {
        float d = float.MaxValue;
        RoadPoint rp = null;

        rps.ForEach(x => {
            float temp = Vector3.Distance(v, x.transform.position);
            if (temp < d)
            {
                d = temp;
                rp = x;
            }
        });

        return rp;
    }

    private bool canReach(Vector3 v)
    {
        //我们假设这个是车
        RoadPoint start = getStartPoint(new Vector3(110f,0f,140f));
        rm.fromPoint = start;
        rm.nextPoint = start;
        RoadPoint temp;
        bool flag = false;
        while (true)
        {
            temp = rm.nextPoint;

            rm.nextPoint = rm.nextPoint.getNextPoint(rm, out flag);

            rm.fromPoint = temp;

            int i = (Mathf.FloorToInt(Mathf.Abs(Vector3.Distance(rm.nextPoint.transform.position, rm.fromPoint.transform.position)) / roadW) + 1);
            Vector3 nv = (rm.fromPoint.transform.position - rm.nextPoint.transform.position) / i;
            for (; i >= 0; i--)
            {

                Bounds bs = new Bounds(rm.fromPoint.transform.position - nv * i, new Vector3(roadW, 0.1f, roadW));

                if (bs.Contains(v))
                {
                    return true;
                }
            }


            if (Mathf.Abs(Vector3.Distance(v, rm.nextPoint.transform.position)) < 0.5)
            {
                return true;
            }

            if (!flag)
            {
                return false;
            }
        }
    }

	private bool coolDownIsOK(int gameId)
	{
		if (mineList.Count == 0)
			return false;
		List<Spawner> templist = mineList [0].getSpawnerList ();
		foreach(Spawner i in templist)
		{
			if (i.spawnUnit.GetComponent<GameobjBase> ().game_ID == gameId) {
				if (i.coolDown <= 0.000001f)
					return true;
				break;
			}
		}
		return false;
	}

    //testAI stage
    /********************************************
             矿山A                                      矿山B
              |                                          |
              |                                          |(15.6,0,53.9)
              |                          (25.8,0,43.7)   |
             路口a————————————————路口b—————————————————路口c
              |                     |                    |
              |                     |(41.8,0,33.8)       |(41.4,0,53.8)
              |                     |                    |           
    		路口d—————————————————矿山C——————————————————路口e
              |                     |     (56.4,0,43)    |           
              |                     |                    |(68.3,0,54.2)
              |                     |                    |
              └—————————————————— 出口 ———————————————————┘


    *********************************************/
    //┌  ┐  └  ┘
    /// <summary>
    /// 路口abcde
    /// 
    /// 路口c： 0 |   1 ┘   2 ┐
	/// 路口e： 0 |   1 ┘   2 ┐
    /// 		-1 正在被操作
    /// </summary>

    private Vector3 subPos = new Vector3(94, 0, 86.8f);
    private Vector3 endPos = new Vector3(45.5f, 0, 18.4f);//存放终点坐标
    private Vector3 cForkPos = new Vector3(26.1f, 0, 53.7f);//道闸c坐标
    private Vector3 eForkPos = new Vector3(56.3f, 0, 53.9f);//道闸e坐标

    //存放各类变量
    int roadIsOK = 0;
    int cForkFlag = 0;
    int eForkFlag = 0;
	int cForkWorker = 0;
	int eForkWorker = 0;

    void mainAI()
    {



        //场面分析，逐个判断道闸口状态
        //以后可能的判断： 时间相关的，场上现有单位对道路的影响
        //                时间相关的，场上未出现的单位对道路的影响
        //                可能的组合套路技

    	if (canReach(new Vector3(41.4f, 0, 53.8f) + subPos))
  	     	cForkFlag = 0;
    	else if (canReach(new Vector3(25.8f, 0, 43.7f) + subPos))
    	    cForkFlag = 1;
    	else
   	     	cForkFlag = 2;
		if (canReach (new Vector3(68.3f, 0, 54.2f) + subPos))
			eForkFlag = 0;
		else if (canReach (new Vector3(56.4f, 0, 43f) + subPos))
			eForkFlag = 1;
		else
			eForkFlag = 2;

        //场面分析
		if (cForkFlag == 0 && eForkFlag == 0)
			roadIsOK = 1;
		else
			roadIsOK = 0;

        //*/

        //根据场面情况开始处理


        //道路不通的情况
		if (cForkFlag != 0 && cForkWorker == 0)
        {
            //道闸c方向不对，派遣道闸工人
			if (coolDownIsOK (IDs.getIDByName (Tags.Character.GATEWORKER))) {
                UnityEventCenter.SendMessage<CreateUnitEvent> (new CreateUnitEvent (IDs.getIDByName (Tags.Character.GATEWORKER), cForkPos + subPos, owner, mineList [0]));
				cForkWorker = 1;
				TimerController.getInstance ().NewTimer (3f, false, delegate(float time) {
				}, delegate() {
					cForkWorker = 0;
					print ("time over  cForkFlag:" + cForkFlag + "   cForkWoker:" + cForkWorker);
				}).Start ();
				return;
			}
        }
		if (cForkFlag == 0 && eForkFlag != 0 && eForkWorker == 0)
        {
            //道闸e方向不对，派遣道闸工人
			if (coolDownIsOK (IDs.getIDByName (Tags.Character.GATEWORKER))) {
                UnityEventCenter.SendMessage<CreateUnitEvent> (new CreateUnitEvent (IDs.getIDByName (Tags.Character.GATEWORKER), eForkPos + subPos, owner, mineList [0]));
				eForkWorker = 1;
				TimerController.getInstance ().NewTimer (3f, false, delegate(float time) {
				}, delegate() {
					eForkWorker = 0;
				}).Start ();
				return;
			}
        }

        //道路畅通的情况
        if (roadIsOK == 1)
        {
            //前方道路畅通，该发车了
			if (coolDownIsOK (IDs.getIDByName (Tags.Vehicle.BASETRAMCAR))) {
                UnityEventCenter.SendMessage<CreateUnitEvent> (new CreateUnitEvent (IDs.getIDByName (Tags.Vehicle.BASETRAMCAR), cForkPos + subPos, owner, mineList [0]));
				return;
			}
        }




    }

	//需要链表用于存储场面上所存在的矿山，车，人。

	private List<MineMountain> mineList = new List<MineMountain>();//矿山列表，期望首位储存的是 起始矿山。
	private List<GameObject> characterList = new List<GameObject>();
	private List<GameObject> vehicleList = new List<GameObject>();


	int airelay = 1;
    // Update is called once per frame
    void Update()
    {

        //感知是否可以到达某个地点,比如
		if (airelay == 1) {
			if (mineList.Count != 0)
				mainAI ();
			airelay = 0;
			TimerController.getInstance().NewTimer(aiThinkingTime, false, delegate(float time){}, delegate()
				{
					airelay = 1;
				}).Start();
			
		}
		
		//int man_speed = 7;
        //EventCenter.SendMessage<CreateUnitEvent>(new CreateUnitEvent(IDs.getIDByName(Tags.Character.GATEWORKER), cForkPos+subPos, mineList[0]));	

		//mineList [0].getSpawnerList () [0].CD;//
		//mineList [0].getSpawnerList () [0].coolDown;
		//mineList [0].getSpawnerList () [0].spawnUnit.GetComponent<GameobjBase>().game_ID;

    }





    public void Handle(DestroyEvent message)
    {
        //游戏物体被销毁
        //暂时设定是不知道谁销毁了谁,这个可以改
        //Debug.Log(message.getObject());//获取被破坏的物体
        //characterList.Remove(message.getObject());
        //vehicleList.Remove(message.getObject());

    }


    public void Handle(SpawnEvent message)
    {
        //游戏单位建造事件
        //Debug.Log(message.getObject());//获取建造出来的单位
        //更多详细信息的话,比如

        if (message.hasNavData())//对象是否是人,如果是人就会有目的地距离啥的
            characterList.Add(message.getObject());
        else
            vehicleList.Add(message.getObject());
    }

    public void Handle(ScoreEvent message)
    {//分数变化
        message.getPlayer();//获取这个分数是哪个玩家的
        if (message.getPlayer() == GameobjBase.PLAYER)
        {
            //如果分数是本机玩家的
            message.getScore();

        }

    }

    public void Handle(MineMoutainSpawnerEvent message)
    {//有矿山生成,这里我估计你要做个判断,然后把自己应该控制的矿山存下来

        //这里其实还有问题,首先生成单位的ID这个先不说,到时候会统一,主要是,如果你是人,那么你需要目标地点,
        //然而现在还没有得到目标地点的办法,你不可能随便一个点都可以到,你起码必须是路上的坐标(这个还没有获得方式)
        //不过我们可以这样,前面有生成的单位信息,你可以获取到敌人的单位,直接获取里面的位置信息(这个可以实时获取),
        //这个是目前位置能获取位置的唯一办法
        //EventCenter.SendMessage<CreateUnitEvent>(new CreateUnitEvent(1, new Vector3(0, 0, 0), message.getMineMountaion()));
        if(message.getSubjectOwner() == owner)
            mineList.Add(message.getMineMountaion());



    }
}
