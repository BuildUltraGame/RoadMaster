using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAINode : ICondition, IExecute
{
    void addNode(IAINode node);

    void addNodes(ICollection c);
}
