using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;

public class BaseAudioGameObj : MonoBehaviour {

    public AudioClip startAudio;
    public AudioClip destoryAudio;

    // Use this for initialization
    void Start() {
        UnityEventCenter.SendMessage<AudioEvent>(new AudioEvent(gameObject, startAudio, 1.5f));
    }

    // Update is called once per frame
    void Update() {

    }

    void OnDestroy()
    {
        UnityEventCenter.SendMessage<AudioEvent>(new AudioEvent(gameObject, destoryAudio, 1.5f));
    }


    public void TryDestroyGameObject()
    {
        
    }

}
