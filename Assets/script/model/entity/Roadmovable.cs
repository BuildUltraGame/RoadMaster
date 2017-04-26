﻿using Pathfinding;
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


    //计算出来的路线;  
    Path path;

    //移动速度;  
    public float playerMoveSpeed = 10f;

    //当前点  
    int currentWayPoint = 0;

    bool stopMove = true;

    //Player中心点;  
    float playerCenterY = 1.0f;
    private Animator mAnime;

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
    }

    //寻路结束;  
    private void OnPathComplete(Path p)
    {
       

        if (!p.error)
        {
            currentWayPoint = 0;
            path = p;
            stopMove = false;
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

    /// <summary>
    /// 设置速度
    /// </summary>
    /// <param name="s"></param>
    public void setSpeed(float s)
    {
        playerMoveSpeed = s;

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
        Vector3 dir = (currentWayPointV - transform.position).normalized;
        transform.LookAt(currentWayPointV);
        //计算这一帧要朝着 dir方向 移动多少距离;  
        dir *= playerMoveSpeed * Time.fixedDeltaTime;

        //计算加上这一帧的位移，是不是会超过下一个节点;  
        float offset = Vector3.Distance(transform.localPosition, currentWayPointV);

        if (offset < 0.1f)
        {
            transform.localPosition = currentWayPointV;

            currentWayPoint++;

            if (currentWayPoint == path.vectorPath.Count)
            {
                stopMove = true;
                //到达
                currentWayPoint = 0;
                path = null;

                SendMessage(ARRIVEFUNC);
            }
        }
        else
        {
            if (dir.magnitude > offset)
            {
                Vector3 tmpV3 = dir * (offset / dir.magnitude);
                dir = tmpV3;

                currentWayPoint++;

                if (currentWayPoint == path.vectorPath.Count)
                {
                    stopMove = true;

                    currentWayPoint = 0;
                    path = null;

                    //到达
                    SendMessage(ARRIVEFUNC);
                }
            }
            transform.localPosition += dir;
        }
    }

}
