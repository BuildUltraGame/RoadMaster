using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 矿山生成的时候发生事件
/// </summary>
public class MineMoutainSpawnerEvent : BaseEvent {
    private MineMountain mine;
    
    public MineMoutainSpawnerEvent(MineMountain mine )
        : base(mine.gameObject, "MineMoutainSpawne",null)
    {
        this.mine = mine;
    }

    public MineMountain getMineMountaion()
    {
        return mine;
    }

	
}
