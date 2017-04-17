using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEventAggregator;


public class createUnit : MonoBehaviour, IListener<MineSelectEvent>, IListener<cancelMountainEvent>
{
    public UISprite cdsprite;
    public UISprite producer;
    public UIButton button;
    public UISprite lockButton;    //封锁建造
    private List<int> IDList;   //存放ID
    private List<string> picList;    //存放名称
    private List<float> CDTime;    //存放冷却时间
    public int nameNum=0;     //将ID与name相对应

    public int unitSelected;    //当前所选单位序号
    public List<Spawner> sp;
    

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

        picList = new List<string>();
        IDList= new List<int>();
        button = this.gameObject.GetComponent<UIButton>();
        cdsprite = this.transform.Find("forCD").GetComponent<UISprite>();
        cdsprite.fillAmount = 0.0f;
        CDTime = new List<float>();
       
    }
    public void Handle(MineSelectEvent message)
    {
        lockButton.gameObject.SetActive(false);
        sp = message.getMine().getSpawnerList();
        for (int i = 0; i < sp.Count; i++)
        {
            //sp[i].spawnUnit.GetComponent<GameobjBase>().game_ID;
            picList.Add(sp[i].spawnUnit.GetComponent<GameobjBase>().game_name_en);
            IDList.Add(IDs.getIDByName(picList[i]));
        }
    }

    
    /// <summary>
    /// 
    /// </summary>
    
    public void nextButton()
    {
        nameNum++;
        if (nameNum >= 8)
        {
            nameNum = 0;
        }

        producer.spriteName = picList[nameNum];
        unitSelected = nameNum;
    }

    public void frontButton()
    {
        nameNum--;
        if (nameNum < 0)
        {
            nameNum = 7;
        }
        producer.spriteName = picList[nameNum];
        unitSelected = nameNum;
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
        EventAggregator.SendMessage<unitEvent>(new unitEvent(null, null, null, IDList[unitSelected]));
    }

    public void LateUpdate()
    {
     
    }

    public void Update()
    {
        for (int i = 0; i < sp.Count;i++ )
        {
            if (IDList[unitSelected]==sp[i].spawnUnit.GetComponent<GameobjBase>().game_ID)
            {
                cdsprite.fillAmount = (sp[i].coolDown / sp[unitSelected].CD);
            }
        }
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
