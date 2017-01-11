using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class Railway : MonoBehaviour {

    protected List<Vector3> pointList = new List<Vector3>();


	// Use this for initialization
    protected void Start () {
		Transform[] ts=GetComponentsInChildren<Transform>();
       
        foreach(Transform t in ts){
            pointList.Add(t.position);

        }
        pointList.Remove(transform.position);

        print("start"+name+pointList.Count);
	}

    void OnCollisionEnter(Collision collision)
    {

    
        if (collision.gameObject.tag == "moveObj")
        {
            print(name);
            OnObjEnter(collision);

         
        }
    }

    void OnCollisionExit(Collision collision)
    {

        
        if (collision.gameObject.layer ==Layers.VEHICLE)
        {

            Railwaymovable obj = collision.gameObject.GetComponent<Railwaymovable>();
            obj.removeRoadsPoint(pointList);
        }
    }


    virtual protected void OnObjEnter(Collision collision)
    {
        Railwaymovable obj = collision.gameObject.GetComponent<Railwaymovable>();
        
        if (Vector3.Distance(collision.transform.position, pointList[0]) <
            Vector3.Distance(collision.transform.position, pointList[pointList.Count - 1]))
        {
            obj.addRoadsPoint(new List<Vector3>(pointList));

        }
        else
        {
            List<Vector3> vs = new List<Vector3>(pointList);
            vs.Reverse();
            obj.addRoadsPoint(vs);
        }

     //   obj.printRoad();
    
    }

   
    

    
}
