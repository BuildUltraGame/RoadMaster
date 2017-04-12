using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;
using UnityEngine.AI;

/// <summary>
/// 例子里面没有展示怎么去自己调用方法去造兵啊啥的,因为没写好,写的时候你可以先留空哪部分,写剩下
/// </summary>
public class BaseAI : MonoBehaviour, 
    IListener<DestroyEvent>, IListener<SpawnEvent>, IListener<ScoreEvent>,IListener<MineMoutainSpawnerEvent>
{

    private NavMeshAgent nav;//一个神奇的东西,它可以代替车去做个模拟,看看能不能到达哪里,或者也可以代替人

	// Use this for initialization
	void Awake () {//这里不能改动,一定只能在Awake函数里
		EventAggregator.Register<DestroyEvent>(this);
        EventAggregator.Register<SpawnEvent>(this);
        EventAggregator.Register<ScoreEvent>(this);
        EventAggregator.Register<MineMoutainSpawnerEvent>(this);
        nav=GetComponent<NavMeshAgent>();
	}


    private bool canReach(Vector3 v,int type)
    {
        if(type==1){
            //我们假设这个是车

            nav.areaMask = NavMesh.GetAreaFromName("railway");//设置下我们的导航只能走铁路
            nav.transform.position = Vector3.zero;//这个是设置下导航起始点,按道理一般应该是你的某个矿山,
            NavMeshPath path=new NavMeshPath();
            nav.CalculatePath(v,path);//这里path会返回给你

            if(path.status==NavMeshPathStatus.PathComplete){//路径完整,证明直接可达

            }

            if (path.status == NavMeshPathStatus.PathPartial)
            {//有部分路径,证明到中间断了
               Vector3 vv= path.corners[path.corners.Length - 1];//这个按道理就是最后断点的位置

            }

            if (path.status == NavMeshPathStatus.PathInvalid)
            {//路径完全无效,我也不知道什么时候会出现这个情况,好像如果你起始点不在可行走的路上的话,会直接返回这个

            }

        }


        nav.areaMask = NavMesh.GetAreaFromName("road");//设置下我们的导航只能走人行路
        return false;
    }
	
	// Update is called once per frame
	void Update () {

        //感知是否可以到达某个地点,比如


		
	}

    public void Handle(DestroyEvent message)
    {
       //游戏物体被销毁
        //暂时设定是不知道谁销毁了谁,这个可以改
        Debug.Log(message.getObject());//获取被破坏的物体


    }

    public void Handle(SpawnEvent message)
    {
        //游戏单位建造事件
        Debug.Log(message.getSubject());//获取建造的生成器
        Debug.Log(message.getObject());//获取建造出来的单位
        //更多详细信息的话,比如

        message.hasNavData();//对象是否是人,如果是人就会有目的地距离啥的
        message.getDistance();
        message.getSpeed();

        //更多的信息的话,要自己去获取,如

        BaseTramcarGuard b=   message.getObject().GetComponent<BaseTramcarGuard>();//我们试图去获取下这个组件,如果有这个组件,那就是基础运输矿车.
        if(b!=null){
            b.getGuardLevel();//我们就可以获取到他的防御等级,(虽然并没有什么卵用)
        }

    }

    public void Handle(ScoreEvent message)
    {//分数变化
        message.getPlayer();//获取这个分数是哪个玩家的
        if(message.getPlayer()==GameobjBase.PLAYER){
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
        EventAggregator.SendMessage<UICreateUnitEvent>(new UICreateUnitEvent(1,new Vector3(0,0,0),message.getMineMountaion()));
        
    }
}
