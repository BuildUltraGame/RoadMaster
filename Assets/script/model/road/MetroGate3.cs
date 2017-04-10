using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private List<Vector3> linkRoad = new List<Vector3>();
    private Vector3 blockRoad;//未连接的点

    private Vector3[] allPoint;//按顺序存的点信息.除了中心点



    private Vector3 center;//中心点


	
	void Start () {

        base.Start();

        initPointInfo();

	}
    /// <summary>
    /// 把点的一些信息存下来
    /// </summary>
    private void initPointInfo()
    {
        center = pointList[0];

        allPoint = new Vector3[3];
        allPoint[0] = pointList[1];
        allPoint[1] = pointList[2];
        allPoint[2] = pointList[3];

        linkRoad.Add(allPoint[0]);
        linkRoad.Add(center);
        linkRoad.Add(allPoint[1]);

        blockRoad = allPoint[2];
    }
	
	
	void Update () {

      
		
	}

    override protected void OnObjEnter(Collider collider)
    {
        int pointNum = FromWhichPoint(allPoint, collider.transform.position);

       if (blockRoad == allPoint[pointNum])
       {
           print("不能通过");
           
           Railwaymovable obj = collider.gameObject.GetComponent<Railwaymovable>();
          // obj.addRoadPoint(allPoint[pointNum]);
         //  obj.addRoadPoint(collider.transform.position);
           obj.Back();


        }
        else
        {
            print("可以通过");
            //可以通过
            Railwaymovable obj = collider.gameObject.GetComponent<Railwaymovable>();
            if(allPoint[pointNum]==linkRoad[0]){
                obj.addRoadsPoint(linkRoad);
            }
            else
            {
                List<Vector3> l = new List<Vector3>(linkRoad);
                l.Reverse();
                obj.addRoadsPoint(l);
            }


        }
        

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
            linkRoad.Add(center);
            linkRoad.Add(Random.Range(0,2)>0.5?allPoint[(minNum+1)%3]:allPoint[(minNum+2)%3]);//三目装逼法

            
        }
        else {
            //如果工人来的位置原来已经和某铁路连接,那就是情况1
            //直接转换另一个接口到未连接接口
            linkRoad.Clear();
            linkRoad.Add(allPoint[minNum]);
            linkRoad.Add(center);
            linkRoad.Add(blockRoad);
        }
       

        
        updateblockRoad();
        destroyVehilesOnRoad();//摧毁当前在岔道口的车辆
    }

    /// <summary>
    /// 更新堵塞铁路信息
    /// </summary>
    private void updateblockRoad()
    {
        for (int i = 0; i < allPoint.Length;i++ )
        {
            if(!linkRoad.Contains(allPoint[i])){
                blockRoad = allPoint[i];
            }
        }
    }

    

}
