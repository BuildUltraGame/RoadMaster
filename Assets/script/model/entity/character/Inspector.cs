using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 督察员
/// </summary>
public class Inspector : ArriveDo
{
    public override void Arrive()
    {
        
        SendMessage(Builder.BUILDFUNC);
        base.Arrive();
    }
}
