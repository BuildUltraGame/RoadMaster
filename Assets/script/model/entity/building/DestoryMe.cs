using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂在所有新产生的矿车上的一个脚本，用来控制第一次碰撞不被毁灭
/// 感觉有二次开发的潜力
/// </summary>
public class DestoryMe : MonoBehaviour {

    public bool couldDestory = false;
    void Awake()
    {
        couldDestory = false;
    }
}
