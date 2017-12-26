using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDragCard : UIDragDropItem
{

    protected override void OnDragEnd() {
        print("结束");
    }
    protected override void OnDragDropStart() {
        print("开始");
    }
    protected override void OnDragDropRelease(GameObject surface) {
        surface.BroadcastMessage("onDragCrad",gameObject,SendMessageOptions.DontRequireReceiver);
        print("???????" + surface.name);
    }

}
