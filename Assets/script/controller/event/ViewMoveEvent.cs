using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMoveEvent : BaseEvent
{
    public bool movable = false;
    public ViewMoveEvent(bool movable)
        : base(null, "ViewMove", null)
    {
        this.movable = movable;
    }

    public bool canMove()
    {
        return movable;
    }
}
