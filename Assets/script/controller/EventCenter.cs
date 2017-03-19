using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件中心,用于分发事件
/// </summary>
public class EventCenter : MonoBehaviour {

    private static EventCenter _instance;

    private List<EventListener> listenerList=new List<EventListener>();//监听者列表
    private Queue<Event> eventQueue = new Queue<Event>();//事件消息队列
    

    public static EventCenter getInstance()
    {
        return _instance;
    }

    

    void Awake()
    {
        _instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// 加入监听器
    /// </summary>
    /// <param name="l">监听者</param>
    public void addListener(EventListener l)
    {
        listenerList.Add(l);
    }

    /// <summary>
    /// 清除监听器
    /// </summary>
    public void removeListener()
    {
        listenerList.Clear();
    }
    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="_subject">事件主体</param>
    /// <param name="verb">动词</param>
    /// <param name="_object">事件发生对象</param>
    /// <param name="args">其他参数</param>
    /// <returns>是否成功</returns>
    public bool sendEvent(GameObject _subject, string verb, GameObject _object, params object[] args)
    {
        Event e = new Event(_subject,verb,_object,args);
        eventQueue.Enqueue(e);
        return true;
    }

	
	// Update is called once per frame
	void Update () {
        //每次只发送一个事件到监听者
        while(eventQueue.Count>0){
            Event e = eventQueue.Dequeue();
            foreach (EventListener l in listenerList)
            {
                l.eventHappend(e);
            }
        }
	}


    /// <summary>
    /// 发生事件监听接口
    /// </summary>
    public interface EventListener
    {
        /// <summary>
        /// 发生事件的时候调用该接口
        /// </summary>
        /// <param name="e">事件</param>
        public void eventHappend(Event e);
    }

    public class Event
    {
        private GameObject _subject;
        private string verb;
        private GameObject _object;
        private object[] args;

        /// <summary>
        /// 创建事件
        /// </summary>
        /// <param name="_subject">事件主体</param>
        /// <param name="verb">动作</param>
        /// <param name="_object">事件作用对象</param>
        /// <param name="args">其他参数</param>
        public Event(GameObject _subject, string verb, GameObject _object,object[] args)
        {
            this._subject = _subject;
            this._object = _object;
            this.verb = verb;
            this.args = args;

        }

        public GameObject getSubject()
        {
            return _subject;
        }

        public GameObject getObject()
        {
            return _object;
        }

        public string getVerb()
        {
            return verb;
        }

        public object[] getArgs()
        {
            return args;
        }
    }

}
