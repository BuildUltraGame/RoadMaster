using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 道口基类
/// </summary>
public abstract class MetroGate :MonoBehaviour{

    protected OffMeshLink link;//道闸连线集合

    protected List<GameObject> vehilesOnRoad = new List<GameObject>();
    protected List<Transform> allPoint=new List<Transform>();//按顺序存的点信息
    public Texture[] stateTexture;
    /// <summary>
    /// 变换道口连接情况
    /// 
    /// 经过讨论后不需要用户操作,分情况处理:
    /// 当是三岔道口的时候:
    ///                     1.如果工人所在点已经和其他接通,那就只有一种情况,直接转换另一个接口到未连接接口
    ///                     2.如果工人所在点没有联通,则随机将所在点和其他两个点联通
    /// 当四岔道口的时候:
    ///                 只有两种情况,直接转换就可以了
    ///                 
    /// 所以用户并不能显式决定所连接的铁路,于是第二个参数暂时无效
    /// 
    /// </summary>
    /// <param name="v">搬道闸工人的起始点</param>
    /// <param name="linkNum">(暂时无效)需要连接的铁路的序号,从起始点顺时针算第几个铁路</param>
   public abstract void GateChange(Vector3 v,int linkNum=1);

   public void Start()
   {
       link = GetComponent<OffMeshLink>();

       Transform[] ts = GetComponentsInChildren<Transform>();


       foreach (Transform t in ts)
       {
           if (t.gameObject.tag == Tags.RAILWAY_POINT)
           {
               allPoint.Add(t);
           }
       }
       regiseterPathPoint();
   }

   /// <summary>
   /// 摧毁本岔路的车
   /// </summary>
   protected void destroyVehilesOnRoad()
   {
       foreach (GameObject obj in vehilesOnRoad)
       {
           Destroy(obj);
       }
   }


   void OnTriggerEnter(Collider other)
   {
       if (other.gameObject.layer == Layers.VEHICLE)
       {
           addVehileOnRoad(other.gameObject);
       }
   }

   void OnTriggerExit(Collider other)
   {
       if (other.gameObject.layer == Layers.VEHICLE)
       {
           removeVehileOnRoad(other.gameObject);
       }

   }

   public void addVehileOnRoad(GameObject vehile)
   {
       vehilesOnRoad.Add(vehile);
   }

   public void removeVehileOnRoad(GameObject vehile)
   {
       vehilesOnRoad.Remove(vehile);
   }



   public static int FromWhichPoint(List<Transform> vs, Vector3 v)
   {


       float minDistance = 99;
       int minNum = 0;
       for (int i = 0; i < vs.Count; i++)
       {
           float tempDist = Vector3.Distance(v, vs[i].position);
           if (tempDist < minDistance)
           {
               minDistance = tempDist;
               minNum = i;
           }
       }

       return minNum;
   }

  
   private void regiseterPathPoint()
   {
       foreach(Transform t in allPoint){
           PathDataCenter.registerPathPoint(t.transform.position);
       }
   }

   private void unRegiseterPathPoint()
   {
       foreach (Transform t in allPoint)
       {
           PathDataCenter.unRegisterPathPoint(t.transform.position);
       }
   }

   void OnDisable()
   {
       unRegiseterPathPoint();
   }


}
