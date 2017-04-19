using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailwayMovable : MonoBehaviour {
	public int Tag;

	public RoadPoint fromPoint;
	public RoadPoint nextPoint;

	public float step=0.5f;
	public bool ignoreY = true;

	// Use this for initialization
	void Start () {
		if(nextPoint==null){
			initRoad ();
		}


	}


	private void initRoad(){
		//初始先找个最近的点,Tag相同
		GameObject[] objs=GameObject.FindGameObjectsWithTag(Tags.RAILWAY_POINT);
		float d = float.MaxValue;
		GameObject temp = null;
		foreach(GameObject o in objs){ 
			float d_temp = Vector3.Distance (o.transform.position, transform.position);
			if(d>d_temp){
				//找最近的点
				temp=o;
				d = d_temp;
			}
		}

		if (temp != null) {
			//找到最近点
			nextPoint = temp.GetComponent<RoadPoint>();
		} else {
			nextPoint = objs[0].GetComponent<RoadPoint>();
		}
	
	}





	
	// Update is called once per frame
	void FixedUpdate () {
		if(nextPoint!=null){
			//向下一个点走
			lookFront(nextPoint.transform.position);

			Vector3 target = nextPoint.transform.position;

			target.y = ignoreY ? target.y : transform.position.y;
			transform.position = Vector3.MoveTowards (transform.position,target,step);
		
				
			if(Vector3.Distance(transform.position,target)<0.05){
				//到了终点附近逻辑等价于已经到达
				RoadPoint tempp=nextPoint;
				nextPoint = nextPoint.getNextPoint(this);
				fromPoint = tempp;
			}
		}



	}


	private void lookFront(Vector3 v){
		Vector3 front = Vector3.Normalize(v - transform.position);

		if(ignoreY){
			front.y = transform.position.y;
		}

		transform.forward = front;
	}
}
