using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEvent;


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
    public bool isMineSelected;    //是否选择矿山
    

    //public Dictionary<int, string> nameDict = new Dictionary<int, string>();
    //public TextAsset nameInfoText;


    void Awake()
    {
        //InitText();
    }
    void Start()
    {
        UnityEventCenter.Register<MineSelectEvent>(this);
        UnityEventCenter.Register<cancelMountainEvent>(this);
        sp = new List<Spawner>();
        picList = new List<string>();
        IDList= new List<int>();
        producer = this.transform.Find("producer").GetComponent<UISprite>();
        button = this.transform.Find("producer").GetComponent<UIButton>();
        cdsprite = this.transform.Find("forCD").GetComponent<UISprite>();
        cdsprite.fillAmount = 0.0f;
        CDTime = new List<float>();
        isMineSelected = false;
        button.normalSprite = null;
    }
    public void Handle(MineSelectEvent message)
    {
        isMineSelected = true;
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
        if (isMineSelected == true)
        {
            nameNum++;
            if (nameNum >= sp.Count)
            {
                nameNum = 0;
            }
            producer.spriteName = picList[nameNum];
            unitSelected = nameNum;
        }
    }

    public void frontButton()
    {
        if (isMineSelected == true)
        {
            nameNum--;
            if (nameNum < 0)
            {
                nameNum = sp.Count - 1;
            }
            producer.spriteName = picList[nameNum];
            unitSelected = nameNum;
        }
    }

    /// <summary>
    /// 用户点击制造单位
    /// </summary>
    public void OnClick()
    {
        if (isMineSelected == true)
        {
            UnityEventCenter.SendMessage<unitEvent>(new unitEvent(null, null, null, IDList[unitSelected]));
        }
    }

    public void LateUpdate()
    {
     
    }

    public void Update()
    {
        if (isMineSelected == true)
        {
            for (int i = 0; i < sp.Count; i++)
            {
                if (IDList[unitSelected] == sp[i].spawnUnit.GetComponent<GameobjBase>().game_ID)
                {
                    cdsprite.fillAmount = (sp[i].coolDown / sp[i].CD);
                }
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
        isMineSelected = false;
        lockButton.gameObject.SetActive(true);
    }
    void OnDisable()
    {
        UnityEventCenter.UnRegister<MineSelectEvent>(this);
        UnityEventCenter.UnRegister<cancelMountainEvent>(this);
    }
}
