using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 矿山主体
///  通过Inspector面板来设定是主矿还是分矿
///  Attention：不要忘记挂载矿山的碰撞处理脚本！
/// </summary>
public class MineMountain : MonoBehaviour {

    public int initMine = 100;
    private int totalMine = 100;
    public float increaseRate = 0.0f;//每秒增长速率
    public float increaseFlashTime = 1.0f;//
    public bool isSmallMine = false;
    public GameObject Lighthouse; //这个是我用来测试的，指定一个物体，这样比较好找到位置

    private int owner;

    public List<Spawner> SpawnerUnitList = new List<Spawner>();
    private Dictionary<string, Spawner> SpawnerUnitDict = new Dictionary<string, Spawner>();


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
        testBuild();
        Debug.Log("mine" + totalMine);

    }

    /// <summary>
    /// 传递名称来调用其上的生成器
    /// 必须要先在scene里面有对应的spawner才可以
    /// </summary>
    /// <param name="name"></param>
    /// <param name="buildPos"></param>
    /// <returns></returns>
    public bool buildUnitByName(string name,Vector3 buildPos)
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
        targetSpawner.setTarget(buildPos);
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
    }

    void testBuild()
    {
        Vector3 pos = Lighthouse.transform.position;
        buildUnitByName("基础运输矿车", pos);
    }

    public void getMineFromCar(int count)
    {
        totalMine += count;
    }

}
