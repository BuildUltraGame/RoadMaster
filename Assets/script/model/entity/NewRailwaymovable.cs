using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewRailwaymovable : MonoBehaviour {

    private List<Vector3> goByPath=new List<Vector3>();
    private List<Vector3> path = null;

	private NavMeshAgent nav;
    private Vector3 destination;



    void Start()
    {
        nav = GetComponent<NavMeshAgent>();

        if(nav==null){
            throw new System.Exception("載具对象必须要有NavMeshAgent组件");
        }

        path = new List<Vector3>(PathDataCenter.pathPoints);//copy一份
        
        destination = transform.position;
    }

    public void setDestination(Vector3 v)
    {
        destination = v;
        nav.SetDestination(destination);
    }

    void Update()
    {
        path = path ?? new List<Vector3>(PathDataCenter.pathPoints);
        
        if (nav.pathStatus == NavMeshPathStatus.PathComplete && Mathf.Abs(nav.remainingDistance - nav.stoppingDistance) <= 0.05)
        {
            findNewRoad();//如果到达目的地,赶紧找下一个地点,对,累死你,不让你停
        }

    }


    /// <summary>
    /// 找新的目的地
    /// </summary>
    private void findNewRoad()
    {
       

        Vector3 nextV =Vector3.zero;

        int minD = int.MaxValue;

        
        foreach(Vector3 v in path){
            NavMeshPath p=new NavMeshPath();
            if (nav.CalculatePath(v,p)&&p.status==0) {
                int d = p.corners.Length;
                if (d < minD)
                {
                    minD = d;
                    nextV = v;
                }
            }
        }

        if (!nextV.Equals(Vector3.zero))
        {
            //不为零,证明找到了未走过的点中最近且可达
            goByPath.Add(nextV);
            path.Remove(nextV);
            print("NextRoad:" + nextV.ToString());
            nav.SetDestination(nextV);

        }
        else {
            print("Back" );
            back();
        }

       
    
      
    }


    private void back()
    {
        goByPath.Clear();
        goByPath.Add(destination);
        path = new List<Vector3>(PathDataCenter.pathPoints);
        path.Remove(destination);

    }




}
