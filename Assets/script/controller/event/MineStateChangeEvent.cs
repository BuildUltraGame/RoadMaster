using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 当矿山的状态改变的时候会产生本事件
/// 状态改变包括了金钱或者产生速度变化的时候等
/// </summary>
public class MineStateChangeEvent : BaseEvent
{
    private MineMountain mine;
    public MineStateChangeEvent(MineMountain mine):base(mine.gameObject,"StateChange",mine.gameObject)
    {
        this.mine = mine;
    }


    public MineMountain getMine()
    {
        return mine;
    }



}

