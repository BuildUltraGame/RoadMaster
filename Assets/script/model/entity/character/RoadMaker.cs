using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;

public class RoadMaker : CollisionBaseHandler
{

   
    public GameObject targetRoad = null;

    public AudioClip clip;


    public void setTargetRoad(GameObject target)
    {
        if (target == null)
        {
            throw new Exception("传入的路为空");
        }

        BreakableRoad r = target.GetComponent<BreakableRoad>();
        if (r == null)
        {
            throw new Exception("传入的对象并不是一个可以修复的路");
        }

        targetRoad = target;


    }

    void Update()
    {
        if (targetRoad == null)
        {//确定目的地所在的破路
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(new Ray(GetComponent<Roadmovable>().targetPosition + Vector3.up, Vector3.down), out hit, Layers.RAILWAY))
            {
                if (hit.collider.gameObject.tag==Tags.Building.BREAKABLEROAD) {
                    setTargetRoad(hit.collider.gameObject);
                }
            }


        }
    }

    public override void OnWorldUnitCollisionStart(Collider obj)
    {
        if (obj.gameObject != targetRoad)
        {
            return;
        }
        else {
            BreakableRoad road = obj.gameObject.GetComponent<BreakableRoad>();
            road.repairRoad();
          //  UnityEventCenter.SendMessage<AudioEvent>(new AudioEvent(gameObject, clip));
        }

    }



}
