using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEventAggregator;

public class createUnit : MonoBehaviour {
    public MineMountain mm;
    public UISprite producer;
    public UIButton button;
    public List<int> IDList;
    public List<string> picList;
    public int nameNum=0;
    public Vector3 pos=Vector3.zero;
    public Reminder rm;

    //public Dictionary<int, string> nameDict = new Dictionary<int, string>();
    public TextAsset nameInfoText;


    void Awake()
    {
        //InitText();
        producer = this.GetComponentInChildren<UISprite>();
    }
    void Start()
    {
        //producer = this.gameObject.GetComponent<UISprite>();

        /*foreach (KeyValuePair<int, string> item in nameDict)
        {
            for (nameNum = 0; nameNum < 4; nameNum++)
            {
                picList[nameNum]=nameDict[IDList[nameNum]];
            }
        }
        
        producer.spriteName = picList[nameNum];*/
        IDList = new List<int>{0,1,2,3};
        picList = new List<string> { "mine's truck", "police", "rogue", "worker" };
        button = this.gameObject.GetComponent<UIButton>();
    }
    /// <summary>
    /// 
    /// </summary>
    /*void InitText()
    {
        string text = nameInfoText.text;
        string[] strArray = text.Split('\n');
        foreach (string str in strArray)
        {
            string[] proArray = str.Split(',');
            int id = int.Parse(proArray[0]);
            string name = proArray[1];
            nameDict.Add(id, name);
        }
    }*/
    public void nextButton()
    {
        nameNum++;
        if (nameNum >= 4)
        {
            nameNum = 0;
        }

        producer.spriteName = picList[IDList[nameNum]];
    }

    public void frontButton()
    {
        nameNum--;
        if (nameNum < 0)
        {
            nameNum = 3;
        }
        producer.spriteName = picList[IDList[nameNum]];
    }
/// <summary>
/// 未知
/// </summary>
/// <param name="newNameList"></param>
    /*public void updateNameList(List<int> newNameList)
    {
        IDList = newNameList;
    }*/
    /// <summary>
    /// 用户点击制造单位，未完成
    /// </summary>
    public void OnClick()
    {
        EventAggregator.SendMessage<BaseEvent>(new unitEvent(null, null, null, nameNum));
        Debug.Log(nameNum,null);
        /*if (pos == Vector3.zero)
        {
            rm.sendHint("please choose your point");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
        }
        else 
        {
            
            EventAggregator.SendMessage<BaseEvent>(new DestroyEvent(gameObject, attackObj.gameObject));
            pos = Vector3.zero;
            Tags.RAILWAY
        }*/
    }

    public void update()
    {

    }

    public class unitEvent : BaseEvent
    {
        public int unitID;
        Vector3 p;
        public unitEvent(GameObject _subject, string verb, GameObject _object, int ID)
            : base(_subject, verb, _object)
        {
            unitID = ID;
        }
    }
}
