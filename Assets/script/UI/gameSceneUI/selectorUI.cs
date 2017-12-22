using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectorUI : MonoBehaviour {

    public Transform[] worlds;
    public UITable Table;
    public UISprite missionPrefab;
    // Use this for initialization
	void Start () {
        for(int i=0;i<15;i++)
        {
            UISprite obj = Instantiate<UISprite>(missionPrefab);
            obj.transform.GetChild(0).GetComponent<UILabel>().text = (i+1).ToString();
            if (i>2)
            {
                obj.spriteName = "蓝色展示框";
            }
            obj.transform.parent = Table.transform;
            obj.transform.localPosition = new Vector3(0, 0, 0);
            obj.transform.localScale= new Vector3(1, 1, 1);
            obj.name = (i + 1).ToString();
            if(i<2)//only two
            {
                UIEventListener.Get(obj.gameObject).onClick = delegate (GameObject clicked)
                {

                    baseController.missionToLoad = int.Parse(clicked.name);
                    SceneManager.LoadSceneAsync("hint");
                };
            }
        }
        Table.Reposition();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    ///待改进
    ///
    void setAllNotClicked()
    {
        foreach(Transform i in worlds)
        {
            NGUITools.SetActive(i.GetChild(0).gameObject, false);
        }
    }
    public void onClick1()
    {
        setAllNotClicked();
        NGUITools.SetActive(worlds[0].GetChild(0).gameObject, true);
    }
    public void onClick2()
    {
        setAllNotClicked();
        NGUITools.SetActive(worlds[1].GetChild(0).gameObject, true);
    }
    public void onClick3()
    {
        setAllNotClicked();
        NGUITools.SetActive(worlds[2].GetChild(0).gameObject, true);
    }
}
