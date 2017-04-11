using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 三道口
/// 
/// prefab里面有四个point:
/// 顺序从上到下是:
/// 
/// 中心点
/// 点1
/// 点2=点1顺时针下一个点
/// 点3=点2顺时针下一个点
/// 
/// </summary>
public class MetroGate3 : MetroGate {
    

    private List<Transform> linkRoad = new List<Transform>();
    private Transform blockRoad;//未连接的点



	
	void Start () {
        base.Start();
        initPointInfo();

	}



    /// <summary>
    /// 把点的一些信息存下来
    /// </summary>
    private void initPointInfo()
    {


        linkRoad.Add(allPoint[0]);
        linkRoad.Add(allPoint[1]);

        blockRoad = allPoint[2];

        link.startTransform = linkRoad[0];
        link.endTransform = linkRoad[1];

    }
	


    public override void GateChange(Vector3 v, int linkNum)
    {
        if(linkNum<=0||linkNum%3==0){
            return;
        }

        
        int minNum=FromWhichPoint(allPoint,v);//来的那个点的位置

        if (allPoint[minNum].Equals(blockRoad))
        {
            //如果工人来的位置原来就没有连接,那就是情况2
            //随机将所在点和其他两个点联通
            linkRoad.Clear();
            linkRoad.Add(allPoint[minNum]);
            linkRoad.Add(Random.Range(0,2)>0.5?allPoint[(minNum+1)%3]:allPoint[(minNum+2)%3]);//三目装逼法

            
        }
        else {
            //如果工人来的位置原来已经和某铁路连接,那就是情况1
            //直接转换另一个接口到未连接接口
            linkRoad.Clear();
            linkRoad.Add(allPoint[minNum]);
            linkRoad.Add(blockRoad);
        }

        link.startTransform = linkRoad[0];
        link.endTransform = linkRoad[1];

        updateblockRoad();
        destroyVehilesOnRoad();//摧毁当前在岔道口的车辆
    }

    /// <summary>
    /// 更新堵塞铁路信息
    /// </summary>
    private void updateblockRoad()
    {
        for (int i = 0; i < allPoint.Count;i++ )
        {
            if(!linkRoad.Contains(allPoint[i])){
                blockRoad = allPoint[i];
            }
        }

    }

    

}
