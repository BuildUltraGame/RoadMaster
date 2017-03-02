using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mineGold : MonoBehaviour {
    public Reminder remind;
    public UILabel goldNow;
    public UILabel goldNeed;
    public UILabel GoldAll;
    public UILabel Train;
    public UILabel GoldGet;
    public UILabel GoldTime;
    MineMountain mineSelected;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void changeGold0()
    {
        goldNow.text = "";
    }
    public void changeGold1()
    {
        goldNeed.text = "";
    }
    public void changeGold2()
    {
        if (mineSelected == null)
           remind.sendHint("please select a mine");
        else GoldAll.text = ""+mineSelected.totalMine;
    }
    public void changeGold3()
    {
        Train.text = "";
    }
    public void changeGold4()
    {
        GoldGet.text = "";
    }
    public void changeGold5()
    {
        GoldTime.text = "";
    }
}
