using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 刷怪笼脚本
/// 
/// </summary>
public class Spawner : MonoBehaviour
{
    public string name = "null";//生成单位名称
    protected int cost = 0;//造价
    
    protected float coolDown = 0;//建造下一个单位还需要等待的时间(CD)
    public float CD = 0;//建造一个单位所需要的时间

    public GameObject spawnUnit;//建造的单位prefab

    private Vector3 target;//设定建造出来的单位目的地

    private float lastBuildTime=-1;//上次建造的时间,用于CD计算
    private bool canBuildFlag = true;

    public string getName()
    {
        return name;
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
        target = v;
    }

    public void setTarget(GameObject obj)
    {
        target = obj.transform.position;
    }

    public bool build()
    {
        if(!canBuild()){
            return false;
        }

        startTimer();

        //生成游戏单位代码

        GameObject obj=GameObject.Instantiate<GameObject>(spawnUnit);
        obj.transform.position = transform.position;


        canBuildFlag = false;

        return true;

    }

    private void startTimer()
    {
   
            lastBuildTime = Time.time;
            coolDown=CD-(Time.time-lastBuildTime);
        
    }

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKey(KeyCode.W)){
            build();
        }


        if(canBuildFlag==false){
            coolDown = CD - (Time.time - lastBuildTime);

        }
        if(coolDown<=0){
            canBuildFlag = true;
        }


	}
}
