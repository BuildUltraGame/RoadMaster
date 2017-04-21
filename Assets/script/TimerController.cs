using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全局计时器类
/// 
/// 使用本计时器的情况
/// 1.这个计时器是和游戏所在状态有关(比如暂停,比如游戏开始等)
/// 2.这个计时器要实现的功能必须是以下的一种:
///                         A.一定时间后执行某代码
///                         B.每隔一定时间执行某代码
///                         C.要获取开始到现在的时间(类似计时器那种,你跑步的那种计时器)
///                         
/// 本计时器提供的功能:
///     开始
///     暂停
///     停止
///     
/// 
/// 
/// 
/// 
/// 给个使用例子:

///  public class test : MonoBehaviour {

//    private TimerController.Timer t;

//    // Use this for initialization
//    void Start () {

//    }

//    // Update is called once per frame
//    void Update () {
//        if(Input.GetKeyUp(KeyCode.A)){
//            print("!!!!!!!!!!!!!");
//            t= TimerController.getInstance().NewTimer(3f, true, delegate(float time)
//            {
//                print(time);

//            }, delegate()
//            {
//                print("OK");
//            });

//            t.Start();
//        }

//        if (Input.GetKeyUp(KeyCode.S))
//        {
//            t.Start();
//        }

//        if (Input.GetKeyUp(KeyCode.D))
//        {
//            t.Pause();
//        }

//        if (Input.GetKeyUp(KeyCode.F))
//        {
//            t.Stop();
//        }


//    }
//}

/// 
/// 
/// 
/// </summary>
public class TimerController : MonoBehaviour
{
    protected List<Timer> timerList = new List<Timer>();//全部计时器list

    private static TimerController _instance;//单例

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    public static TimerController getInstance(){
        return _instance;
    }

    /// <summary>
    /// 生成计时器
    /// </summary>
    /// <param name="interval">时间间隔</param>
    /// <param name="repeat">是否重复执行</param>
    /// <param name="update">回调函数,会一直返回当前开始到现在多久,排开暂停的时间</param>
    /// <param name="run">到达指定时间间隔后执行的函数</param>
    /// <returns>计时器</returns>
    public Timer NewTimer(float interval, bool repeat, Timer.UpdataHandle update, Timer.RunEventHandle run)
    {
        Timer timer = new Timer(this,interval,repeat,update,run);
        timerList.Add(timer);
        return timer;
    }

    /// <summary>
    /// 生产计时器
    /// </summary>
    /// <param name="intervalGen">时间间隔产生器</param>
    /// <param name="repeat">是否重复执行</param>
    /// <param name="update">回调函数,会一直返回当前开始到现在多久,排开暂停的时间</param>
    /// <param name="run">到达指定时间间隔后执行的函数</param>
    /// <returns></returns>
    public Timer NewTimer(Func<float> intervalGen, bool repeat, Timer.UpdataHandle update, Timer.RunEventHandle run)
    {
        Timer timer = new Timer(this, intervalGen, repeat, update, run);
        timerList.Add(timer);
        return timer;
    }


    /// <summary>
    /// 生成计时器,默认不重复执行,而且还要手动设定函数,不推荐
    /// </summary>
    /// <param name="interval">时间间隔</param>
    /// <returns>计时器</returns>
    public Timer NewTimer(float interval,bool repeat=false)
    {
        Timer timer = new Timer(this, interval,repeat);
        timerList.Add(timer);
        return timer;
    }

    /// <summary>
    /// 移除计时器
    /// </summary>
    /// <param name="t"></param>
    public void RemoveTimer(Timer t) {
        timerList.Remove(t);
    }


    public void StartAll()
    {
        foreach (Timer t in timerList)
        {
            t.Start();
        }
    }

    public void StopAll()
    {
        foreach (Timer t in timerList)
        {
            t.Stop();
        }
    }

    public void PauseAll()
    {
        foreach (Timer t in timerList)
        {
            t.Pause();
        }
    }

    void Update()
    {
        foreach(Timer t in timerList){
            t.Update();
        }
    }

