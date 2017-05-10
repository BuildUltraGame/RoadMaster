using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPoint : MonoBehaviour {

	public List<RoadPoint> reachList;
	public int Tag;	

	public bool active =true;

	public delegate RoadPoint canReachWhat(RailwayMovable obj,out bool flag);

	public canReachWhat canReach = null;

	// Use this for initialization
	void Start () {
		
	}

	public RoadPoint getNextPoint(RailwayMovable obj){
        bool b=false;
		if (canReach != null) {
			return canReach (obj,out b);
		} else {
			//默认行为是得到所有能到达的点中非玩家到来的那个点
			RoadPoint from=obj.fromPoint;//获取到玩家的来到的点

			RoadPoint next = null;

			foreach(RoadPoint p in reachList){
				if(!p.Equals(from)){
					if(obj.Tag==this.Tag&&p.active){
						next = p;
					}
				}
			}

			if (next == null) {
				//为空证明找不到可用点,这时候需要回头

				return from;
			} else {
			
				return next;
			}
			
		}

	}


	public RoadPoint getNextPoint(RailwayMovable obj,out bool flag)
    {
        if (canReach != null)
        {
            return canReach(obj,out flag);
        }
        else
        {
            //默认行为是得到所有能到达的点中非玩家到来的那个点
            RoadPoint from = obj.fromPoint;//获取到玩家的来到的点

            RoadPoint next = null;
            //print(from.transform.position);
            foreach (RoadPoint p in reachList)
            {
                if (!p.Equals(from))
                {
                    if (obj.Tag == this.Tag && p.active)
                    {
                        next = p;
                    }
                }
            }

            if (next == null)
            {
                //为空证明找不到可用点,这时候需要回头
                flag = false;
                return from;
            }
            else
            {
                flag = true;
                return next;
            }

        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
