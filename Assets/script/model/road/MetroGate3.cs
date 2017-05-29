using System;
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

   
    private RoadPoint blockPoint;
	private GameObject roadBlock;

	private List<RoadPoint> edge=new List<RoadPoint>();
	private RoadPoint centerPoint;

	
	void Start () {
        
        base.Start();
        initPointInfo();
        
	}



    /// <summary>
    /// 把点的一些信息存下来
    /// </summary>
    private void initPointInfo()
    {

		foreach(RoadPoint p in allPoint){
			if (p.gameObject.name == "c") {
				centerPoint = p;
			} else {
				edge.Add(p);
			}

		}

		updateblockRoad();
    }
	


    public override void GateChangeAbs(Vector3 v, int linkNum)
    {
        if(linkNum<=0||linkNum%3==0){
            return;
        }

        
        int minNum=FromWhichPoint<RoadPoint>(edge,v);//来的那个点的位置

		if (edge [minNum].active) {
			//如果已经激活,证明道闸工人到来的方向是连接好的
			//如果工人来的位置原来已经和某铁路连接,那就是情况1
			//直接转换另一个接口到未连接接口

			foreach (RoadPoint p in edge) {
				if (!p.Equals (edge [minNum])) {
					p.active = !p.active;
					if (!p.active) {
						blockPoint = p;
					}
				}
			}

		} else {
			//如果工人来的位置原来就没有连接,那就是情况2
			//随机将所在点和其他两个点联通

			edge [minNum].active = true;
			if (UnityEngine.Random.Range (0, 2) > 0.5) {
				edge [(minNum + 1) % 3].active = false;
			} else {
				edge [(minNum + 1) % 3].active = false;
			}
		
		}
        updateblockRoad();
        destroyVehilesOnRoad();//摧毁当前在岔道口的车辆
    }

    /// <summary>
    /// 更新堵塞铁路信息
    /// </summary>
    private void updateblockRoad()
    {

		foreach(RoadPoint p in edge){
			if(!p.active){
				blockPoint = p;
			}

		}

        updateBlock();
    }

    private void updateBlock()
    {
		if (roadBlock!=null)
        {
           Destroy(roadBlock,0.2f);
        }
	
		roadBlock = GameObject.Instantiate<GameObject>(roadBlockPrefab, blockPoint.transform.position + Vector3.up * 4, Quaternion.Inverse(blockPoint.transform.rotation));

    } 


    

}
