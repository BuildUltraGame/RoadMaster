using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEventAggregator
{
    public abstract class IEvent<T>
    {
        protected T result;
        public bool cancelable = false;
        public void setCancel(bool cancel = true)
        {
            cancelable = cancel;
        }

        public void setResult(T result)
        {
            this.result = result;
        }

        public T getResult()
        {
            return result;
        }

    }
}
