using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 播放语音事件
/// </summary>
public class AudioEvent : BaseEvent {
    private AudioClip audio;
    private float maxWait = 0;
    private float sendTime = 0;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj">游戏物体</param>
    /// <param name="audio">语音</param>
    /// <param name="maxWait">语音播放最大等待时间</param>
    public AudioEvent(GameObject obj, AudioClip audio,float maxWait):base(obj,"playAudio",null)
    {
        this.audio = audio;
        this.maxWait = maxWait;
        sendTime=Time.realtimeSinceStartup;
    }

    public AudioClip getAudio()
    {
        return audio;
    }

    public bool isAble()
    { 
        if (Time.realtimeSinceStartup-sendTime<maxWait)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
