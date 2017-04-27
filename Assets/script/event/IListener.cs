using System;
using UnityEvent;
namespace UnityEvent
{

    public interface IListener<T>
    {
        void Handle(T message);
    }

}