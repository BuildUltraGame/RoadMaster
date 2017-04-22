using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 该类指挥后部演示的脚本播放
/// </summary>
public class gamePlayer : MonoBehaviour {

    int cameraNum;//摄像机数
    const int maxCamera = 3;
    int frameCountCam;//摄像机切换计数
    int frameCountUnit;//单位制造计数
    GameObject[] camera;
    // Use this for initialization
	void Start () {
        cameraNum = 1;
        frameCountCam = 0;
        camera = new GameObject[3];
        camera[0]=GameObject.Find("spotting1");
        camera[1] = GameObject.Find("spotting2");
        camera[2] = GameObject.Find("spotting3");
        camera[1].SetActive(false);
        camera[2].SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        frameCountCam++;
        frameCountUnit++;
        if(frameCountCam%100==0)
        {
            changeCameraPosition();
            frameCountCam = 0;
        }
	}

    /// <summary>
    /// 更改摄像机机位的函数
    /// </summary>
    public void changeCameraPosition()
    {
        if(cameraNum==maxCamera)
        {
            cameraNum = 1;
        }
        else
        {
            cameraNum++;
        }
        for (int i = 0; i < 3; i++)
            camera[i].SetActive(false);
        camera[cameraNum-1].SetActive(true);
    }
    /// <summary>
    /// 制造单位
    /// </summary>
    public void createRandomCar()
    {

    }
}
