using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
//管理游戏状态的类
[System.Serializable]
public class savedData
{
    public Achievement achievement;
    public MyProgress progress;
}

public class gameStatus : MonoBehaviour {

    //管理基本的游戏状态


    //全局属性
    int Mode;
    savedData data=new savedData();

    //method
    int signIn() {
        return 0;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mode"></param>
    void setMode(int mode)
    {
        Mode = mode;
    }

    void save()
    {
        int flag = 0;
        string path=Application.persistentDataPath;
        path = path + @"save.dat";
        if (File.Exists(path))//已经存在存档
        {
            flag = 1;
        }
        if (flag == 1)
        {
            FileStream stream = File.OpenRead(path);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
            stream.Flush();
            stream.Close();
        }
        else
        {
            FileStream stream = File.Create(path);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
            stream.Flush();
            stream.Close();
        }
    }

    void saveAuto ()
    {
        save();
    }

    void load()
    {
        int flag = 0;
        string path = Application.persistentDataPath;
        path = path + @"save.dat";
        if (File.Exists(path))//已经存在档位
        {
            flag = 1;
        }
        if (flag == 1)
        {
            FileStream stream = File.OpenRead(path);
            BinaryFormatter formatter = new BinaryFormatter();
            data = (savedData)formatter.Deserialize(stream);
            stream.Close();
        }
        else
        {
            FileStream stream = File.Create(path);
            BinaryFormatter formatter = new BinaryFormatter();
            data = (savedData)formatter.Deserialize(stream);
            stream.Close();
        }
    }
    // Use this for initialization
	void Start () {
        setMode(constDic.INITIAL);//初始界面
        load();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
