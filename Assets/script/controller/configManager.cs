using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class configManager : MonoBehaviour {

    string[] properties;//读取属性项
    int propertiesAmount;//属性数量
    static Dictionary<int, Dictionary<string, string>> config = new Dictionary<int, Dictionary<string, string>>();//配置字典
    public string path;//配置的指定路径
    // Use this for initialization
    void Start () {
        readConfig();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// 读取配置
    /// </summary>
    void readConfig()
    {
        StreamReader configFile= new StreamReader(path, System.Text.Encoding.UTF8);
        string line = configFile.ReadLine();//先读取第一行的属性名单表
        properties = line.Split(',');
        propertiesAmount = properties.Length;

        while((line=configFile.ReadLine())!=null)
        {
            string[] content = line.Split(',');
            Dictionary<string, string> Information = new Dictionary<string, string>();

            int id = Convert.ToInt32(content[0]);
            for(int i= 1;i< propertiesAmount;i++)//读出一行数据并放入字典,0号元素恒为id
            {
                Information.Add(properties[i], content[i]);
            }
            config.Add(id, Information);//将数据记录插入字典
        }
    }

    /// <summary>
    /// 使用id获取对应游戏对象的属性,返回一个string,如果属性为int 或者float可以强转
    /// </summary>
    /// <param name="id">游戏对象的id</param>
    /// <param name="property">你需要查找的属性</param>
    public static string getInformationById(int id,string property)
    {
        string ret=null;
        Dictionary<string, string> Information = configManager.config[id];
        ret = Information[property];
        return ret;
    }

    /// <summary>
    /// 通过已知的属性内容来获取对应的id
    /// </summary>
    /// <param name="property">属性的名字</param>
    /// <param name="content">属性的值</param>
    /// <returns></returns>
    public static int[] searchIdByInformation(string property,string content)
    {
        //var list = (from information in configManager.config.Values where information[property].Equals(content) select d.Key).ToList();
        Dictionary<string, string> Information=null;
        int[] idList=null;
        int idCount = 0;
        foreach (Dictionary<string, string> temp in configManager.config.Values)//先搜索符合条件的数据
        {
            if (temp[property].Equals(content))
                Information = temp;
        }
        foreach(int id in configManager.config.Keys)//再搜索id
        {
            if (configManager.config[id] == Information)
            {
                idList[idCount] = id;
                idCount++;
            }
        }

        return idList;
    }
}
