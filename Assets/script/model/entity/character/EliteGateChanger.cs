using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;

public class EliteGateChanger : GateChanger {


    public override void OnWorldUnitCollisionStart(Collider obj)
    {
        if (obj.gameObject != targetGate)
        {
            return;
        }


        if (obj.tag == Tags.GATE)
        {
            if (targetGate.GetComponent<MetroGate>().getCanChange())
            {
                UnityEventCenter.SendMessage<AudioEvent>(new AudioEvent(gameObject, clip));
                targetGate.GetComponent<MetroGate>().GateChange(transform.position);
                targetGate.GetComponent<MetroGate>().setCanChange(false);
            }
            else {
                targetGate.GetComponent<MetroGate>().GateChange(transform.position);
                gameObject.SendMessage(GameobjBase.TryDestroyFUNC);
            }
            

        }

    }


    void OnDestroy()
    {
        if (null!= targetGate) {
            targetGate.GetComponent<MetroGate>().setCanChange(true);
        }
    }
}
