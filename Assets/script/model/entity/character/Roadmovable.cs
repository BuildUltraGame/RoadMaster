using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 人的基础移动脚本
/// 使用方法:
/// 1.挂载本脚本
/// 2.加NavMeshAgent组件
/// 3.设置NavMeshAgent组件中只能走road标记的物体
/// 4.设置目的地
/// 
/// 
/// </summary>
public class Roadmovable : MonoBehaviour {
    public const string ARRIVEFUNC = "Arrive";

    //目标位置;  
    public Vector3 targetPosition;

    Seeker seeker;

    //移动速度;  
    public float speed = 10f;
    [HideInInspector]public float step = 0.5f;

    //计算出来的路线;  
    Path path;
    //当前点  
    int currentWayPoint = 0;

    bool stopMove = true;

    //Player中心点;  
    float playerCenterY = 1.0f;
    private Animator mAnime;

    private float distance = 0f;

    // Use this for initialization  
    void Awake()
    {
        seeker = GetComponent<Seeker>();
        mAnime = GetComponent<Animator>();
        if (mAnime != null)
        {
            mAnime.SetFloat("VSpeed", 1);
        }
        playerCenterY = transform.localPosition.y;
        step = speed / 50;
    }

    //寻路结束;  
    private void OnPathComplete(Path p)
    {
       

        if (!p.error)
        {
            currentWayPoint = 0;
            path = p;
            stopMove = false;
            distance=p.GetTotalLength();
            if (delDistance!=null){
                delDistance(distance);
            }
        }

        
    }

    /// <summary>
    /// 设置目的地
    /// </summary>
    /// <param name="v"></param>
    public void setDestination(Vector3 v)
    {
        targetPosition = v;
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
        
    }

    /// <summary>
    /// 设置目标对象,实时跟踪
    /// </summary>
    /// <param name="targetObj">目标对象</param>
    public void setDestination(GameObject targetObj)
    {
        targetPosition = targetObj.gameObject.transform.position;

    }

    public delegate void DelDistance(float d);
    private DelDistance delDistance=null;
    public void getDistance(DelDistance del)
    {
        delDistance = del;
    }


    void FixedUpdate()
    {
        if (path == null || stopMove)
        {
            return;
        }

        

        //根据Player当前位置和 下一个寻路点的位置，计算方向;  
        Vector3 currentWayPointV = new Vector3(path.vectorPath[currentWayPoint].x, 
                                                path.vectorPath[currentWayPoint].y + playerCenterY,
                                                path.vectorPath[currentWayPoint].z);
        transform.LookAt(currentWayPointV);
        transform.position = Vector3.MoveTowards(transform.position, currentWayPointV, step);
        if (Mathf.Abs(Vector3.Distance(transform.position, currentWayPointV))<0.1f)
        {//到达某个目标点
            currentWayPoint++;
            if (currentWayPoint == path.vectorPath.Count)
            {
                stopMove = true;
                //到达
                currentWayPoint = 0;
                path = null;

                SendMessage(ARRIVEFUNC, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public Vector3 getTargetPosition()
    {
        return targetPosition;
    }

}
