using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 事件中心
/// </summary>
namespace UnityEvent
{
    public static class UnityEventCenter
    {
        private static readonly Dictionary<Type, List<object>> _cache = new Dictionary<Type, List<object>>();

        private static Comparison<object> comp = delegate (object x, object y)
        {//比较函数,这个是用于排序优先级的
            ListenerPack xx = (ListenerPack)x;
            ListenerPack yy = (ListenerPack)y;
            if (xx.priority < yy.priority)
            {
                return 1;
            }
            else if (xx.priority > yy.priority)
            {
                return -1;
            }
            else
            {
                return 0;
            }

        };


        /// <summary>
        /// Register the game object to listen for events of type T.
        /// </summary>
        /// <typeparam name="T">The type of event being listened for.</typeparam>
        /// <param name="obj"></param>
        public static void Register<T>(this object obj, int priority = 10)
        {
            if (!_cache.ContainsKey(typeof(T))) _cache[typeof(T)] = new List<object>();
            _cache[typeof(T)].Add(new ListenerPack(obj, priority));

            _cache[typeof(T)].Sort(comp);
        }

        /// <summary>
        /// Removes the registration for listening to events of type T.
        /// </summary>
        /// <typeparam name="T">The type of event to no longer listen for.</typeparam>
        /// <param name="obj"></param>
        public static void UnRegister<T>(this object obj)
        {
            if (!_cache.ContainsKey(typeof(T))) return;

            int n = -1;
            for (int i = 0; i < _cache[typeof(T)].Count; i++)
            {
                if ((_cache[typeof(T)][i] as ListenerPack).listener.Equals(obj))
                {
                    n = i;
                }

            }
            if (n != -1)
            {
                _cache[typeof(T)].RemoveAt(n);
            }
            _cache[typeof(T)].Sort(comp);
        }

        /// <summary>
        /// Register the game object to listen for events of type T.
        /// </summary>
        /// <typeparam name="T">The type of event being listened for.</typeparam>
        /// <param name="obj"></param>
        /// <param name="listener"></param>
        public static void Register(object obj, Type listener, int priority = 10)
        {
            if (!_cache.ContainsKey(listener))
                _cache[listener] = new List<object>();

            _cache[listener].Add(new ListenerPack(obj, priority));
            _cache[listener].Sort(comp);
        }

        /// <summary>
        /// Removes the registration for listening to events of type T.
        /// </summary>
        /// <typeparam name="T">The type of event to no longer listen for.</typeparam>
        /// <param name="obj"></param>
        /// <param name="listener"></param>
        public static void UnRegister(object obj, Type listener)
        {
            if (!_cache.ContainsKey(listener)) return;

            int n = -1;
            for (int i = 0; i < _cache[listener].Count; i++)
            {
                if ((_cache[listener][i] as ListenerPack).listener.Equals(obj))
                {
                    n = i;
                }

            }
            if (n != -1)
            {
                _cache[listener].RemoveAt(n);
            }

        }

        /// <summary>
        /// Notifies all listeners of event type T.
        /// </summary>
        /// <typeparam name="T">The type of event being notified.</typeparam>
        /// <param name="message"></param>
        public static void SendMessage<T>(object message)
        {
            if (!_cache.ContainsKey(message.GetType())) return;
            foreach (object x in _cache[message.GetType()])
            {
                try
                {
                    ((IListener<T>)((ListenerPack)x).listener).Handle((T)message);
                    if (((IEvent)message).cancelable)
                    {//如果消息被取消
                        break;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e.ToString());
                    Debug.LogError("send" + message.GetType() + "to" + x.ToString());

                }


            }

        }


    }


    internal class ListenerPack
    {
        public int priority = 10;
        public object listener;

        public ListenerPack(object l, int p)
        {
            this.listener = l;
            priority = p;
        }

    }
}