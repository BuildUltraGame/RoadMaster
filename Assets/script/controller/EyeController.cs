using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;

/// <summary>
/// 相机控制器
/// </summary>
public class EyeController : MonoBehaviour {

    void Awake() {
        //注册生成单位事件
        //TODO: 未完成
        UnityEventCenter.Register<SpawnEvent>(this);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
