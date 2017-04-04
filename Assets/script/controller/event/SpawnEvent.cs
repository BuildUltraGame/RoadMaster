using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


class SpawnEvent:BaseEvent
{

    public SpawnEvent(GameObject gameObj)
        : base(gameObj, "Spawn", null)
    {
        
    }

    public bool hasNavData()
    {
        return getObject().GetComponent<NavMeshAgent>() != null;
    }
    public float getDistance()
    {
        NavMeshAgent nav;
        if ((nav = getObject().GetComponent<NavMeshAgent>())!=null)
        {
           return nav.remainingDistance;
        }else{
            return -1;
        }
    }

    public float getSpeed()
    {
        NavMeshAgent nav;
        if ((nav = getObject().GetComponent<NavMeshAgent>()) != null)
        {
            return nav.speed;
        }
        else
        {
            return -1;
        }
    }


}

