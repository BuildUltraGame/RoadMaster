using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  悬崖
///  目前是摧毁接触到的所有物体
/// </summary>

public class FuckHole : CollisionBaseHandler{

    void OnTriggerEnter(Collider fuckedBoy)
    {
        GameObject fuckedBoyOb = fuckedBoy.gameObject;
        Debug.Log("name:"+ fuckedBoyOb.name);
        //应当调用物体的销毁脚本的销毁方法，但是好像还没有
        Destroy(fuckedBoyOb);

    }
}
