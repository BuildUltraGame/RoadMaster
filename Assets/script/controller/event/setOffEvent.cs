using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setOffEvent : BaseEvent
{
    //不需要额外信息
    public setOffEvent(GameObject _subject, GameObject _object)
        : base(_subject, "WaitClick", _object)
    {

    }


}