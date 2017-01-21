using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewRailwaymovable : MonoBehaviour {

	private NavMeshAgent nav;
    private Vector3 destination;

    void Start()
    {
        nav=GetComponent<NavMeshAgent>();
        destination = transform.position;

        if(nav==null){
            throw new System.Exception("載具对象必须要有NavMeshAgent组件");
        }
    }

    public void setDestination(Vector3 v)
    {
        destination = v;
        nav.SetDestination(destination);
    }

    public void back()
    {
        

    }




}
