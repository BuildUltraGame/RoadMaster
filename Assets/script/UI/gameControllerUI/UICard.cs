using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;

public class UICard : MonoBehaviour {


    private Spawner sp;

    public UILabel nameLabel;
    public UILabel costLabel;

    private int ID;
 
 
    private void setID(int i)
    {
        ID = i;
        gameObject.name = ID+"";
    }

    public void setSpawner(Spawner sp)
    {
        this.sp = sp;

        setID(sp.spawnUnit.GetComponent<GameobjBase>().game_ID);
        setName(sp.spawnUnit.GetComponent<GameobjBase>().game_name);
        setSpriteName(sp.spawnUnit.GetComponent<GameobjBase>().game_name);
        setCost(sp.getCost());
    }

    private void setCost(int cost)
    {
        costLabel.text = cost + "";
    }


    private void setName(string name)
    {
        nameLabel.text=name;
    }

    private void setSpriteName(string name)
    {
        this.gameObject.GetComponent<UISprite>().spriteName = name;
    }

    public int getID()
    {
        return ID;
    }

    public Spawner getSpawner()
    {
        return sp;
    }

    
}
