using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 督察员
/// </summary>
public class Inspector : MonoBehaviour
{
    public void Arrive()
    {
        Destroy(gameObject, 1f);
        SendMessage(Builder.BUILDFUNC);
    }
}
