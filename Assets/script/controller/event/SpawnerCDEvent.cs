using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 生成器CD事件,当生成器造完一个单位的时候,开始CD计时,每一秒会发送一个本事件
/// </summary>
public class SpawnerCDEvent : BaseEvent {
    private int id;
    private float CD;
    private float timeRemain;
    public SpawnerCDEvent(GameObject _subject,float timeRemain)
        : base(_subject, "CD", null)
    {
        id = getSubject().GetComponent<Spawner>().spawnUnit.GetComponent<GameobjBase>().game_ID;
        CD = getSubject().GetComponent<Spawner>().getCD();
        this.timeRemain = timeRemain;
    }


    public float getCD()
    {
        return CD;
    }

    public float getRemainTime()
    {
        return timeRemain;
    }

    public int getUnitID() {
        return id;
    }
}
