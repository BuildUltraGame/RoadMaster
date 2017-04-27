using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 到达后做的事情,默认销毁
/// </summary>
public class ArriveDo : MonoBehaviour {
    public float destoryTime = 1f;
    public virtual void Arrive()
    {
        Destroy(gameObject, destoryTime);
    }

}
