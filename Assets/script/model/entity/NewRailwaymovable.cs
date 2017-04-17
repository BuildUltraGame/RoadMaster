using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 铁路寻路脚本
/// 
/// 寻路遵循以下原则
/// 
/// 1.优先检测到关键点是否可达,选择最近的作为目的地
/// 2.关键点没有可达的时候,选择最近的点,尽可能靠近
/// 3.最后无路可走,回头
/// 
/// </summary>
public class NewRailwaymovable : MonoBehaviour {

    private List<Vector3> goByPath=new List<Vector3>();
    private List<Vector3> path = null;

	private NavMeshAgent nav;
    public Vector3 destination;

    private bool lastIsTerminal = false;//最后一次寻路是否为末尾

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();

        if(nav==null){
            throw new System.Exception("載具对象必须要有NavMeshAgent组件");
        }

        path = new List<Vector3>(PathDataCenter.pathPoints);//copy一份
        destination = transform.position;
        nav.SetDestination(destination);

    }

    public void setDestination(Vector3 v)
    {
        destination = v;
        nav.SetDestination(destination);
    }

    void Update()
    {
        path = path ?? new List<Vector3>(PathDataCenter.pathPoints);

        if (!nav.hasPath||Mathf.Abs(nav.remainingDistance - nav.stoppingDistance) <= 1)
        {
            findNewRoad();//如果到达目的地,赶紧找下一个地点,对,累死你,不让你停
        }
        Debug.DrawLine(transform.position, destination, Color.red);
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
            if (nav.CalculatePath(v,p)&&p.status==NavMeshPathStatus.PathComplete) {
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
            nav.SetDestination(nextV);
            destination = nextV;
        }
        else {
            if (lastIsTerminal)
            {
                print("Back");
                back();
                lastIsTerminal = false;
                return;
            }

            minD = int.MaxValue;
            foreach (Vector3 v in path)
            {
                NavMeshPath p = new NavMeshPath();
                if (nav.CalculatePath(v, p) && p.status == NavMeshPathStatus.PathPartial)
                {
                    int d = p.corners.Length;
                    if (d < minD)
                    {
                        minD = d;
                        nextV = v;
                    }
                }
            }

            if (nextV.Equals(Vector3.zero))
            {
                print("Back");
                back();
            }
            else
            {
                lastIsTerminal = true;
                goByPath.Add(nextV);
                nav.SetDestination(nextV);
                destination = nextV;

            }

           
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
