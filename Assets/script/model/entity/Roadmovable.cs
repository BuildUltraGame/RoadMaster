﻿using System.Collections;
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

    private NavMeshAgent nav=null;
    private Vector3 destination;
    public GameObject target;
    private Animator mAnime;

	// Use this for initialization
	void Start () {
        nav = GetComponent<NavMeshAgent>();
        mAnime=GetComponent<Animator>();
        if (mAnime != null)
        {
            mAnime.SetFloat("VSpeed", 1);
        }
    }
	
	// Update is called once per frame
	void Update () {
        nav = nav ?? GetComponent<NavMeshAgent>();
        if (destination == null)
        {
            return;
        }
        

        if (target != null)
        {
            setDestination(target.transform.position);
        }
    

 

        //到达目的地,销毁自己
        if (nav.pathStatus==NavMeshPathStatus.PathComplete&&Mathf.Abs(nav.remainingDistance-nav.stoppingDistance)<=0.1)
        {
          //  Destroy(gameObject);
        }
	}

    /// <summary>
    /// 设置目的地
    /// </summary>
    /// <param name="v"></param>
    public void setDestination(Vector3 v)
    {
        nav = nav ?? GetComponent<NavMeshAgent>();
        destination = v;
       
        nav.SetDestination(destination);
    }

    /// <summary>
    /// 设置目标对象,实时跟踪
    /// </summary>
    /// <param name="targetObj">目标对象</param>
    public void setDestination(GameObject targetObj)
    {
        target = targetObj;
       
    }

    /// <summary>
    /// 设置速度
    /// </summary>
    /// <param name="s"></param>
    public void setSpeed(float s)
    {
        nav.speed = s;

    }

}
