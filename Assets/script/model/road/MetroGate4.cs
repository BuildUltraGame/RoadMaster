using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroGate4 : MetroGate
{

    private int state = 0;


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


        link.startTransform = allPoint[0];
        link.endTransform = allPoint[2];

    }



    public override void GateChange(Vector3 v, int linkNum)
    {
  
        if (state==0)
        {
            //如果工人来的位置原来就没有连接,那就是情况2
            //随机将所在点和其他两个点联通
            link.startTransform = allPoint[0];
            link.endTransform = allPoint[2];
            state = 1;
        }
        else if(state==1)
        {
            //如果工人来的位置原来已经和某铁路连接,那就是情况1
            //直接转换另一个接口到未连接接口
            link.startTransform = allPoint[1];
            link.endTransform = allPoint[3];

            state = 0;
        }


  
        destroyVehilesOnRoad();//摧毁当前在岔道口的车辆
    }


 
}
