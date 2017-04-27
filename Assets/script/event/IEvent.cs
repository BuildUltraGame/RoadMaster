using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEvent
{
    public abstract class IEvent
    {
        protected object result;
        public bool cancelable = false;
        public void setCancel(bool cancel = true)
        {
            cancelable = cancel;
        }

        public void setResult<T>(T result)
        {
            this.result = result;
        }

        public T getResult<T>()
        {
            return (T)result;
        }

    }
}
