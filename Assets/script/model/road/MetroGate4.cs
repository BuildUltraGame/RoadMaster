using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroGate4 : MetroGate
{

    private int state = 0;

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
        edge[state].active = false;
        edge[state+1].active = true;
        edge[state+2].active = false;
        edge[(state + 3)%4].active = true;
        state = 1 - state;
    }



    public override void GateChangeAbs(Vector3 v, int linkNum)
    {
       setState();
       destroyVehilesOnRoad();//摧毁当前在岔道口的车辆
    }


 
}
