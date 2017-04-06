using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 请求选择事件
/// 当建造 人 单位的时候需要选择目的地(或者跟踪的物体),此时会发生该事件
/// </summary>
public class RequestSelectEvent : BaseEvent {
    private int pointNum = 1;
    public RequestSelectEvent(GameObject _subject,int pointNum=1)//此处为了以后扩展,多了一个参数,一般情况都是请求一个点而已
        : base(_subject, "RequestSelect", null)
    {
        this.pointNum = pointNum;
    }
    /// <summary>
    /// 请求选择的目标数(或者点数)
    /// </summary>
    /// <returns></returns>
    public int getPointNum()
    {
        return pointNum;
    }

    /// <summary>
    /// 生成与之关联的确认选择事件,当已经获取到了所需的目的地或者物体的时候,发送该事件
    /// </summary>
    /// <returns>事件</returns>
    public SelectEvent createSelectEvent() {
        return new SelectEvent(getSubject());
    }


}
