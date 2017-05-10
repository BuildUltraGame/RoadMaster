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

	public float aiThinkingTime = 0.1f;

	private RailwayMovable rm;
    private List<RoadPoint> rps=new List<RoadPoint>();

    private static float roadW = 2f;
    private float distance = 0;
    private float speed = 0;
    private int gameID = 0;

    // Use this for initialization
    void Awake()
    {   //这里不能改动,一定只能在Awake函数里
        UnityEventCenter.Register<DestroyEvent>(this);
        UnityEventCenter.Register<SpawnEvent>(this);
        UnityEventCenter.Register<ScoreEvent>(this);
        UnityEventCenter.Register<MineMoutainSpawnerEvent>(this);
		rm = GetComponent<AIDetector>();

        GameObject[] gos = GameObject.FindGameObjectsWithTag(Tags.RAILWAY_POINT);

        foreach (GameObject o in gos)
        {
            rps.Add(o.GetComponent<RoadPoint>());
        }

        init();
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
    private bool canReach(Vector3 startP, Vector3 v)
    {
        int time = 3;
        RoadPoint start = getStartPoint(startP);
        //我们假设这个是车
        rm.fromPoint = start;
        rm.nextPoint = start;
        RoadPoint temp;
        bool flag = false;
        distance = 0;//距离
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
                    distance += Vector3.Distance(rm.fromPoint.transform.position, bs.center);
                    return true;
                }
            }
            distance += Vector3.Distance(rm.fromPoint.gameObject.transform.position, rm.nextPoint.transform.position);
            if (Mathf.Abs(Vector3.Distance(v, rm.nextPoint.transform.position)) < 0.5)
                return true;
            if (!flag)
                time--;
            if (time < 0)
                return false;
        }
    }

    private bool cdAndGoldIsOK(int gameId, int mineID = 0)
	{
		if (mineList.Count == 0)
			return false;
		List<Spawner> templist = mineList [mineID].getSpawnerList ();
		foreach(Spawner i in templist)
		{       
			if (i.spawnUnit.GetComponent<GameobjBase> ().game_ID == gameId) {
				if (i.coolDown <= 0.000001f && i.cost <= mineList[mineID].totalMine)
					return true;
				break;
			}
		}
		return false;
	}
    private float getSpeedById(int id)
    {
        if (mineList.Count == 0)
            return -1;
        List<Spawner> templist = mineList[0].getSpawnerList();
        foreach (Spawner i in templist)
        {
            if (i.spawnUnit.GetComponent<GameobjBase>().game_ID == id)
            {
                if (IDs.getLayerByID(id) == Layers.VEHICLE)
                    return i.spawnUnit.GetComponent<GameobjBase>().GetComponent<RailwayMovable>().speed;
                if (IDs.getLayerByID(id) == Layers.CHARACTER)
                    return i.spawnUnit.GetComponent<GameobjBase>().GetComponent<Roadmovable>().speed;
                break;
            }
        }
        return -1;
    }
    //testAI stage
    /********************************************
             矿山A(-25,-0.4,-1.7)                       矿山B(-24.8,-0.4,38.8)
              |                                          |
              |(15.5,0,13.2)                             |                      (15.6,0,53.9)
              |    (26.2,0,23.1)         (23.6, 0, 53.4) |
             路口a4————————————————路口b3————————————————路口c1    (26.1f, 0, 53.7f)
              |                     |                    |(26.5f, 0, 56.1f)
              |(41.3,0,13.1)        |(41.8,0,33.8)       |                           (41.4,0,53.8)
              |                     |                    |           
    		路口d5—————————————————矿山C—————————————————路口e2    (56.3f, 0, 53.9f)
              |    (56.4,0,25.1)    |   (53.8, 0, 53.6)  |(56.7f, 0, 56.3f)  
              |(68.1,0,13.4)        |                    |                           (68.3,0,54.2)
              |                     |                    |
              └—————————————————— 出口 ——————————————————┘


    *********************************************/
    //  ┌  ┐  └  ┘
    /// <summary>
    /// 路口abcde
    /// 
    /// 路口c： 0 |   1 ┘   2 ┐
	/// 路口e： 0 |   1 ┘   2 ┐
    /// 		-1 正在被操作
    /// </summary>
    private Vector3 mineMountain1Pos = new Vector3(99.3f, 0, 100);
    private Vector3 mineMountain2Pos = new Vector3(99.5f, 0, 140.5f);
    private Vector3 mineMountain3Pos = new Vector3(150.3f, 0, 120.3f);

    private Vector3 subPos = new Vector3(94, 0, 86.8f);
    //private Vector3 endPos = new Vector3(45.5f, 0, 18.4f);//存放终点坐标
    private Vector3 cForkPos = new Vector3(26.1f, 0, 53.7f);//道闸c坐标
    private Vector3 eForkPos = new Vector3(56.3f, 0, 53.9f);//道闸e坐标

    private Vector3 aForkPos = new Vector3(26.2f, 0, 13.1f);//道闸a坐标
    private Vector3 bForkPos = new Vector3(25.9f, 0, 33.8f);//道闸b坐标
    private Vector3 dForkPos = new Vector3(56.2f, 0, 13.1f);//道闸d坐标

    private List<Vector3> forkPosList = new List<Vector3>();

    //对于道闸口的路点（RoadPoint）坐标的偏移量计算方式
    //      p1          
    //      |           0 代表  p1,p3 连通，即  |
    // p2——道闸         1 代表  p1,p2 连通，即  ┘
    //      |           2 代表  p2,p3 连通，即  ┐
    //      p3 
    private Vector3 forkOffsetp1 = new Vector3(-0.3f, 0, 0);
    private Vector3 forkOffsetp2 = new Vector3(-2.5f, 0, -0.3f);
    private Vector3 forkOffsetp3 = new Vector3(0.3f, 0, 2.4f);


    //存放各类变量
    int roadIsOK = 0;
     
    int cForkFlag = 0;
    int eForkFlag = 0;
	int cForkWorker = 0;
	int eForkWorker = 0;

    private const int forkNum = 5;
    private int[] forkFlag = new int[forkNum + 1];
    private int[] forkWorker = new int[forkNum + 1];


    private void init()
    {
        //用于存放可能的初始化
        forkPosList.Add(new Vector3(0,0,0));
        forkPosList.Add(cForkPos);
        forkPosList.Add(eForkPos);
        forkPosList.Add(bForkPos);
        forkPosList.Add(aForkPos);
        forkPosList.Add(dForkPos);


    }
    void analyzeScene()
    {
        //场面分析，逐个判断道闸口状态
        //以后可能的判断： 时间相关的，场上现有单位对道路的影响
        //                时间相关的，场上未出现的单位对道路的影响
        //                可能的组合套路技
        if (canReach(new Vector3(15.6f, 0, 53.9f)+ subPos, new Vector3(41.4f, 0, 53.8f)+ subPos))
            forkFlag[1] = 0;
        else if (canReach(new Vector3(15.6f, 0, 53.9f)+ subPos, new Vector3(23.6f, 0, 53.4f)+ subPos))
            forkFlag[1] = 1;
        else
            forkFlag[1] = 2;

        if (canReach(new Vector3(41.4f, 0, 53.8f)+ subPos, new Vector3(68.3f, 0, 54.2f)+ subPos))
            forkFlag[2] = 0;
        else if (canReach(new Vector3(41.4f, 0, 53.8f)+ subPos, new Vector3(53.8f, 0, 53.6f)+ subPos))
            forkFlag[2] = 1;
        else
            forkFlag[2] = 2;

        if (canReach(new Vector3(26.2f, 0, 23.1f) + subPos, new Vector3(23.6f, 0, 53.4f) + subPos))
            forkFlag[3] = 0;
        else if (canReach(new Vector3(26.2f, 0, 23.1f) + subPos, new Vector3(41.8f, 0, 33.8f) + subPos))
            forkFlag[3] = 1;
        else
            forkFlag[3] = 2;

        if (canReach(new Vector3(41.3f, 0, 13.1f) + subPos, new Vector3(15.5f, 0, 13.2f) + subPos))
            forkFlag[4] = 0;
        else if (canReach(new Vector3(41.3f, 0, 13.1f) + subPos, new Vector3(26.2f, 0, 23.1f) + subPos))
            forkFlag[4] = 1;
        else
            forkFlag[4] = 2;

        if (canReach(new Vector3(68.1f, 0, 13.4f) + subPos, new Vector3(41.3f, 0, 13.1f) + subPos))
            forkFlag[5] = 0;
        else if (canReach(new Vector3(68.1f, 0, 13.4f) + subPos, new Vector3(56.4f, 0, 25.1f) + subPos))
            forkFlag[5] = 1;
        else
            forkFlag[5] = 2;

        for (int i = 4; i <= 5; i++)
            print("路口" + i + "状态 : " + forkFlag[i]);
    }

    void rushAI()
    {
        //场面分析
		if (forkFlag[1] == 0 && forkFlag[2] == 0)
			roadIsOK = 1;
		else
			roadIsOK = 0;
        //根据场面情况开始处理
            //道路不通的情况
		if (forkFlag[1] != 0 && forkWorker[1] == 0)
        {
            //道闸c方向不对，派遣道闸工人
            gameID = IDs.getIDByName(Tags.Character.GATEWORKER);
            if (cdAndGoldIsOK(gameID)) {
                UnityEvent.UnityEventCenter.SendMessage<CreateUnitEvent> (new CreateUnitEvent (gameID, forkPosList[1] + subPos, owner, mineList [0]));
                forkWorker[1] = 1;
                canReach(mineMountain2Pos, cForkPos+ subPos);//为了得到距离
                speed = getSpeedById(gameID);
                TimerController.getInstance ().NewTimer (3f, false, delegate(float time) {}, delegate() {
                    forkWorker[1] = 0;				
				}).Start ();
                print("距离： "+distance+" 速度： "+speed+ "时间： "+ distance / speed);
				return;
			}
        }
		if (forkFlag[2] != 0 && forkWorker[2] == 0)
        {
            //道闸e方向不对，派遣道闸工人
            gameID = IDs.getIDByName(Tags.Character.GATEWORKER);
            if (cdAndGoldIsOK(gameID)) {
                UnityEvent.UnityEventCenter.SendMessage<CreateUnitEvent> (new CreateUnitEvent (gameID, forkPosList[2] + subPos, owner, mineList [0]));
                forkWorker[2] = 1;
                canReach(mineMountain2Pos, eForkPos+ subPos);
                speed = getSpeedById(gameID);
                TimerController.getInstance ().NewTimer (7f, false, delegate(float time) {}, delegate() {
                    forkWorker[2] = 0;
				}).Start ();
				return;
			}
        }

        //道路畅通的情况
        if (roadIsOK == 1)
        {
            //前方道路畅通，该发车了
            gameID = IDs.getIDByName(Tags.Vehicle.OVERWEIGHTTRAMCAR);
            if (cdAndGoldIsOK(gameID))
            {
                UnityEvent.UnityEventCenter.SendMessage<CreateUnitEvent>(new CreateUnitEvent(gameID, cForkPos + subPos, owner, mineList[0]));
                return;
            }

            gameID = IDs.getIDByName(Tags.Vehicle.BASETRAMCAR);
            if (cdAndGoldIsOK(gameID)) {
				UnityEvent.UnityEventCenter.SendMessage<CreateUnitEvent> (new CreateUnitEvent (gameID, cForkPos + subPos, owner, mineList [0]));
				return;
			}
            
            gameID = IDs.getIDByName(Tags.Vehicle.TRAIN);
            if (cdAndGoldIsOK(gameID))
            {
                UnityEvent.UnityEventCenter.SendMessage<CreateUnitEvent>(new CreateUnitEvent(gameID, cForkPos + subPos, owner, mineList[0]));
                return;
            }
        }
    }

    void disturbAI()
    {
        //干扰AI
        //初期先控制玩家无法直达
        if (forkFlag[5] == 0 && forkWorker[5] == 0)
        {
            //道闸d方向不对，派遣道闸工人
            gameID = IDs.getIDByName(Tags.Character.GATEWORKER);
            if (cdAndGoldIsOK(gameID))
            {
                UnityEvent.UnityEventCenter.SendMessage<CreateUnitEvent>(new CreateUnitEvent(gameID, forkPosList[5] + subPos, owner, mineList[0]));
                forkWorker[5] = 1;
                canReach(mineMountain2Pos, eForkPos + subPos);
                speed = getSpeedById(gameID);
                TimerController.getInstance().NewTimer(13f, false, delegate (float time) { }, delegate () {
                    forkWorker[5] = 0;
                }).Start();
                return;
            }
        }
        if (forkFlag[4] == 0 && forkWorker[4] == 0)
        {
            //道闸c方向不对，派遣道闸工人
            gameID = IDs.getIDByName(Tags.Character.GATEWORKER);
            if (cdAndGoldIsOK(gameID))
            {
                UnityEvent.UnityEventCenter.SendMessage<CreateUnitEvent>(new CreateUnitEvent(gameID, forkPosList[4] + subPos, owner, mineList[0]));
                forkWorker[4] = 1;
                canReach(mineMountain2Pos, cForkPos + subPos);//为了得到距离
                speed = getSpeedById(gameID);
                TimerController.getInstance().NewTimer(8f, false, delegate (float time) { }, delegate () {
                    forkWorker[4] = 0;
                }).Start();
                return;
            }
        }
    }
    

    void otherMineAI()
    {
        //占分矿ai，只选择下面的道路
        if (forkFlag[1] == 0 && forkFlag[2] == 1)
            roadIsOK = 1;
        else
            roadIsOK = 0;

        if (forkFlag[1] != 0 && forkWorker[1] == 0)
        {
            //道闸c方向不对，派遣道闸工人
            gameID = IDs.getIDByName(Tags.Character.GATEWORKER);
            if (cdAndGoldIsOK(gameID))
            {
                UnityEvent.UnityEventCenter.SendMessage<CreateUnitEvent>(new CreateUnitEvent(gameID, forkPosList[1] + subPos, owner, mineList[0]));
                forkWorker[1] = 1;
                canReach(mineMountain2Pos, cForkPos + subPos);//为了得到距离
                speed = getSpeedById(gameID);
                TimerController.getInstance().NewTimer(3f, false, delegate (float time) { }, delegate () {
                    forkWorker[1] = 0;
                }).Start();
                print("距离： " + distance + " 速度： " + speed + "时间： " + distance / speed);
                return;
            }
        }
        if (forkFlag[2] != 1 && forkWorker[2] == 0)
        {
            //道闸e方向不对，派遣道闸工人
            gameID = IDs.getIDByName(Tags.Character.GATEWORKER);
            if (cdAndGoldIsOK(gameID))
            {
                UnityEvent.UnityEventCenter.SendMessage<CreateUnitEvent>(new CreateUnitEvent(gameID, forkPosList[2] + subPos, owner, mineList[0]));
                forkWorker[2] = 1;
                canReach(mineMountain2Pos, eForkPos + subPos);
                speed = getSpeedById(gameID);
                TimerController.getInstance().NewTimer(7f, false, delegate (float time) { }, delegate () {
                    forkWorker[2] = 0;
                }).Start();
                return;
            }
        }

        //下方道路通畅，派遣占矿车
        if (roadIsOK == 1)
        {
            gameID = IDs.getIDByName(Tags.Vehicle.EXPLORATIONTRAMCAR);
            if (cdAndGoldIsOK(gameID))
            {
                UnityEvent.UnityEventCenter.SendMessage<CreateUnitEvent>(new CreateUnitEvent(gameID, cForkPos + subPos, owner, mineList[0]));
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
        if (airelay == 1) {
			if (mineList.Count != 0)
            {
                analyzeScene();
                /*if (mineList.Count == 1)
                    otherMineAI();
                else if (mineList.Count == 2)
                    rushAI();
                else
                    rushAI();//*/
                rushAI();
                disturbAI();
            }
			airelay = 0;
			TimerController.getInstance().NewTimer(aiThinkingTime, false, delegate(float time){}, delegate()
				{
					airelay = 1;
				}).Start();
		}
		//*/

        //UnityEvent.UnityEventCenter.SendMessage<CreateUnitEvent>(new CreateUnitEvent(IDs.getIDByName(Tags.Character.GATEWORKER), forkPosList[5] + subPos, owner, mineList[0]));


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
        characterList.Remove(message.getObject());
        vehicleList.Remove(message.getObject());

    }


    public void Handle(SpawnEvent message)
    {
        //游戏单位建造事件
        //Debug.Log(message.getObject());//获取建造出来的单位
        //更多详细信息的话,比如
        if (message.hasRoadEnd())//对象是否是人,如果是人就会有目的地距离啥的
            characterList.Add(message.getObject());
        else
            vehicleList.Add(message.getObject());
        message.getDistance(delegate(float d) {
            print("距离 ： "+d);
        });
        if (message.hasRoadEnd() && message.getObjectOwner() != owner){
            //if (IDs.getIDByName(Tags.Character.GATEWORKER) == message.getObjectID()) {
                Vector3 tempv = message.getObject().GetComponent<Roadmovable>().getTargetPosition();
                print("坐标: "+ tempv);
                if(tempv.z > 135)
                {
                    gameID = IDs.getIDByName(Tags.Character.GUNNER);
                    if (cdAndGoldIsOK(gameID))
                    {
                        UnityEvent.UnityEventCenter.SendMessage<CreateUnitEvent>(new CreateUnitEvent(gameID, new Vector3(15.5f, 0, 13.2f) + subPos, owner, mineList[0]));
                  //  }
                }
            }
        }


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
        //UnityEvent.UnityEventCenter.SendMessage<CreateUnitEvent>(new CreateUnitEvent(1, new Vector3(0, 0, 0), message.getMineMountaion()));
        if (message.getSubjectOwner() == owner)
            mineList.Add(message.getMineMountaion());



    }
}
