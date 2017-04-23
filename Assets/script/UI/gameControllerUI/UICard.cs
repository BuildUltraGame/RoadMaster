using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;

public class UICard : UIDragDropItem {
    public TweenScale ts;
    private static bool DEBUG = true;

    private int ID;
    protected override void OnDragDropStart()
    {
        base.OnDragDropStart();
        ts.enabled = true;
        ts.PlayForward();

    }

    protected override void OnDragEnd()
    {
        base.OnDragEnd();
        ts.PlayReverse();
    }
    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);

        

        Ray ray = new Ray();
        if (DEBUG)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        else {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        }

        RaycastHit hit = new RaycastHit();
        Physics.Raycast(ray,out hit);

        if (hit.collider!=null)
        {
            EventAggregator.SendMessage<CreateUnitEvent>(new CreateUnitEvent(ID,hit.point));
        }
        
    }

    public void setID(int i)
    {
        ID = i;
    }




    public void setName(string name)
    {
       GetComponentInChildren<UILabel>().text=name;
    }

    public int getID()
    {
        return ID;
    }



    
}
