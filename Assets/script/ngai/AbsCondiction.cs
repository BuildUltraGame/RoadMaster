using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 抽象条件节点类
/// 去掉了执行功能
/// </summary>
public abstract class AbsCondiction : IAINode
{
    public abstract void addNode(IAINode node);
    public abstract void addNodes(ICollection c);
    public abstract bool condition(AIContext context);

    public void execute(AIContext context)
    {
        
    }

}
