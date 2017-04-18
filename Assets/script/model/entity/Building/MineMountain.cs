using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;
/// <summary>
/// 矿山主体
///  通过Inspector面板来设定是主矿还是分矿
///  Attention：不要忘记挂载矿山的碰撞处理脚本！
/// </summary>
public class MineMountain : MonoBehaviour
{

    public int initMine = 100;
    public int totalMine = 100;
    public float increaseRate = 1.0f;//每秒增长速率
    public float increaseFlashTime = 1.0f;//金币更新频率
    public bool isSmallMine = false;//是否是分矿
    public GameObject Lighthouse; //这个是我用来测试的，指定一个物体，这样比较好找到位置

    public List<Spawner> SpawnerUnitList = new List<Spawner>();//old
    private Dictionary<string, Spawner> SpawnerUnitDict = new Dictionary<string, Spawner>();//old

    void Awake()
    {
        InitSpawnerDict();
        totalMine = initMine;

    }

    void Start()
    {
        EventAggregator.SendMessage<MineMoutainSpawnerEvent>(new MineMoutainSpawnerEvent(this));
        InvokeRepeating("increaseMine", 0.0f, increaseFlashTime);
        PathDataCenter.registerPathPoint(transform.position);
        
    }


    void OnDisable()
    {
        PathDataCenter.unRegisterPathPoint(transform.position);
    }


    void Update()
    {
        //testBuild();
        //Debug.Log("mine" + totalMine);
    }

    /// <summary>
    /// 根据ID来生产单位，原来的接口暂时还没有删
    /// 实现方式跟之前一样，因为我在spawner上面并没有找到ID属性，所以暂时没有修改接口内部的实现方式
    /// </summary>
    /// <param name="id"></param>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    public bool buildUnitByID(int id, Vector3 targetPos)
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
            {
                Debug.Log("无法建造");
                return false;
            }

            
        }
        return false;
    }

    public List<Spawner> getSpawnerList()
    {
        return SpawnerUnitList;
    }

    /// <summary>
    /// 传递名称来调用其上的生成器
    /// 必须要先在scene里面有对应的spawner才可以
    /// </summary>
    /// <param name="name"></param>
    /// <param name="buildPos"></param>
    /// <returns></returns>
    public bool buildUnitByName(string name, Vector3 targetPos)
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
        foreach (Spawner spawnerUnit in SpawnerUnitList)
        {
            SpawnerUnitDict.Add(spawnerUnit.GetComponent<GameobjBase>().game_name_en, spawnerUnit);
        }
    }




    public void getMineFromCar(int count)
    {
        totalMine += count;
    }

    void OnMouseDown()
    {
        EventAggregator.SendMessage<MineSelectEvent>(new MineSelectEvent(gameObject));//矿山被选择事件
    }

    public void Handle(SpawnEvent message)
    {
        GameObject targetObj = message.getSubject();
        if (targetObj.GetComponents<DestoryMe>() == null)
        {
            targetObj.AddComponent<DestoryMe>();

        }
        else
        {
            Debug.Log("不可能啊");
        }

    }

}
