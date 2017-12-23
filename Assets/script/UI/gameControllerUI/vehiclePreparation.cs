using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;

/// <summary>
/// 预选车辆卡片的相关UI脚本
/// </summary>
public class vehiclePreparation : MonoBehaviour {

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


    public int vehicleAmountUpgrd;
    public float createGap;
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
        return sp;
    }
}
