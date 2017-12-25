using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;

/// <summary>
/// 预选车辆卡片的相关UI脚本
/// </summary>
public class vehiclePreparation : MonoBehaviour {

    public UIButton outButton;
    public UIButton inButton;
    public UISprite preWindow;//预放矿车的窗口
    // Use this for initialization
    void Start () {
        UnityEventCenter.Register<setOffEvent>(this);
    }

    void OnDisable()
    {
        UnityEventCenter.UnRegister<setOffEvent>(this);
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void Handle(setOffEvent message)
    {

    }


    public int vehicleAmountUpgrd;//可一次性发车的数量
    public float createGap;//矿车发车的间隔
    /// <summary>
    /// 接收到出发消息准备发车
    /// </summary>
    public void setOffTramcar()
    {
        Spawner[] arr = getUnitInformation();
        StartCoroutine(createTramcar(arr));        
    }

    /// <summary>
    /// 按照间隔制造矿车的协程
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    IEnumerator createTramcar(Spawner[] arr)
    {
        foreach (Spawner sp in arr)
        {
            UnityEventCenter.SendMessage<CreateUnitEvent>(new CreateUnitEvent(sp.spawnUnit.GetComponent<GameobjBase>().game_ID, Vector3.zero));
            yield return new WaitForSeconds(createGap);
        }
    }

    /// <summary>
    /// 初始化发车，todo
    /// </summary>
    /// <returns></returns>
    public Spawner[] getUnitInformation()
    {
        Spawner[] sp=new Spawner[10];
        ///
        /// 这里加入如何赋值序列的流程
        ///
        return sp;
    }

    /// <summary>
    /// 使选框进入场景
    /// </summary>
    public void onInButton()
    {
        NGUITools.SetActive(inButton.gameObject, false);
        SpringPanel.Begin(preWindow.gameObject, new Vector3(100, 86, 0), 6);
        Invoke("showOutButton", 1);
    }
    /// <summary>
    /// 使选框飞出场景
    /// </summary>
    public void onOutButton()
    {
        NGUITools.SetActive(outButton.gameObject, false);
        SpringPanel.Begin(preWindow.gameObject, new Vector3(2000, 86, 0), 6);
        Invoke("showInButton", 1);
    }

    void showOutButton()
    {
        NGUITools.SetActive(outButton.gameObject, true);
    }

    void showInButton()
    {
        NGUITools.SetActive(inButton.gameObject, true);
    }
    
}
