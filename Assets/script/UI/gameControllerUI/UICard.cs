using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;

public class UICard : UIDragDropItem {
    public TweenScale ts;
    private static bool DEBUG = true;

    private Spawner sp;

    public UISprite CD;
    public BoxCollider cardCollider;

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

    private void setID(int i)
    {
        ID = i;
    }

    public void setSpawner(Spawner sp)
    {
        this.sp = sp;

        setID(sp.spawnUnit.GetComponent<GameobjBase>().game_ID);
        setName(sp.spawnUnit.GetComponent<GameobjBase>().game_name);
        
    }


    private void setName(string name)
    {
       GetComponentInChildren<UILabel>().text=name;
    }

    public int getID()
    {
        return ID;
    }

    void Update()
    {
        base.Update();

        CD.fillAmount = sp.coolDown / sp.CD;
        if (CD.fillAmount.Equals(0f))
        {
            cardCollider.enabled = true;
        }
        else
        {
            cardCollider.enabled = false;
        }

    }



    
}
