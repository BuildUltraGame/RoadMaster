using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;
/// <summary>
/// 刷怪笼脚本
/// 注意:挂载本脚本的时候,请务必也挂载上游戏基础脚本,因为脚本带了游戏对象所属
/// 生成所有的对象的控制权都和本刷怪笼带的游戏基础脚本指定的控制方所用
/// 
/// </summary>
public class Spawner : MonoBehaviour
{
   
    public int cost = 0;//造价
    public GameObject spawnUnit;//建造的单位prefab
    public float CD = 0;//建造一个单位后需要等待的时间

    public float buildTime = 0;//建造一个单位需要的时间(和CD有区别)
    
    [HideInInspector]public float coolDown = 0;//建造下一个单位还需要等待的时间(CD)

    private Vector3 targetPoint=Vector3.zero;//设定建造出来的单位目的地
    private GameObject targetObj;//设置建造出来的单位的跟踪目标

    private bool isCharacter = false;

    protected TimerController.Timer CDtimer;
    private bool canBuildFlag = true;

    protected GameobjBase gBase;

    /// <summary>
    /// 设置是否能建造
    /// </summary>
    /// <param name="flag"></param>
    public void setBuildFlag(bool flag)
    {
        canBuildFlag = flag;
    }


    public int getCost()
    {
        return cost;
    }

    public float getCD()
    {
        return coolDown;
    }

    public bool canBuild()
    {
        return canBuildFlag;
    }

    public void setTarget(Vector3 v)
    {
        targetPoint = v;
    }
    /// <summary>
    /// 设置生成器产生的游戏对象的行走目的地
    /// PS:仅对人有效
    /// </summary>
    /// <param name="obj">目标</param>
    public void setTarget(GameObject obj)
    {
        targetObj = obj;
    }
    /// <summary>
    /// 建造函数,直接生成一个spawnUnit指定的对象在生成器所在位置
    /// </summary>
    /// <returns>是否生成成功</returns>
    public bool build()
    {
        if(!canBuild()){
            return false;
        }
        canBuildFlag = false;

        Invoke("buildNow", buildTime);//延迟建造所需要的时间(非CD)

        return true;

    }


    protected void buildNow()
    {
        //生成游戏单位代码

        GameObject obj = GameObject.Instantiate<GameObject>(spawnUnit,transform.position,Quaternion.identity);

        obj.gameObject.GetComponent<GameobjBase>().setOwner(gBase.getOwner());//设置控制权

        if (obj.GetComponent<Stone>()!=null)
        {
            obj.GetComponent<Stone>().setSpawner(gameObject.transform.parent.gameObject);
        }

        if (isCharacter)
        {
            //只对人有效,对车无效
            Roadmovable roadmovable = obj.gameObject.GetComponent<Roadmovable>();
            if (targetObj != null)
            {
                roadmovable.setDestination(targetObj);//设置跟踪目标,优先
            }
            else if (targetPoint != Vector3.zero)
            {
                roadmovable.setDestination(targetPoint);//设置目的地
            }


        }
        obj.GetComponent<Collider>().enabled = true;
        EventAggregator.SendMessage<SpawnEvent>(new SpawnEvent(gameObject, obj));//发送生成单位事件
        startTimer();
       

      
    }

    private void startTimer()
    {

        CDtimer.Start();
        
    }

    
	// Use this for initialization
    public	void Start () {
        gBase=GetComponent<GameobjBase>();
        if(gBase==null){
            throw new System.Exception("生成器忘了加基础游戏脚本了亲");
        }

        if (spawnUnit.GetComponent<Roadmovable>() != null)
        {
            isCharacter = true;
        }
        else {
            isCharacter = false;
        }

        CDtimer = TimerController.getInstance().NewTimer(CD, false, delegate(float time) {
            float newcool = CD - time;
            coolDown = newcool;
            
        }, delegate()
        {
            setBuildFlag(true);
        });
	}
	
	
 
}
