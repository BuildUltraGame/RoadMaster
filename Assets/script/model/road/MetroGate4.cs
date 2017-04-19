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


      //  link.startTransform = allPoint[0];
     //   link.endTransform = allPoint[2];

    }



    public override void GateChange(Vector3 v, int linkNum)
    {
  
        if (state==0)
        {
           
      //      link.startTransform = allPoint[1];
      //      link.endTransform = allPoint[3];
            state = 1;
        }
        else if(state==1)
        {
            
     //       link.startTransform = allPoint[0];
     //       link.endTransform = allPoint[2];

            state = 0;
        }

       
  
        destroyVehilesOnRoad();//摧毁当前在岔道口的车辆
    }


 
}
