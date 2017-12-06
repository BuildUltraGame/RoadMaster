using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 四道闸口
/// </summary>
public class MetroGate4 : MetroGate
{

    private int state = 0;

    //栏杆
    private List<GameObject> roadBlocks=new List<GameObject>();
    
    //阻塞点
    private List<RoadPoint> blockPoints=new List<RoadPoint>();

    private List<RoadPoint> edge = new List<RoadPoint>();
    private RoadPoint centerPoint;

    void Start()
    {
        base.Start();

        initPointInfo();
      

    }



    /// <summary>
    /// 把点的一些信息存下来
    /// </summary>
    private void initPointInfo()
    {
        foreach (RoadPoint p in allPoint)
        {
            if (p.gameObject.name == "c")
            {
                centerPoint = p;
            }
            else
            {
                edge.Add(p);
            }

        }

        setState();
        
    }


    private void setState()
    {
        blockPoints.Clear();
        blockPoints.Add(edge[state]);
        blockPoints.Add(edge[state + 2]);

        edge[state].active = false;
        edge[state+1].active = true;
        edge[state+2].active = false;
        edge[(state + 3)%4].active = true;
        state = 1 - state;
        updateBlock();
    }



    public override void GateChangeAbs(Vector3 v, int linkNum)
    {
       setState();
       
       destroyVehilesOnRoad();//摧毁当前在岔道口的车辆
    }

    /// <summary>
    /// 更新障碍显示
    /// </summary>
    private void updateBlock()
    {
        if (roadBlocks != null&& roadBlocks.Count==2)
        {
            Destroy(roadBlocks[0], 0.2f);
            Destroy(roadBlocks[1], 0.2f);
            roadBlocks.Clear();
        }
        foreach (RoadPoint p in blockPoints) {
            roadBlocks.Add(GameObject.Instantiate<GameObject>(roadBlockPrefab, p.transform.position + Vector3.up * 4, Quaternion.Inverse(p.transform.rotation)));
        }
        

    }




}
