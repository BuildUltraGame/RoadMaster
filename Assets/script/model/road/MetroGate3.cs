using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 三道口
/// </summary>
public class MetroGate3 : MetroGate {

    private List<Vector3> linkRoad = new List<Vector3>();
    private Vector3 blockRoad;

    private Vector3[] allPoint;



    private Vector3 center;


	// Use this for initialization
	void Start () {

        base.Start();

        center = pointList[0];
       
        allPoint=new Vector3[3];
        allPoint[0]=pointList[1];
        allPoint[1]=pointList[2];
        allPoint[2]=pointList[3];

        linkRoad.Add(allPoint[0]);
        linkRoad.Add(center);
        linkRoad.Add(allPoint[1]);

        blockRoad = allPoint[2];
     
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    override protected void OnObjEnter(Collision collision)
    {
       int pointNum= FromWhichPoint(allPoint,collision.transform.position);

       if (blockRoad == allPoint[pointNum])
       {
           print("不能通过");
           //TODO:不能通过,应该回头
           Railwaymovable obj = collision.gameObject.GetComponent<Railwaymovable>();
           obj.addRoadPoint(allPoint[pointNum]);
           obj.addRoadPoint(collision.transform.position);


        }
        else
        {
            print("可以通过");
            //可以通过
             Railwaymovable obj = collision.gameObject.GetComponent<Railwaymovable>();
            if(allPoint[pointNum]==linkRoad[0]){
                obj.addRoadsPoint(linkRoad);
            }
            else
            {
                List<Vector3> l = new List<Vector3>(linkRoad);
                l.Reverse();
                obj.addRoadsPoint(l);
            }


        }
        

    }


    public override void GateChange(Vector3 v, int linkNum)
    {
        if(linkNum<=0||linkNum%3==0){
            return;
        }

        
        int minNum=FromWhichPoint(allPoint,v);
       
        

        linkRoad.Clear();
        linkRoad.Add(allPoint[minNum]);
        linkRoad.Add(allPoint[(minNum+linkNum)%3]);
        
        

    }


}
