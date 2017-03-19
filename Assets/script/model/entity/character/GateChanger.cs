using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 扳道闸工人脚本
/// 使用时需要先设置目标setTargetGate
/// </summary>
public class GateChanger : CollisionBaseHandler {

    private Collider targetGate = null;
    private Vector3 targetV = Vector3.zero;
    private int linkNum;

    public void setTargetGate(Collider targetGate,Vector3 v, int linkNum)
    {
        this.targetGate = targetGate;
        this.targetV = v;
        this.linkNum = linkNum;
    }

    public override void OnWorldUnitCollisionStay(Collider targetGateOb)
    {
        if(targetGateOb == targetGate)
        {
            try
            {
                MetroGate3 metroGate = targetGateOb.GetComponent<MetroGate3>();
                metroGate.GateChange(targetV, linkNum);

            }
            catch
            {
                Debug.Log("搬道闸的时候出错！");
            }
            finally
            {
                Destroy(this.gameObject,1);
            }
        }

    }

}
