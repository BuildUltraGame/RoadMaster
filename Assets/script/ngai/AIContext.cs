using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// AI上下文,传递行为树变量用
/// </summary>
public class AIContext {

    private Hashtable table = new Hashtable();

    public void add(Object key ,Object val) {
        table.Add(key,val);
    }

    public Object get(Object key)
    {
        return table[key] as Object;
    }


}
