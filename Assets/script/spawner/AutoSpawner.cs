using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 刷怪笼-自动生成版本
/// </summary>
public class AutoSpawner : Spawner {
    public float randomTime = 5f;
    public float randomOffset = 0.5f;

	// Use this for initialization
   public void Start () {
        base.Start();
        CDtimer.Destroy();

        
        CDtimer = TimerController.getInstance().NewTimer(() => Random.Range(randomTime - randomOffset, randomTime + randomOffset)
        , true, delegate (float time) {
            float newcool = CD - time;
            coolDown = newcool;

        }, delegate ()
        {
            setBuildFlag(true);
            build();
        });

        CDtimer.Start();
    }
	
	
}
