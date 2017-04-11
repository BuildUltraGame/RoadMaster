using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEventAggregator;

public class createUnit : MonoBehaviour, IListener<MineSelectEvent>, IListener<cancelMountainEvent>
{
    public UISprite producer;
    public UIButton button;
    public UISprite lockButton;
    public List<int> IDList;
    public List<string> picList;
    public int nameNum=0;

    //public Dictionary<int, string> nameDict = new Dictionary<int, string>();
    //public TextAsset nameInfoText;


    void Awake()
    {
        //InitText();
        producer = this.GetComponentInChildren<UISprite>();
    }
    void Start()
    {
        EventAggregator.Register<MineSelectEvent>(this);
        EventAggregator.Register<cancelMountainEvent>(this);
        //producer = this.gameObject.GetComponent<UISprite>();

        /*foreach (KeyValuePair<int, string> item in nameDict)
        {
            for (nameNum = 0; nameNum < 4; nameNum++)
            {
                picList[nameNum]=nameDict[IDList[nameNum]];
            }
        }*/
        
        IDList = new List<int>{1,1001,1000,1002};
        picList = new List<string> { "truck", "police", "rogue", "worker" };
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

        producer.spriteName = picList[nameNum];
    }

    public void frontButton()
    {
        nameNum--;
        if (nameNum < 0)
        {
            nameNum = 3;
        }
        producer.spriteName = picList[nameNum];
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
    /// 用户点击制造单位
    /// </summary>
    public void OnClick()
    {
        EventAggregator.SendMessage<BaseEvent>(new unitEvent(null, null, null, IDList[nameNum]));
        Debug.Log(IDList[nameNum], null);
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

    public void Handle(MineSelectEvent message)
    {
        lockButton.gameObject.SetActive(false);
    }
    public void Handle(cancelMountainEvent message)
    {
        lockButton.gameObject.SetActive(true);
    }
    void OnDisable()
    {
        EventAggregator.UnRegister<MineSelectEvent>(this);
        EventAggregator.UnRegister<cancelMountainEvent>(this);
    }
}
