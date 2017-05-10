using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;

/// <summary>
/// 督察员
/// </summary>
public class Inspector : ArriveDo
{

    public AudioClip clip;
    public override void Arrive()
    {
        
        SendMessage(Builder.BUILDFUNC);
        UnityEventCenter.SendMessage<AudioEvent>(new AudioEvent(gameObject,clip));
        base.Arrive();
    }
}
