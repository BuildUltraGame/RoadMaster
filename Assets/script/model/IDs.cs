using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IDs  : MonoBehaviour{


    void Awake(){
        
        initFromPrefab();
    }

    private static Hashtable IDMap=new Hashtable();
    
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

        if(IDMap.ContainsValue(new GameObjectName(name))){
            foreach(DictionaryEntry item in IDMap){
                if((item.Value as GameObjectName).en==name){
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
            return (IDMap[ID] as GameObjectName).en;
        }else if(IDMap.ContainsKey(ID.ToString())){
            return (IDMap[ID.ToString()] as GameObjectName).en;
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
    private static void initFromPrefab()
    {
        GameObject[] prefabs=Resources.LoadAll<GameObject>("prefab/entity");
        foreach(GameObject obj in prefabs){
            GameobjBase b=obj.GetComponent<GameobjBase>();
            IDMap.Add(b.game_ID, new GameObjectName(b.game_name_en, b.game_name));
        }
    }

    public class GameObjectName
    {
        public string en;
        public string cn;

        public GameObjectName(string en,string cn=null)
        {
            this.en = en;
            this.cn = cn;
        }

        public override bool Equals(object obj)
        {
            return (obj as GameObjectName).en==en;
        }
    }
	
}
