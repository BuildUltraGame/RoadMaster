using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEvent;

/// <summary>
/// 扳道闸工人脚本
/// 使用时需要先设置目标setTargetGate
/// </summary>
public class GateChanger : CollisionBaseHandler {
    [HideInInspector]
    public GameObject targetGate = null;

    public AudioClip clip;
    private int linkNum;

    public void setTargetGate(GameObject target)
    {
        if(target==null){
            throw new Exception("传入目标道闸为空");
        }

        MetroGate gate = target.GetComponent<MetroGate>();
        if (gate == null)
        {
            throw new Exception("传入对象并没有包含道闸脚本");
        }

        targetGate = target;


    }

    void Update()
    {
        if(targetGate==null){//确定目的地所在的道闸口
            RaycastHit hit=new RaycastHit();
            if (Physics.Raycast(new Ray(GetComponent<Roadmovable>().targetPosition + Vector3.up, Vector3.down), out hit,Layers.RAILWAY))
            {
                setTargetGate(hit.collider.gameObject);
            }
            
           
        }
    }

    public override void OnWorldUnitCollisionStart(Collider obj)
    {
        if(obj.gameObject!=targetGate){
            return;
        }


        if(obj.tag==Tags.GATE){
            UnityEventCenter.SendMessage<AudioEvent>(new AudioEvent(gameObject, clip));
            targetGate.GetComponent<MetroGate>().GateChange(transform.position);
            gameObject.SendMessage(GameobjBase.TryDestroyFUNC);
            //Destroy(gameObject);
        }

    }

}
