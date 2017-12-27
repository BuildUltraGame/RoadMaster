using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class configManager : MonoBehaviour {

    string[] properties;//读取属性项
    int propertiesAmount;//属性数量
    Dictionary<int, Dictionary<string, string>> config = new Dictionary<int, Dictionary<string, string>>();//配置字典
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// 读取配置
    /// </summary>
    void readConfig()
    {

    }

    /// <summary>
    /// 使用id获取对应游戏对象的属性,返回一个string,如果属性为int 或者float可以强转
    /// </summary>
    /// <param name="id">游戏对象的id</param>
    /// <param name="property">你需要查找的属性</param>
    public string getInformationById(int id,string property)
    {
        return null;
    }

    /// <summary>
    /// 通过已知的属性内容来获取对应的id
    /// </summary>
    /// <param name="property">属性的名字</param>
    /// <param name="content">属性的值</param>
    /// <returns></returns>
    public int searchIdByInformation(string property,string content)
    {
        return 0;
    }
}