     public class Timer
    {
        private float startTime=-1;//开始计时的时间
        private float pauseStartTime=-1;//开始暂停的时间
        private float pauseTotalTime=0;//暂停总时间
        private bool startPauseFlag=false;//是否处于暂停状态
        private bool startFlag = false;//是否处于计时状态
        private float intervalTime=0;//设置间隔时间
        private bool isRepeat = false;//是否重复计时
        private bool autoDestory = false;//是否自动销毁


        private TimerController controller;//全局控制器

        public delegate void UpdataHandle(float time);//这里用的委托
        public UpdataHandle updataFun;//委托对象,每次更新都会调用的函数,会传个计时多久的参数回来

        public delegate void RunEventHandle();//这里用的委托
        public RunEventHandle runFun;//委托对象,到达间隔时间会执行一次,是否重复执行取决于repeat的值

        public Func<float> intervalTimeGenFunc=null;

         /// <summary>
        /// 不要直接使用本方法生成计时器,请用TimerController.newTimer
         /// </summary>
         /// <param name="c"></param>
         /// <param name="interval"></param>
         /// <param name="repeat"></param>
         /// <param name="update"></param>
         /// <param name="run"></param>
        public Timer(TimerController c, float interval, bool repeat, UpdataHandle update, RunEventHandle run)
            : this(c, interval,repeat)
        {
            
            updataFun = update;
            runFun = run;
        }

        public Timer(TimerController c, Func<float> gen, bool repeat, UpdataHandle update, RunEventHandle run)
           : this(c, gen(), repeat)
        {
            intervalTimeGenFunc = gen;
            updataFun = update;
            runFun = run;
        }

        /// <summary>
        /// 不要直接使用本方法生成计时器,请用TimerController.newTimer
        /// </summary>
        /// <param name="c"></param>
        /// <param name="interval"></param>
        /// <param name="repeat"></param>
        public Timer(TimerController c, float interval, bool repeat=false)
        {
            if (interval <= 0)
            {
                throw new System.Exception("什么鬼,你传了什么鬼参数进来");
            }
            isRepeat = repeat;
            controller = c;
            intervalTime = interval;
            
        }
         /// <summary>
         /// 设置自动销毁
         /// </summary>
         /// <param name="flag">是否自动销毁</param>
        public void setAutoDestory(bool flag)
        {
            autoDestory = flag;
        }


         /// <summary>
         /// 开始
         /// </summary>
        public void Start()
        {
            if (intervalTimeGenFunc!=null)
            {
                intervalTime = intervalTimeGenFunc();
            }
            startFlag = true;
            if(startPauseFlag){
                startPauseFlag = false;
                pauseTotalTime += Time.time - pauseStartTime;
                
            }
            else
            {
                startTime = Time.time;
            }

        }

         /// <summary>
         /// 设置时间间隔
         /// </summary>
         /// <param name="newInterval">新的时间间隔</param>
        public void setInterval(float newInterval)
        {
            intervalTime = newInterval;
         }
         /// <summary>
         /// 停止
         /// </summary>
        public void Stop()
        {
            startFlag = false;
            pauseTotalTime = 0;
            startPauseFlag = false;

        }

         /// <summary>
         /// 暂停
         /// </summary>
        public void Pause() 
        {
            pauseStartTime = Time.time;
            startPauseFlag = true;
        }

        
         /// <summary>
         /// 别调用这个函数
         /// </summary>
        public void Update()
        {
            if(startFlag==false){
                return;
            }

            if(startPauseFlag){
                //正在暂停

                if (updataFun != null)
                {
                    updataFun(pauseStartTime - startTime - pauseTotalTime);
                }
                
            }
            else
            {//计时状态

                if (updataFun != null)
                {
                    updataFun(Time.time - startTime-pauseTotalTime);
                }

                if (runFun != null)
                {
                    if (Time.time - startTime - pauseTotalTime >= intervalTime)
                    {//到达计时间隔,委托运行
                        runFun();
                        
                        if(isRepeat){
                            pauseTotalTime = 0;
                            Start();
                        }
                        else
                        {
                             Stop();
                             if(autoDestory){
                                 Destroy();
                             }
                        }

                    }

                }
            }


           
        }
         /// <summary>
         /// 销毁计时器,不用了就销毁
         /// </summary>
        public void Destroy()
        {
            controller.RemoveTimer(this);
        }

    }

}
