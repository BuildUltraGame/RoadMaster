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
    public GameObject[] vehicleToBuild;//连接制造槽的数组
    public int vehicleAmountUpgrd;//可一次性发车的数量
    public float createGap;//矿车发车的间隔
    public GameObject vehicleCardPrefab;
    public UITable vehicleCardTable;
    // Use this for initialization
    void Start () {
        UnityEventCenter.Register<setOffEvent>(this);
        UnityEventCenter.Register<setVehicleEvent>(this);
        for(int i=0;i<vehicleAmountUpgrd;i++)//初始化发车预选框
        {
            GameObject vehicle = NGUITools.AddChild(vehicleCardTable.gameObject,vehicleCardPrefab);
            vehicle.tag = Tags.CAR_SELECTOR;
            vehicle.transform.localScale = new Vector3(1, 1, 1);
            vehicle.transform.localPosition = new Vector3(1, 1, 1);
            vehicle.layer = Layers.UICARD;
        }
        vehicleCardTable.Reposition();        
    }

    void OnDisable()
    {
        UnityEventCenter.UnRegister<setOffEvent>(this);
        UnityEventCenter.UnRegister<setVehicleEvent>(this);
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void Handle(setOffEvent message)
    {

    }

    public void Handle(setVehicleEvent message)
    {
        message.obj.GetComponent<UISprite>().spriteName = message.name;
        //=message.id
        //id待完善-应建立唯一id-name对应
    }

    
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

    /// <summary>
    /// 卡片释放
    /// </summary>
    /// <param name="o">卡片的游戏物体</param>
    void onDragCrad(GameObject o)
    {
        print("!!!!!!!!!!!!!!!!!"+o.GetComponent<UICard>().getID());

        
    }

}
