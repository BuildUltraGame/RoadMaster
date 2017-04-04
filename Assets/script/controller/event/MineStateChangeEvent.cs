using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



class MineStateChangeEvent:BaseEvent
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

