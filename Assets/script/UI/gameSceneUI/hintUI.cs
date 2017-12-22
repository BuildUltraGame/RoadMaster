using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hintUI : MonoBehaviour {

    public UILabel report;
    public UILabel unitReport;
    public UILabel winOrLose;
    public UILabel endHint;
    public UIButton next;
    public UIButton start;
    public UIButton end;
    private int mission;//预留，当前关卡，以读取关卡和单位介绍
    private int isNewUnit;//是否出现新单位
    // Use this for initialization
	void Start () {
        isNewUnit = 1;
        if(isNewUnit!=1)
        {
            NGUITools.SetActive(next.gameObject, false);
            NGUITools.SetActive(start.gameObject, true);
        }
        setLabelText();
        if (baseController.isWin == 1)
        {
            NGUITools.SetActive(end.gameObject, true);
            NGUITools.SetActive(endHint.gameObject, true);
            NGUITools.SetActive(next.gameObject, false);
            NGUITools.SetActive(start.gameObject, false);
            NGUITools.SetActive(report.gameObject, false);
            NGUITools.SetActive(unitReport.gameObject, false);
            winOrLose.text = "哈哈哈！";
            endHint.text = "这波我们可赚大了老板！这下，矿区只有您一个老板啦！今儿个您心情好，赏小的几个钱怎么样？";
            baseController.isWin = 0;
            return;
        }
        if (baseController.isWin == -1)
        {
            NGUITools.SetActive(end.gameObject, true);
            NGUITools.SetActive(endHint.gameObject, true);
            NGUITools.SetActive(next.gameObject, false);
            NGUITools.SetActive(start.gameObject, false);
            NGUITools.SetActive(report.gameObject, false);
            NGUITools.SetActive(unitReport.gameObject, false);
            winOrLose.text = "可恶！";
            endHint.text = "老板，什么时候把小的的工钱结一下……";
            baseController.isWin = 0;
            return;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
    }
    public void onClickNext()
    {
        NGUITools.SetActive(next.gameObject, false);
        NGUITools.SetActive(start.gameObject, true);
        NGUITools.SetActive(report.gameObject, false);
        NGUITools.SetActive(unitReport.gameObject, true);
    }

    public void onClickStart()
    {
        SceneManager.LoadSceneAsync("level"+baseController.missionToLoad.ToString());
        Time.timeScale = 1;

    }

    public void onEnd()
    {
        SceneManager.LoadSceneAsync("selector");
    }
    void setLabelText()
    {
        report.text = "老板您早！欢迎来到这片充满了轨道的矿区！如您所见，这片矿区富的流油，真希望是我的啊……不管怎样，虽然矿区产量惊人，竞争也异常激烈，还有其他老板……不，其他奸商！老板只有您一个！控制这片矿区的重中之重，就在于控制那一个个道闸。让我们的矿车平安通过，说不准还能对别人的矿车动点手脚嘿嘿！把矿运到矿区的出口，咱们就赚大钱啦！";
        unitReport.text = "这是我们的扳道闸工人。他们能够扳动道闸，使得轨道上的矿车通向连通的方向,包括其他奸商的矿车！当然,您是老板,不是奸商~";
    }
}
