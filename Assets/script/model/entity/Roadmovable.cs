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

    private NavMeshAgent nav;
    private Vector3 destination;
    private GameObject target;

	// Use this for initialization
	void Start () {
	    nav=GetComponent<NavMeshAgent>();
        destination = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if(target!=null){
            setDestination(target.transform.position);
        }
	}

    /// <summary>
    /// 设置目的地
    /// </summary>
    /// <param name="v"></param>
    public void setDestination(Vector3 v)
    {
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
