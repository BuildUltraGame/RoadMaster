﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;

public class UICardUnitBuilder : UIDragDropItem
{

    public TweenScale ts;


    private Spawner sp;

    public UISprite CD;
    public BoxCollider cardCollider;

    protected override void OnDragDropStart()
    {
        base.OnDragDropStart();
        ts.enabled = true;
        ts.PlayForward();


    }

    protected override void OnPress(bool isPressed)
    {
        base.OnPress(isPressed);
        if (isPressed)
        {
            UnityEventCenter.SendMessage<ViewMoveEvent>(new ViewMoveEvent(false));
        }
        else
        {
            UnityEventCenter.SendMessage<ViewMoveEvent>(new ViewMoveEvent(true));
        }

    }

    protected override void OnDragDropEnd()
    {
        base.OnDragDropEnd();

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

        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        }
        else
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        }



        RaycastHit hit = new RaycastHit();
        //if (Layers.CHARACTER == IDs.getLayerByID(sp.spawnUnit.GetComponent<GameobjBase>().game_ID))
        if (sp.spawnUnit.GetComponent<GameobjBase>().game_ID == IDs.getIDByName(Tags.Character.GATEWORKER))
        {//如果是人的话,检测到是搬道闸才能发送信息建造
            Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << Layers.BUILDING | 1 << Layers.RAILWAY);
            if (hit.collider == null || hit.collider.gameObject.tag != Tags.GATE)
            {
                return;
            }
        }
        else
        {
            //如果是其他的话,随便拖
            Physics.Raycast(ray, out hit);
        }



        if (hit.collider != null)
        {//建造信息
            UnityEventCenter.SendMessage<CreateUnitEvent>(new CreateUnitEvent(sp.spawnUnit.GetComponent<GameobjBase>().game_ID, hit.point));
        }


    }



    public void setSpawner(Spawner sp)
    {
        this.sp = sp;

    }

  

    void Update()
    {
        base.Update();

        CD.fillAmount = sp.coolDown / sp.CD;
        if (CD.fillAmount.Equals(0f))
        {
            interactable = true;
        }
        else
        {
            interactable = false;
        }

    }
}
