using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;
/// <summary>
/// 语音控制器
/// </summary>
public class AudioManager : MonoBehaviour,IListener<AudioEvent> {
    public static AudioManager instance;

    private Queue<AudioEvent> audios = new Queue<AudioEvent>();

    private AudioEvent audioNow;

	void Awake() {
        instance = this;
        UnityEventCenter.Register<AudioEvent>(this);
    }

    public static AudioManager getInstance()
    {
        return instance;
    }
	
	void Update () {
        if (audioNow==null)
        {

            do
            {
                audioNow = audios.Dequeue();

            } while (!audioNow.isAble());
            
            audioNow.getAudio().Play();
        }
        else
        {
            if (!audioNow.getAudio().isPlaying)
            {
                audioNow = null;
            }
        }
        
	}

    public void Handle(AudioEvent message)
    {
        audios.Enqueue(message);
    }
}
