using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建造者
/// 挂载这个脚本,然后本游戏对象里任意脚本 中调用  SendMessage(Builder.BUILDFUNC);
/// 
/// 就直接会建造相应的prefab
/// </summary>
public class Builder : MonoBehaviour {

    public const string BUILDFUNC = "BuilderToBuild";
    public GameObject prefab;
    public float buildTime = 0.5f;
    [HideInInspector]public GameObject entity;
    private GameobjBase gb;

    void Start()
    {
        gb = GetComponent<GameobjBase>();
    }
    public void BuilderToBuild()
    {
        Invoke("Build", buildTime);
    }

    void Build()
    {
        entity = Instantiate<GameObject>(prefab, transform.position + transform.right * 2, transform.localRotation);
        entity.GetComponent<GameobjBase>().setOwner(gb.getOwner());
    }


}
