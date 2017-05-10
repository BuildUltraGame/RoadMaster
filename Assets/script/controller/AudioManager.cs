using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;
/// <summary>
/// 语音控制器
/// </summary>
/// 
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour,IListener<AudioEvent> {

    public AudioSource audioSource;
    public static AudioManager instance;

    private Queue<AudioEvent> audios = new Queue<AudioEvent>();

    private AudioEvent audioNow;

	void Awake() {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        UnityEventCenter.Register<AudioEvent>(this);
        AudioListener.volume = 5f;
    }

    public static AudioManager getInstance()
    {
        return instance;
    }
	
	void FixedUpdate () {
        
        
	}

    public void Handle(AudioEvent message)
    {
        if (message.getSubjectOwner()==GameobjBase.PLAYER)
        {
            AudioSource.PlayClipAtPoint(message.getAudio(), message.getSubject().transform.position);
        
        }
        // audios.Enqueue(message);
    }
}
