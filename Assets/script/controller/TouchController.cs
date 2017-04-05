using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventAggregator;
/// <summary>
/// 触摸控制器,功能是接受RequestSelectEvent的请求事件,然后返回SelectEvent事件
/// 只要用于当model层需要用户选择一个位置时候(发送RequestSelectEvent请求)
/// </summary>
public class TouchController : MonoBehaviour, IListener<RequestSelectEvent>
{
    private bool DEBUG = true;//调试用
    private Queue<RequestSelectEvent> reqQueue = new Queue<RequestSelectEvent>();
    
	void Awake () {
        EventAggregator.Register<RequestSelectEvent>(this);
	}

    void OnDisable()
    {
        EventAggregator.UnRegister<RequestSelectEvent>(this);
    }

    private string a;
	void Update () {
	    if(reqQueue.Count!=0){
            //有请求,需要监听用户点击的事件情况

            if (DEBUG)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit rh;
                    Physics.Raycast(ray, out rh);
                    int layout = rh.collider.gameObject.layer;
                    if (layout == Layers.ROAD || layout == Layers.RAILWAY)
                    {//如果点击到了路面(包括铁路和人行道)
                        sendResult(reqQueue.Dequeue(), Input.mousePosition);
                    }
                    else if (rh.collider.gameObject.layer == Layers.VEHICLE || rh.collider.gameObject.layer == Layers.CHARACTER)
                    { //如果点击到了车辆,或者人
                        sendResult(reqQueue.Dequeue(), rh.collider.gameObject);
                       
                    }
                }
            }
            else {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && touch.tapCount >= 2)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit rh;
                    Physics.Raycast(ray, out rh);
                    int layout = rh.collider.gameObject.layer;
                    if (layout == Layers.ROAD || layout == Layers.RAILWAY)
                    {//如果点击到了路面(包括铁路和人行道)
                        sendResult(reqQueue.Dequeue(),touch.position);
                    }
                    else if (rh.collider.gameObject.layer == Layers.VEHICLE || rh.collider.gameObject.layer == Layers.CHARACTER)
                    { //如果点击到了车辆,或者人
                        sendResult(reqQueue.Dequeue(), rh.collider.gameObject);

                    }

                }
            }

            
        }
	}


    private void sendResult(RequestSelectEvent e,Vector3 p)
    {
       SelectEvent se= e.createSelectEvent();
       se.addSelect(p);
       EventAggregator.SendMessage<SelectEvent>(se);//已经选择完毕,发送选择完毕事件
    }

    private void sendResult(RequestSelectEvent e, GameObject obj)
    {
        SelectEvent se = e.createSelectEvent();
        se.addSelect(obj);
        EventAggregator.SendMessage<SelectEvent>(se);//已经选择完毕,发送选择完毕事件
    }

    public void Handle(RequestSelectEvent message)
    {
        reqQueue.Enqueue(message);
    }
}
