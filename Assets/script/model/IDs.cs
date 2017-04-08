using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDs  : MonoBehaviour{
    public TextAsset list;

    void Awake(){
        initIDMap(list);

    }

    private static Hashtable IDMap;
    
    /// <summary>
    /// 根据名字获取ID 
    /// </summary>
    /// <param name="name">名字</param>
    /// <returns>对应ID</returns>
    public static int getIDByName(string name)
    {
       

        if(IDMap==null){
            throw new Exception("ID库初始化错误");
        }

        if (name == null)
        {
            return 0;
        }

        if(IDMap.ContainsValue(name)){
            foreach(DictionaryEntry item in IDMap){
                if(item.Value==name){
                    return (int)item.Key;
                }
            }
        }
        else
        {
            throw new Exception("没找到与name对应的ID");
            
        }

        return -1;

    }
    /// <summary>
    /// 根据ID获取名字
    /// </summary>
    /// <param name="ID">ID</param>
    /// <returns>名字</returns>
    public static string getNameByID(int ID)
    {


        if(IDMap==null){
            throw new Exception("ID库初始化错误");
        }

        if(IDMap.ContainsKey(ID)){
            return (string)IDMap[ID];
        }else if(IDMap.ContainsKey(ID.ToString())){
            return (string)IDMap[ID.ToString()];
        }
        else
        {
            throw new Exception("无法找到ID:"+ID+"的name");
        }

    
    }
    /// <summary>
    /// 通过ID获得代表的游戏单位所在层(如,建筑,车,人)
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>层</returns>
    public static int getLayerByID(int id)
    {

        if(id<0){
            return Layers.BUILDING;
        }
        else if (id > 0 && id < 1000)
        {
            return Layers.VEHICLE;
        }
        else if(id>=1000) {
            return Layers.CHARACTER;
        }
        else
        {
            throw new Exception("并没有相应的ID");
        }

    }


    /// <summary>
    /// 初始化ID库
    /// </summary>
    private static void initIDMap(TextAsset list)
    {
        IDMap = new Hashtable();

        string[] lines = list.text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        foreach(string line in lines){
            string[] s = line.Split(',');
            IDMap.Add(s[0],s[1]);
        }

    }
	
}
