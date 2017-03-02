using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class createUnit : MonoBehaviour {
    public MineMountain mm;
    private UISprite producer;
    public List<int> nameList;
    public List<string> picList;
    private int nameNum;
    public Vector3 pos=Vector3.zero;
    public Reminder rm;

    public Dictionary<int, string> nameDict = new Dictionary<int, string>();
    public TextAsset nameInfoText;


    void Awake()
    {
        InitText();
    }
    void Start()
    {
        producer = this.gameObject.GetComponent<UISprite>();

        foreach (KeyValuePair<int, string> item in nameDict)
        {
            for (nameNum = 0; nameNum < 4; nameNum++)
            {
                picList[nameNum]=nameDict[nameList[nameNum]];
            }
        }
        nameNum = 0;
        producer.spriteName = picList[nameNum];
    }
    /// <summary>
    /// 
    /// </summary>
    void InitText()
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
    }
    public void frontButton()
    {
        nameNum++;
        if (nameNum >= 4)
        {
            nameNum = 0;
        }

        producer.spriteName = picList[nameNum];
    }

    public void nextButton()
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
    public void updateNameList(List<int> newNameList)
    {
        nameList = newNameList;
    }
    /// <summary>
    /// 用户点击制造单位，未完成
    /// </summary>
    public void OnClick()
    {
        if (pos == Vector3.zero)
        {
            rm.sendHint("please choose your point");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
        }
        /*else 
        { 
            mm.buildUnitByName(picList[nameNum], pos);
            pos = Vector3.zero;
            Tags.RAILWAY
        }*/
    }
}
