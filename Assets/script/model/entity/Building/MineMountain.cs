using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;
/// <summary>
/// 矿山主体
///  通过Inspector面板来设定是主矿还是分矿
///  Attention：不要忘记挂载矿山的碰撞处理脚本！
/// </summary>
public class MineMountain : MonoBehaviour {

    public int initMine = 100;
    public int totalMine = 100;
    public float increaseRate = 0.0f;//每秒增长速率
    public float increaseFlashTime = 1.0f;//金币更新频率
    public bool isSmallMine = false;//是否是分矿
    public GameObject Lighthouse; //这个是我用来测试的，指定一个物体，这样比较好找到位置
    public float currentScore; //当前分数

    private int owner;

    public List<Spawner> SpawnerUnitList = new List<Spawner>();
    private Dictionary<string, Spawner> SpawnerUnitDict = new Dictionary<string, Spawner>();

    public Dictionary<int, Spawner> SpawnerIDDict = new Dictionary<int, Spawner>();//等待ID与Spawner的对应关系实现之后再填入

    void Awake()
    {
        InitSpawnerDict();
        totalMine = initMine;
        InitOwner();
    }

    void Start () {
        InvokeRepeating("increaseMine",0.0f,increaseFlashTime);
	}
	

	void Update () {
        //testBuild();
        Debug.Log("mine" + totalMine);
    }

    /// <summary>
    /// 根据ID来生产单位，原来的接口暂时还没有删
    /// 实现方式跟之前一样，因为我在spawner上面并没有找到ID属性，所以暂时没有修改接口内部的实现方式
    /// </summary>
    /// <param name="id"></param>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    public bool buildUnitByID(int id,Vector3 targetPos)
    {
        
        string name = IDs.getNameByID(id);
        Spawner targetSpawner = null;
        if (SpawnerUnitDict.ContainsKey(name) == true)
        {
            targetSpawner = SpawnerUnitDict[name];
        }
        else
        {
            Debug.Log("没有查找到对应的spawner");
            return false;
        }
        targetSpawner.setTarget(targetPos);
        if (totalMine >= targetSpawner.getCost())
        {
            if (targetSpawner.build())
            {
                totalMine -= targetSpawner.getCost();
            }

            else
                Debug.Log("行走时出错");
                return false;
        }
        return false;
    }

    /// <summary>
    /// 传递名称来调用其上的生成器
    /// 必须要先在scene里面有对应的spawner才可以
    /// </summary>
    /// <param name="name"></param>
    /// <param name="buildPos"></param>
    /// <returns></returns>
    public bool buildUnitByName(string name,Vector3 targetPos)
    {
        Spawner targetSpawner = null;
        if (SpawnerUnitDict.ContainsKey(name) == true)
        {
            targetSpawner = SpawnerUnitDict[name];
        }
        else
        {
            return false;
        }
        targetSpawner.setTarget(targetPos);
        if (totalMine >= targetSpawner.getCost())
        {
            if (targetSpawner.build())
            {
                totalMine -= targetSpawner.getCost();
            }
               
            else
                // TODO
                return false;
        }
        return false;
    }

    void increaseMine()
    {
        EventAggregator.SendMessage<MineStateChangeEvent>(new MineStateChangeEvent(this));//发送矿山状态改变事件
        totalMine += (int)(increaseRate * increaseFlashTime);
    }

    /// <summary>
    /// 初始化，将孵化器list存入dict
    /// </summary>
    void InitSpawnerDict()
    {
        foreach(Spawner spawnerUnit in SpawnerUnitList)
        { 
            SpawnerUnitDict.Add(spawnerUnit.getName(), spawnerUnit);
        }
    }

    void InitOwner()
    {
        GameobjBase gameObjectBaseGo = this.GetComponent<GameobjBase>();
        owner = gameObjectBaseGo.getOwner();
        currentScore = 0;
    }

    public void testBuild()
    {
        Vector3 pos = Lighthouse.transform.position;
        buildUnitByName("基础运输矿车", pos);
    }

    public void getMineFromCar(int count)
    {
        totalMine += count;
    }

    public bool changeOwner(int targetOwner)
    {
        if (!isSmallMine)
            return false;
        owner = targetOwner;
        Debug.Log("owner changed = to" + owner);
        return true;
    }

    public void changeScore(float num)
    {
        currentScore += num;
    }
}
